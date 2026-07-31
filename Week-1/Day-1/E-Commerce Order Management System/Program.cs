using System;

class Program
{

    static string[] orders =
    {
        "ORD1001|John Smith|Laptop|2|$1200|Delivered",
        "ORD1002|Alice Brown|Mobile|1|$800|Pending",
        "ORD1003|David Wilson|Keyboard|3|$150|Shipped",
        "ORD1004|Emma Davis|Monitor|2|$350|Delivered",
        "ORD1005|James Miller|Mouse|5|$50|Pending"
    };
    static void Main(string[] args)
    {

        //DisplayOrderDetails();

        //static void DisplayOrderDetails()
        //{
        //    foreach (string order in orders)
        //    {
        //        string[] data = order.Split('|');

        //        Console.WriteLine("Order Id: " + data[0]);
        //        Console.WriteLine("Name: " + data[1]);
        //        Console.WriteLine("Quantity: " + data[2]);
        //        Console.WriteLine("Order Status: " + data[3]);

        //    }
        //}

        //static void DisplayUpperCaseNames()
        //{
        //    foreach(string order in orders)
        //    {
        //        string[] data = order.Split('|');
        //        Console.WriteLine(data[1].ToUpper());
        //    }
        //}

        //DisplayUpperCaseNames();

        //static void DisplayInitials()
        //{
        //    foreach(string order in orders)
        //    {
        //        string[] data = order.Split('|');

        //        string[] name = data[1].Split(' ');

        //        string initials = "";

        //        foreach(string s in name)
        //        {
        //            initials += s.Substring(0, 1);
        //        }
        //        Console.WriteLine(data[1] + " : " + initials);


        //    }
        //}

        //DisplayInitials();



        //delivered order
        static void DisplayDeliveredOrders()
        {
            foreach(string order in orders)
            {
                string[] data = order.Split('|');
                if (data[5] == "Delivered")
                {
                    Console.WriteLine("OrderId: " + data[0]);
                    Console.WriteLine("Name: " + data[1]);
                }
            }
        }

        DisplayDeliveredOrders();

        //TotalOrder

        static void CountOrders()
        {
            //int count = 0;
            //foreach(string order in orders)
            //{
            //    count++;
            //}
            //Console.WriteLine("Total no of orders are: " + count);

            Console.WriteLine("Total Orders are: "+orders.Length);
        }

        CountOrders();

        static void SearchOrderById()
        {
            bool found = false;
            foreach (string order in orders)
            {
                string[] data = order.Split('|');
                if (data[0] == "ORD1003")
                {
                    Console.WriteLine(data[0]);
                    Console.WriteLine(data[1]);
                    Console.WriteLine(data[2]);
                    Console.WriteLine(data[3]);
                    Console.WriteLine(data[4]);
                    Console.WriteLine(data[5]);

                    found = true;
                    break;

                }
            }
            if (!found)
            {
                Console.WriteLine("Order not found");
            }
        }

        SearchOrderById();

        static void ExtractOrder()
        {
            foreach(string order in orders)
            {
                string[] data = order.Split('|');
                Console.WriteLine("Price: " + data[4]);


            }
        }

        ExtractOrder();


    }
}