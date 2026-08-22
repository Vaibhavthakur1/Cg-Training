using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

    public interface IEntity
    {
        int Id { get; }
    }

    public enum OrderStatus
    {
        Placed,
        Queued,
        Dispatched,
        Delivered,
        Cancelled
    }

    public record MenuItem(int Id, string Name, decimal Price) : IEntity;

    public class Restaurant : IEntity
    {
        public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public bool IsOpen { get; set; } = true;
        public List<MenuItem> Menu { get; init; } = new();

        public Restaurant(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public record Customer(int Id, string Name, bool IsVip) : IEntity;

    public record OrderItem(MenuItem MenuItem, int Quantity);

    public class Order : IEntity
    {
        public int Id { get; init; }
        public Customer Customer { get; init; } = null!;
        public Restaurant Restaurant { get; init; } = null!;
        public List<OrderItem> Items { get; init; } = new();
        public DateTime PlacedAt { get; init; }
        public bool IsExpress { get; init; }
        public OrderStatus Status { get; set; }

        public Order(int id, Customer customer, Restaurant restaurant, bool isExpress, DateTime placedAt)
        {
            Id = id;
            Customer = customer;
            Restaurant = restaurant;
            IsExpress = isExpress;
            PlacedAt = placedAt;
            Status = OrderStatus.Placed;
        }
    }

    public record DeliveryAgent(int Id, string Name) : IEntity;

    public record DispatchRecord(Order Order, DeliveryAgent Agent, DateTime DispatchedAt);


    // 2. GENERIC REPOSITORY LAYER
   
    public class Repository<T> : IEnumerable<T> where T : class, IEntity
    {
        private readonly Dictionary<int, T> _storage = new();

        public void Add(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            if (!_storage.TryAdd(entity.Id, entity))
            {
                throw new InvalidOperationException($"Entity of type {typeof(T).Name} with ID {entity.Id} already exists.");
            }
        }

        public void Update(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            if (!_storage.ContainsKey(entity.Id))
            {
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID {entity.Id} was not found.");
            }
            _storage[entity.Id] = entity;
        }

        public bool Remove(int id) => _storage.Remove(id);

        public T? GetById(int id) => _storage.TryGetValue(id, out var entity) ? entity : null;

        public IReadOnlyCollection<T> GetAll() => _storage.Values;

        public IEnumerator<T> GetEnumerator() => _storage.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    // 3. PRIORITY COMPARER
    public class OrderPriorityComparer : IComparer<Order>
    {
        public int Compare(Order? x, Order? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x is null) return 1;
            if (y is null) return -1;

            // 1. Express flag first (true > false)
            int expressComp = y.IsExpress.CompareTo(x.IsExpress);
            if (expressComp != 0) return expressComp;

            // 2. VIP customer next (true > false)
            int vipComp = y.Customer.IsVip.CompareTo(x.Customer.IsVip);
            if (vipComp != 0) return vipComp;

            // 3. PlacedAt ascending (earlier dates first)
            int dateComp = x.PlacedAt.CompareTo(y.PlacedAt);
            if (dateComp != 0) return dateComp;

            return x.Id.CompareTo(y.Id);
        }
    }

    // 4. MULTI-TIER FIFO DISPATCH QUEUE
    public class DispatchQueue
    {
        private readonly Queue<Order> _expressQueue = new();
        private readonly Queue<Order> _vipQueue = new();
        private readonly Queue<Order> _standardQueue = new();

        public int Count => _expressQueue.Count + _vipQueue.Count + _standardQueue.Count;

        public void Enqueue(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);
            order.Status = OrderStatus.Queued;

            if (order.IsExpress)
            {
                _expressQueue.Enqueue(order);
            }
            else if (order.Customer.IsVip)
            {
                _vipQueue.Enqueue(order);
            }
            else
            {
                _standardQueue.Enqueue(order);
            }
        }

        public Order DispatchNext()
        {
            if (_expressQueue.Count > 0) return _expressQueue.Dequeue();
            if (_vipQueue.Count > 0) return _vipQueue.Dequeue();
            if (_standardQueue.Count > 0) return _standardQueue.Dequeue();

            throw new InvalidOperationException("The dispatch queue is empty.");
        }

        public bool TryDispatchNext(out Order? order)
        {
            if (_expressQueue.Count > 0) { order = _expressQueue.Dequeue(); return true; }
            if (_vipQueue.Count > 0) { order = _vipQueue.Dequeue(); return true; }
            if (_standardQueue.Count > 0) { order = _standardQueue.Dequeue(); return true; }

            order = null;
            return false;
        }

        public void PrependOrder(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);
            order.Status = OrderStatus.Queued;

            Queue<Order> target = order.IsExpress ? _expressQueue :
                                  order.Customer.IsVip ? _vipQueue : _standardQueue;

            var temp = new Queue<Order>();
            temp.Enqueue(order);
            while (target.Count > 0)
            {
                temp.Enqueue(target.Dequeue());
            }
            while (temp.Count > 0)
            {
                target.Enqueue(temp.Dequeue());
            }
        }
    }

    // ==========================================
    // 5. DISPATCH ENGINE & REPORTING CORE
    // ==========================================

    public class DispatchEngine
    {
        private readonly DispatchQueue _queue = new();
        private readonly LinkedList<DeliveryAgent> _agentRoster = new();
        private readonly Stack<DispatchRecord> _dispatchHistory = new();

        public void AddAgent(DeliveryAgent agent)
        {
            ArgumentNullException.ThrowIfNull(agent);
            _agentRoster.AddLast(agent);
        }

        public void EnqueueOrder(Order order) => _queue.Enqueue(order);

        public DeliveryAgent GetNextAvailableAgent()
        {
            if (_agentRoster.First is null)
            {
                throw new InvalidOperationException("No delivery agents registered in the roster.");
            }

            // O(1) round-robin pop from head and push to tail
            var firstNode = _agentRoster.First;
            _agentRoster.RemoveFirst();
            _agentRoster.AddLast(firstNode);

            return firstNode.Value;
        }

        public DispatchRecord Dispatch()
        {
            var order = _queue.DispatchNext();
            var agent = GetNextAvailableAgent();

            order.Status = OrderStatus.Dispatched;
            var record = new DispatchRecord(order, agent, DateTime.UtcNow);
            _dispatchHistory.Push(record);

            return record;
        }

        public DispatchRecord UndoLastDispatch()
        {
            if (_dispatchHistory.Count == 0)
            {
                throw new InvalidOperationException("No dispatch operations to undo.");
            }

            var record = _dispatchHistory.Pop();

            // 1. Restore order status and return to front of priority queue
            record.Order.Status = OrderStatus.Queued;
            _queue.PrependOrder(record.Order);

            // 2. Return agent to the front of the roster
            if (_agentRoster.Last is not null && ReferenceEquals(_agentRoster.Last.Value, record.Agent))
            {
                var lastNode = _agentRoster.Last;
                _agentRoster.RemoveLast();
                _agentRoster.AddFirst(lastNode);
            }
            else
            {
                var node = _agentRoster.Find(record.Agent);
                if (node is not null)
                {
                    _agentRoster.Remove(node);
                    _agentRoster.AddFirst(node);
                }
            }

            return record;
        }

        // --- Reports ---

        public HashSet<int> TodaysUniqueCustomerIds(IEnumerable<Order> allOrders)
        {
            var today = DateTime.UtcNow.Date;
            var result = new HashSet<int>();

            foreach (var order in allOrders)
            {
                if (order.PlacedAt.ToUniversalTime().Date == today)
                {
                    result.Add(order.Customer.Id);
                }
            }
            return result;
        }

        public Dictionary<int, int> LowAvailabilityRestaurants(IEnumerable<Restaurant> restaurants, int minMenuItems)
        {
            var result = new Dictionary<int, int>();

            foreach (var r in restaurants)
            {
                if (r.Menu.Count < minMenuItems)
                {
                    result[r.Id] = r.Menu.Count;
                }
            }
            return result;
        }

        public List<(string ItemName, int TotalOrdered)> TopOrderedItems(IEnumerable<Order> allOrders, int topN)
        {
            var counts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (var order in allOrders)
            {
                foreach (var item in order.Items)
                {
                    counts[item.MenuItem.Name] = counts.GetValueOrDefault(item.MenuItem.Name) + item.Quantity;
                }
            }

            return counts
                .Select(kvp => (ItemName: kvp.Key, TotalOrdered: kvp.Value))
                .OrderByDescending(x => x.TotalOrdered)
                .Take(topN)
                .ToList();
        }

        public bool CustomerOrderedFromBothRestaurants(
            int customerId,
            int restaurantIdA,
            int restaurantIdB,
            IEnumerable<Order> allOrders)
        {
            var customerVisitedRestaurants = new HashSet<int>();

            foreach (var order in allOrders)
            {
                if (order.Customer.Id == customerId)
                {
                    customerVisitedRestaurants.Add(order.Restaurant.Id);
                }
            }

            return customerVisitedRestaurants.IsSupersetOf(new[] { restaurantIdA, restaurantIdB });
        }
    }


    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("=== QuickBite Dispatch Engine Demo ===\n");

            // 1. Setup Repositories
            var customerRepo = new Repository<Customer>();
            var restaurantRepo = new Repository<Restaurant>();
            var orderRepo = new Repository<Order>();

            var alice = new Customer(1, "Alice (Regular)", false);
            var bob = new Customer(2, "Bob (VIP)", true);
            var charlie = new Customer(3, "Charlie (Regular)", false);

            customerRepo.Add(alice);
            customerRepo.Add(bob);
            customerRepo.Add(charlie);

            var burgerJoint = new Restaurant(101, "Burger Joint");
            burgerJoint.Menu.Add(new MenuItem(1, "Cheeseburger", 9.99m));
            burgerJoint.Menu.Add(new MenuItem(2, "Fries", 3.99m));

            var pizzaPlace = new Restaurant(102, "Pizza Palace");
            pizzaPlace.Menu.Add(new MenuItem(3, "Margherita Pizza", 14.99m));
            pizzaPlace.Menu.Add(new MenuItem(4, "Garlic Bread", 4.99m));
            pizzaPlace.Menu.Add(new MenuItem(5, "Soda", 1.99m));

            var noodleBar = new Restaurant(103, "Noodle Express"); // Low availability (< 2 items)
            noodleBar.Menu.Add(new MenuItem(6, "Ramen", 12.00m));

            restaurantRepo.Add(burgerJoint);
            restaurantRepo.Add(pizzaPlace);
            restaurantRepo.Add(noodleBar);

            // 2. Initialize Dispatch Engine & Agents
            var engine = new DispatchEngine();
            engine.AddAgent(new DeliveryAgent(1, "Agent Jack"));
            engine.AddAgent(new DeliveryAgent(2, "Agent Sarah"));

            // 3. Create Orders
            var order1 = new Order(1001, alice, burgerJoint, isExpress: false, DateTime.UtcNow.AddMinutes(-20)); // Standard
            order1.Items.Add(new OrderItem(burgerJoint.Menu[0], 2)); // 2 Burgers

            var order2 = new Order(1002, bob, pizzaPlace, isExpress: false, DateTime.UtcNow.AddMinutes(-15)); // VIP
            order2.Items.Add(new OrderItem(pizzaPlace.Menu[0], 1)); // 1 Pizza

            var order3 = new Order(1003, charlie, pizzaPlace, isExpress: true, DateTime.UtcNow.AddMinutes(-5)); // Express
            order3.Items.Add(new OrderItem(pizzaPlace.Menu[0], 3)); // 3 Pizzas
            order3.Items.Add(new OrderItem(pizzaPlace.Menu[1], 1)); // 1 Garlic Bread

            var order4 = new Order(1004, alice, pizzaPlace, isExpress: false, DateTime.UtcNow.AddMinutes(-2)); // Alice visits 2nd restaurant
            order4.Items.Add(new OrderItem(pizzaPlace.Menu[2], 2)); // 2 Sodas

            orderRepo.Add(order1);
            orderRepo.Add(order2);
            orderRepo.Add(order3);
            orderRepo.Add(order4);

            // Enqueue in non-priority order
            engine.EnqueueOrder(order1);
            engine.EnqueueOrder(order2);
            engine.EnqueueOrder(order3);

            // 4. Demonstrate Priority Dispatch
            Console.WriteLine("--- Dispatching Orders ---");
            var d1 = engine.Dispatch(); 
            Console.WriteLine($"Dispatched Order #{d1.Order.Id} (Express: {d1.Order.IsExpress}, VIP: {d1.Order.Customer.IsVip}) to {d1.Agent.Name}");

            var d2 = engine.Dispatch(); 
            Console.WriteLine($"Dispatched Order #{d2.Order.Id} (Express: {d2.Order.IsExpress}, VIP: {d2.Order.Customer.IsVip}) to {d2.Agent.Name}");

            // 5. Demonstrate Undo Last Dispatch
            Console.WriteLine("\n--- Undo Last Dispatch ---");
            var undone = engine.UndoLastDispatch();
            Console.WriteLine($"Undid dispatch for Order #{undone.Order.Id} assigned to {undone.Agent.Name}. Status reverted to: {undone.Order.Status}");

            var d2Again = engine.Dispatch(); 
            Console.WriteLine($"Re-dispatched Order #{d2Again.Order.Id} to {d2Again.Agent.Name}");

            
            Console.WriteLine("\n--- Real-Time Reports ---");

            var uniqueCustomers = engine.TodaysUniqueCustomerIds(orderRepo);
            Console.WriteLine($"Today's Unique Customers ({uniqueCustomers.Count}): [{string.Join(", ", uniqueCustomers)}]");

            var lowAvailability = engine.LowAvailabilityRestaurants(restaurantRepo, minMenuItems: 2);
            Console.WriteLine($"Low Availability Restaurants (< 2 items): {string.Join(", ", lowAvailability.Select(kv => $"Rest #{kv.Key} has {kv.Value} items"))}");

            var topItems = engine.TopOrderedItems(orderRepo, topN: 2);
            Console.WriteLine("Top 2 Ordered Items:");
            foreach (var (itemName, count) in topItems)
            {
                Console.WriteLine($" - {itemName}: {count} ordered");
            }

            bool aliceCrossOrdered = engine.CustomerOrderedFromBothRestaurants(alice.Id, burgerJoint.Id, pizzaPlace.Id, orderRepo);
            Console.WriteLine($"Alice ordered from both Restaurant 101 & 102: {aliceCrossOrdered}");

            Console.WriteLine("\n=== Demo Complete ===");
        }
    }
