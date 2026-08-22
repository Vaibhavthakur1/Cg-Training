using System;
using System.Collections.Generic;
using System.Linq;

namespace InsightDesk
{
    #region Domain Models & DTOs

    public class SaleLineItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string StaffName { get; set; }
        public string StoreLocation { get; set; }
        public DateTime SoldAt { get; set; }
        public decimal LineTotal => UnitPrice * Quantity;
        public List<string> AppliedPromotionCodes { get; set; } = new();

        public SaleLineItem(int id, string productName, string category, decimal unitPrice, int quantity,
            string staffName, string storeLocation, DateTime soldAt, List<string> appliedPromotions = null)
        {
            Id = id;
            ProductName = productName;
            Category = category;
            UnitPrice = unitPrice;
            Quantity = quantity;
            StaffName = staffName;
            StoreLocation = storeLocation;
            SoldAt = soldAt;
            AppliedPromotionCodes = appliedPromotions ?? new List<string>();
        }
    }

    public abstract class Promotion
    {
        public string Code { get; set; }

        protected Promotion(string code)
        {
            Code = code;
        }
    }

    public class PercentOffPromotion : Promotion
    {
        public double PercentOff { get; set; }

        public PercentOffPromotion(string code, double percentOff) : base(code)
        {
            PercentOff = percentOff;
        }
    }

    public class FlatAmountPromotion : Promotion
    {
        public decimal AmountOff { get; set; }

        public FlatAmountPromotion(string code, decimal amountOff) : base(code)
        {
            AmountOff = amountOff;
        }
    }

    public class BuyOneGetOnePromotion : Promotion
    {
        public BuyOneGetOnePromotion(string code) : base(code) { }
    }

    public class ProductSalesSummary
    {
        public string ProductName { get; set; }
        public int TotalQuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }

        public override bool Equals(object obj) =>
            obj is ProductSalesSummary other &&
            ProductName == other.ProductName &&
            TotalQuantitySold == other.TotalQuantitySold &&
            TotalRevenue == other.TotalRevenue;

        public override int GetHashCode() =>
            HashCode.Combine(ProductName, TotalQuantitySold, TotalRevenue);
    }

    public class CategoryRevenueSummary
    {
        public string Category { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalUnitsSold { get; set; }

        public override bool Equals(object obj) =>
            obj is CategoryRevenueSummary other &&
            Category == other.Category &&
            TotalRevenue == other.TotalRevenue &&
            TotalUnitsSold == other.TotalUnitsSold;

        public override int GetHashCode() =>
            HashCode.Combine(Category, TotalRevenue, TotalUnitsSold);
    }

    public class StaffPerformanceSummary
    {
        public string StaffName { get; set; }
        public int SalesCount { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageSaleValue { get; set; }
    }

    public class HourlyTrendSummary
    {
        public int Hour { get; set; }
        public string TimeWindow => $"{Hour:D2}:00 - {Hour:D2}:59";
        public int SalesCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class StoreComparisonSummary
    {
        public string StoreLocation { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalItemsSold { get; set; }
        public string TopCategoryName { get; set; }
        public decimal TopCategoryRevenue { get; set; }
    }

    public class PromoUsageSummary
    {
        public string PromotionCode { get; set; }
        public int UsageCount { get; set; }
    }

    #endregion

    #region Analytics Engine

    public class SalesAnalyticsEngine
    {
        private readonly List<SaleLineItem> _sales;
        private readonly List<Promotion> _promotions;

        public SalesAnalyticsEngine(List<SaleLineItem> sales, List<Promotion> promotions)
        {
            _sales = sales ?? new List<SaleLineItem>();
            _promotions = promotions ?? new List<Promotion>();
        }

        /// <summary>
        /// 1. TopSellingProducts: Returns the top N products ranked by total volume sold descending.
        /// Syntax: Method Syntax (clean Take/OrderBy chains). Materialized via ToList().
        /// </summary>
        public List<ProductSalesSummary> TopSellingProducts(int topN)
        {
            if (topN <= 0) return new List<ProductSalesSummary>();

            return _sales
                .GroupBy(s => s.ProductName)
                .Select(g => new ProductSalesSummary
                {
                    ProductName = g.Key,
                    TotalQuantitySold = g.Sum(s => s.Quantity),
                    TotalRevenue = g.Sum(s => s.LineTotal)
                })
                .OrderByDescending(p => p.TotalQuantitySold)
                .ThenBy(p => p.ProductName)
                .Take(topN)
                .ToList();
        }

        /// <summary>
        /// 2. RevenueByCategory: Aggregates revenue across product categories.
        /// Syntax: Query Syntax (leverages 'into' group continuation naturally). Deferred execution.
        /// </summary>
        public IEnumerable<CategoryRevenueSummary> RevenueByCategory()
        {
            return from s in _sales
                   group s by s.Category into catGroup
                   let totalRev = catGroup.Sum(item => item.LineTotal)
                   let totalUnits = catGroup.Sum(item => item.Quantity)
                   orderby totalRev descending
                   select new CategoryRevenueSummary
                   {
                       Category = catGroup.Key,
                       TotalRevenue = totalRev,
                       TotalUnitsSold = totalUnits
                   };
        }

        /// <summary>
        /// Method-syntax equivalent of RevenueByCategory for §3 Equivalence Verification.
        /// </summary>
        public IEnumerable<CategoryRevenueSummary> RevenueByCategoryMethodSyntax()
        {
            return _sales
                .GroupBy(s => s.Category)
                .Select(catGroup => new CategoryRevenueSummary
                {
                    Category = catGroup.Key,
                    TotalRevenue = catGroup.Sum(item => item.LineTotal),
                    TotalUnitsSold = catGroup.Sum(item => item.Quantity)
                })
                .OrderByDescending(c => c.TotalRevenue);
        }

        /// <summary>
        /// 3. StaffPerformanceReport: Aggregates employee metrics sorted by revenue DESC then StaffName ASC.
        /// Syntax: Method Syntax (explicit multi-key ordering via OrderByDescending + ThenBy). Materialized.
        /// </summary>
        public List<StaffPerformanceSummary> StaffPerformanceReport()
        {
            return _sales
                .GroupBy(s => s.StaffName)
                .Select(g => new StaffPerformanceSummary
                {
                    StaffName = g.Key,
                    SalesCount = g.Count(),
                    TotalRevenue = g.Sum(s => s.LineTotal),
                    AverageSaleValue = g.Average(s => s.LineTotal)
                })
                .OrderByDescending(sp => sp.TotalRevenue)
                .ThenBy(sp => sp.StaffName)
                .ToList();
        }

        /// 4. HourlySalesTrend: Analyzes hourly transaction counts and revenue throughout the day.
        public IEnumerable<HourlyTrendSummary> HourlySalesTrend()
        {
            return from s in _sales
                   group s by s.SoldAt.Hour into hourGroup
                   orderby hourGroup.Key ascending
                   select new HourlyTrendSummary
                   {
                       Hour = hourGroup.Key,
                       SalesCount = hourGroup.Count(),
                       TotalRevenue = hourGroup.Sum(item => item.LineTotal)
                   };
        }

        /// 5. PercentOffPromotionsOver: Extracts and filters PercentOffPromotion instances safely.
        /// Syntax: Method Syntax with OfType<T>(). Materialized.
        public List<PercentOffPromotion> PercentOffPromotionsOver(double minPercent)
        {
            return _promotions
                .OfType<PercentOffPromotion>()
                .Where(p => p.PercentOff >= minPercent)
                .OrderByDescending(p => p.PercentOff)
                .ToList();
        }

        /// 6. LowPerformingCategories: Identifies categories generating less than a specified revenue threshold.
        /// Syntax: Query Syntax (demonstrates group...by...into with query continuation and filtering). Materialized.
        public List<CategoryRevenueSummary> LowPerformingCategories(decimal revenueThreshold)
        {
            return (from s in _sales
                    group s by s.Category into catGroup
                    let totalRev = catGroup.Sum(item => item.LineTotal)
                    where totalRev < revenueThreshold
                    orderby totalRev ascending
                    select new CategoryRevenueSummary
                    {
                        Category = catGroup.Key,
                        TotalRevenue = totalRev,
                        TotalUnitsSold = catGroup.Sum(item => item.Quantity)
                    }).ToList();
        }

        /// 7. StoreComparisonReport: Aggregates location statistics and identifies top-performing category per store.
        /// Syntax: Method Syntax (nested group projections). Materialized.
        public List<StoreComparisonSummary> StoreComparisonReport()
        {
            return _sales
                .GroupBy(s => s.StoreLocation)
                .Select(storeGroup =>
                {
                    var topCategory = storeGroup
                        .GroupBy(s => s.Category)
                        .Select(cg => new { Category = cg.Key, Revenue = cg.Sum(x => x.LineTotal) })
                        .OrderByDescending(cg => cg.Revenue)
                        .FirstOrDefault();

                    return new StoreComparisonSummary
                    {
                        StoreLocation = storeGroup.Key,
                        TotalRevenue = storeGroup.Sum(s => s.LineTotal),
                        TotalItemsSold = storeGroup.Sum(s => s.Quantity),
                        TopCategoryName = topCategory?.Category ?? "N/A",
                        TopCategoryRevenue = topCategory?.Revenue ?? 0m
                    };
                })
                .OrderByDescending(sc => sc.TotalRevenue)
                .ToList();
        }

        /// 8. DeferredVsSnapshotDemo: Demonstrates live evaluation of deferred queries vs static snapshots.
        public void DeferredVsSnapshotDemo()
        {
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine("DEMO: Live Deferred Execution vs. Immediate .ToList() Snapshot");
            Console.WriteLine("----------------------------------------------------------------------------------");

            var testList = new List<SaleLineItem>(_sales.Take(5));

            // 1. Deferred query definition (no materialization)
            var deferredQuery = testList.Where(s => s.UnitPrice > 100m);

            // 2. Snapshot materialization
            var snapshotList = testList.Where(s => s.UnitPrice > 100m).ToList();

            Console.WriteLine($"[Initial State] Items priced > 100 in Deferred Query: {deferredQuery.Count()}");
            Console.WriteLine($"[Initial State] Items priced > 100 in Snapshot List : {snapshotList.Count}");

            // 3. Mutate source collection
            var newItem = new SaleLineItem(999, "High-End Server Rack", "Enterprise", 5000m, 1, "Admin", "Downtown", DateTime.Now);
            testList.Add(newItem);
            Console.WriteLine($"\n--> Added new item '{newItem.ProductName}' priced at Rs.{newItem.UnitPrice} to source list.\n");

            // 4. Re-evaluate both
            Console.WriteLine($"[Post-Mutation] Items in Deferred Query: {deferredQuery.Count()} (Picked up live change!)");
            Console.WriteLine($"[Post-Mutation] Items in Snapshot List : {snapshotList.Count} (Isolated from source mutation)");
            Console.WriteLine("----------------------------------------------------------------------------------\n");
        }

        /// Demonstrates buggy sort chaining (.OrderBy().OrderBy()) vs fixed (.OrderBy().ThenBy()).
        public void BrokenStaffSort()
        {
            // Broken Sort: Secondary .OrderBy() overrides the primary sort key
            var buggy = _sales
                .GroupBy(s => s.StaffName)
                .Select(g => new StaffPerformanceSummary
                {
                    StaffName = g.Key,
                    SalesCount = g.Count(),
                    TotalRevenue = g.Sum(s => s.LineTotal),
                    AverageSaleValue = g.Average(s => s.LineTotal)
                })
                .OrderByDescending(sp => sp.TotalRevenue)
                .OrderBy(sp => sp.StaffName) // Overwrites total revenue ordering
                .ToList();

            var fixedSort = StaffPerformanceReport();

            Console.WriteLine("==========================================================================================");
            Console.WriteLine("BUGGY SORT (.OrderBy().OrderBy())             | FIXED SORT (.OrderBy().ThenBy())");
            Console.WriteLine("==========================================================================================");

            for (int i = 0; i < buggy.Count; i++)
            {
                var b = buggy[i];
                var f = fixedSort[i];
                Console.WriteLine($"{b.StaffName,-10} Rev: Rs.{b.TotalRevenue,9:F2} | {f.StaffName,-10} Rev: Rs.{f.TotalRevenue,9:F2}");
            }
            Console.WriteLine("\n[Explanation] Calling .OrderBy() sequentially replaces the root comparer with StaffName ASC, destroying the TotalRevenue sort.");
            Console.WriteLine("==========================================================================================\n");
        }

        /// Stretch Goal 1: Dynamic Predicate Composition using Aggregate.
        public List<SaleLineItem> FilterSales(params Func<SaleLineItem, bool>[] predicates)
        {
            if (predicates == null || predicates.Length == 0) return _sales.ToList();

            return predicates
                .Aggregate(_sales.AsEnumerable(), (current, predicate) => current.Where(predicate))
                .ToList();
        }

        /// Stretch Goal 3: Flatten promotion usage using SelectMany.;
        public List<PromoUsageSummary> PromotionUsageReport()
        {
            return _sales
                .SelectMany(s => s.AppliedPromotionCodes)
                .GroupBy(code => code)
                .Select(g => new PromoUsageSummary
                {
                    PromotionCode = g.Key,
                    UsageCount = g.Count()
                })
                .OrderByDescending(p => p.UsageCount)
                .ToList();
        }
    }

    #endregion

    public class Program
    {
        public static void Main()
        {
            // -------------------------------------------------------------
            // 1. Data Seeding (40+ Line Items, 6+ Promotions)
            // -------------------------------------------------------------
            var seedDate = new DateTime(2026, 8, 22);

            var promotions = new List<Promotion>
            {
                new PercentOffPromotion("SUMMER10", 10.0),
                new PercentOffPromotion("MEGA25", 25.0),
                new PercentOffPromotion("VIP50", 50.0),
                new FlatAmountPromotion("FLAT100", 100.0m),
                new FlatAmountPromotion("FLAT500", 500.0m),
                new BuyOneGetOnePromotion("BOGO_ACC")
            };

            var sales = new List<SaleLineItem>
            {
                // Electronics (Store: Downtown & Uptown | Staff: Alice, Bob, Charlie)
                new(1, "Wireless Mouse", "Electronics", 799m, 2, "Alice", "Downtown", seedDate.AddHours(9).AddMinutes(15), new() { "SUMMER10" }),
                new(2, "USB-C Cable", "Electronics", 299m, 5, "Bob", "Downtown", seedDate.AddHours(9).AddMinutes(40)),
                new(3, "Mechanical Keyboard", "Electronics", 2499m, 1, "Charlie", "Uptown", seedDate.AddHours(10).AddMinutes(5), new() { "MEGA25" }),
                new(4, "4K Monitor", "Electronics", 21999m, 1, "Alice", "Downtown", seedDate.AddHours(10).AddMinutes(30), new() { "FLAT500" }),
                new(5, "Noise Cancelling Headphones", "Electronics", 8999m, 1, "Bob", "Uptown", seedDate.AddHours(11).AddMinutes(10), new() { "VIP50" }),
                new(6, "USB-C Hub", "Electronics", 1499m, 3, "Charlie", "Downtown", seedDate.AddHours(11).AddMinutes(45)),
                new(7, "Bluetooth Speaker", "Electronics", 1999m, 2, "Alice", "Uptown", seedDate.AddHours(12).AddMinutes(20), new() { "SUMMER10" }),
                new(8, "Webcam 1080p", "Electronics", 3499m, 1, "Bob", "Downtown", seedDate.AddHours(13).AddMinutes(05)),
                new(9, "Wireless Mouse", "Electronics", 799m, 4, "Charlie", "Uptown", seedDate.AddHours(14).AddMinutes(12)),
                new(10, "HDMI Cable 2m", "Electronics", 199m, 10, "Alice", "Downtown", seedDate.AddHours(15).AddMinutes(50)),

                // Stationery
                new(11, "A5 Dotted Journal", "Stationery", 250m, 4, "Bob", "Downtown", seedDate.AddHours(9).AddMinutes(20)),
                new(12, "Gel Pen Multi-pack", "Stationery", 120m, 10, "Charlie", "Uptown", seedDate.AddHours(9).AddMinutes(55)),
                new(13, "Sticky Notes Pad", "Stationery", 45m, 15, "Alice", "Downtown", seedDate.AddHours(10).AddMinutes(15)),
                new(14, "Desk Organizer", "Stationery", 650m, 2, "Bob", "Uptown", seedDate.AddHours(11).AddMinutes(00)),
                new(15, "Permanent Markers Set", "Stationery", 180m, 5, "Charlie", "Downtown", seedDate.AddHours(12).AddMinutes(30)),
                new(16, "Highlighter Pack", "Stationery", 90m, 8, "Alice", "Uptown", seedDate.AddHours(13).AddMinutes(40)),
                new(17, "Fountain Pen", "Stationery", 1200m, 1, "Bob", "Downtown", seedDate.AddHours(14).AddMinutes(25), new() { "FLAT100" }),
                new(18, "Correction Tape", "Stationery", 55m, 6, "Charlie", "Uptown", seedDate.AddHours(15).AddMinutes(10)),
                new(19, "A4 Copy Paper Ream", "Stationery", 320m, 3, "Alice", "Downtown", seedDate.AddHours(16).AddMinutes(05)),
                new(20, "Metal Ruler 30cm", "Stationery", 40m, 12, "Bob", "Uptown", seedDate.AddHours(17).AddMinutes(15)),

                // Accessories
                new(21, "Desk Mat XXL", "Accessories", 499m, 3, "Charlie", "Downtown", seedDate.AddHours(9).AddMinutes(35), new() { "BOGO_ACC" }),
                new(22, "Ergonomic Wrist Rest", "Accessories", 350m, 2, "Alice", "Uptown", seedDate.AddHours(10).AddMinutes(45)),
                new(23, "Laptop Aluminum Stand", "Accessories", 1299m, 2, "Bob", "Downtown", seedDate.AddHours(11).AddMinutes(25)),
                new(24, "Cable Management Clips", "Accessories", 99m, 20, "Charlie", "Uptown", seedDate.AddHours(12).AddMinutes(15)),
                new(25, "Screen Cleaning Kit", "Accessories", 150m, 4, "Alice", "Downtown", seedDate.AddHours(13).AddMinutes(10)),
                new(26, "Monitor Light Bar", "Accessories", 2199m, 1, "Bob", "Uptown", seedDate.AddHours(14).AddMinutes(50), new() { "MEGA25" }),
                new(27, "Under-Desk Headphone Mount", "Accessories", 299m, 3, "Charlie", "Downtown", seedDate.AddHours(15).AddMinutes(30)),
                new(28, "Mouse Bungee", "Accessories", 399m, 1, "Alice", "Uptown", seedDate.AddHours(16).AddMinutes(20)),
                new(29, "Foot Rest Ergonomic", "Accessories", 1599m, 1, "Bob", "Downtown", seedDate.AddHours(16).AddMinutes(45)),
                new(30, "Desk Mat XXL", "Accessories", 499m, 2, "Charlie", "Uptown", seedDate.AddHours(17).AddMinutes(30)),

                // Furniture
                new(31, "Ergonomic Mesh Chair", "Furniture", 14999m, 1, "Alice", "Downtown", seedDate.AddHours(10).AddMinutes(00), new() { "FLAT500" }),
                new(32, "Motorized Standing Desk", "Furniture", 28999m, 1, "Bob", "Uptown", seedDate.AddHours(11).AddMinutes(30)),
                new(33, "Filing Cabinet 3-Drawer", "Furniture", 4500m, 1, "Charlie", "Downtown", seedDate.AddHours(13).AddMinutes(15)),
                new(34, "Office Wastebin Metal", "Furniture", 350m, 4, "Alice", "Uptown", seedDate.AddHours(14).AddMinutes(00)),
                new(35, "Desk Bookshelf Hutch", "Furniture", 2200m, 1, "Bob", "Downtown", seedDate.AddHours(15).AddMinutes(40)),
                new(36, "Clamp-on Power Strip", "Furniture", 850m, 2, "Charlie", "Uptown", seedDate.AddHours(16).AddMinutes(10)),
                new(37, "Monitor Arm Dual", "Furniture", 3200m, 1, "Alice", "Downtown", seedDate.AddHours(16).AddMinutes(55)),
                new(38, "Anti-Fatigue Standing Mat", "Furniture", 1100m, 2, "Bob", "Uptown", seedDate.AddHours(17).AddMinutes(05)),
                new(39, "Office Footstool", "Furniture", 750m, 1, "Charlie", "Downtown", seedDate.AddHours(17).AddMinutes(45)),
                new(40, "Mesh Chair Lumbar Cushion", "Furniture", 650m, 3, "Alice", "Downtown", seedDate.AddHours(17).AddMinutes(55))
            };

            var engine = new SalesAnalyticsEngine(sales, promotions);

            // -------------------------------------------------------------
            // 2. Execute & Render All 8 Core Reports
            // -------------------------------------------------------------
            Console.WriteLine("==========================================================================================");
            Console.WriteLine("INSIGHTDESK RETAIL SALES ANALYTICS ENGINE");
            Console.WriteLine("==========================================================================================\n");

            // Report 1
            Console.WriteLine("--- 1. TOP 5 SELLING PRODUCTS (Method Syntax, Materialized) ---");
            foreach (var item in engine.TopSellingProducts(5))
            {
                Console.WriteLine($"Product: {item.ProductName,-25} | Units Sold: {item.TotalQuantitySold,3} | Rev: Rs.{item.TotalRevenue,9:F2}");
            }
            Console.WriteLine();

            // Store Deferred Queries for Requirement §5
            var deferredCategoryRevenue = engine.RevenueByCategory();
            var deferredHourlyTrend = engine.HourlySalesTrend();

            // Intermediate computation executes here before deferred evaluation
            var staffReport = engine.StaffPerformanceReport();

            // Report 2 (Enumerating Deferred Query)
            Console.WriteLine("--- 2. REVENUE BY CATEGORY (Query Syntax, Deferred Execution) ---");
            foreach (var cat in deferredCategoryRevenue)
            {
                Console.WriteLine($"Category: {cat.Category,-15} | Units: {cat.TotalUnitsSold,3} | Revenue: Rs.{cat.TotalRevenue,9:F2}");
            }
            Console.WriteLine();

            // Report 3
            Console.WriteLine("--- 3. STAFF PERFORMANCE REPORT (Method Syntax, Materialized) ---");
            foreach (var staff in staffReport)
            {
                Console.WriteLine($"Staff: {staff.StaffName,-10} | Sales: {staff.SalesCount,2} | Total: Rs.{staff.TotalRevenue,9:F2} | Avg: Rs.{staff.AverageSaleValue,7:F2}");
            }
            Console.WriteLine();

            // Report 4 (Enumerating Deferred Query)
            Console.WriteLine("--- 4. HOURLY SALES TREND (Query Syntax, Deferred Execution) ---");
            foreach (var hour in deferredHourlyTrend)
            {
                Console.WriteLine($"Window: {hour.TimeWindow} | Transactions: {hour.SalesCount,2} | Revenue: Rs.{hour.TotalRevenue,9:F2}");
            }
            Console.WriteLine();

            // Report 5
            Console.WriteLine("--- 5. PERCENT-OFF PROMOTIONS (>= 20%) (Method Syntax with OfType<T>) ---");
            foreach (var promo in engine.PercentOffPromotionsOver(20.0))
            {
                Console.WriteLine($"Promo Code: {promo.Code,-12} | Discount: {promo.PercentOff}%");
            }
            Console.WriteLine();

            // Report 6
            Console.WriteLine("--- 6. LOW PERFORMING CATEGORIES (< Rs. 15,000) (Query Syntax + into) ---");
            foreach (var low in engine.LowPerformingCategories(15000m))
            {
                Console.WriteLine($"Category: {low.Category,-15} | Revenue: Rs.{low.TotalRevenue,9:F2}");
            }
            Console.WriteLine();

            // Report 7
            Console.WriteLine("--- 7. STORE COMPARISON REPORT (Method Syntax, Nested Groupings) ---");
            foreach (var store in engine.StoreComparisonReport())
            {
                Console.WriteLine($"Store: {store.StoreLocation,-10} | Revenue: Rs.{store.TotalRevenue,9:F2} | Items: {store.TotalItemsSold,3} | Top Category: {store.TopCategoryName} (Rs.{store.TopCategoryRevenue:F2})");
            }
            Console.WriteLine();

            // Report 8
            Console.WriteLine("--- 8. DEFERRED VS SNAPSHOT LIVE PROOF ---");
            engine.DeferredVsSnapshotDemo();

            // -------------------------------------------------------------
            // 3. Syntax Equivalence Check (§3)
            // -------------------------------------------------------------
            Console.WriteLine("--- 3. SYNTAX EQUIVALENCE VERIFICATION (RevenueByCategory) ---");
            var queryResult = engine.RevenueByCategory().ToList();
            var methodResult = engine.RevenueByCategoryMethodSyntax().ToList();

            bool isEquivalent = queryResult.SequenceEqual(methodResult);
            Console.WriteLine($"Query Syntax count: {queryResult.Count}, Method Syntax count: {methodResult.Count}");
            Console.WriteLine($"Both implementations match exactly: {isEquivalent}\n");

            // 4. OrderBy / ThenBy Correctness Check (§4)
            Console.WriteLine("--- 4. ORDERBY / THENBY BEHAVIOR CHECK ---");
            engine.BrokenStaffSort();

            // 5. Edge Case Graceful Degradation Check
            Console.WriteLine("--- 5. EDGE CASE ROBUSTNESS CHECKS ---");
            var largeTopN = engine.TopSellingProducts(500); // More than existing items
            Console.WriteLine($"Requested Top 500 products (Dataset size ~25 unique): Returned {largeTopN.Count} items safely.");

            var emptyPromos = engine.PercentOffPromotionsOver(999.0); // No matching percent
            Console.WriteLine($"Promotions with >= 999% discount: Returned {emptyPromos.Count} items safely.");

            var lowCatNone = engine.LowPerformingCategories(1.0m); // Threshold below minimum
            Console.WriteLine($"Categories with revenue < Rs.1: Returned {lowCatNone.Count} items safely.\n");

            // 6. Stretch Goals: Ad-hoc Filtering & Promo Usage
            Console.WriteLine("--- BONUS: AD-HOC QUERY COMPOSER (Aggregate .Where) ---");
            var adHocResults = engine.FilterSales(
                s => s.StoreLocation == "Downtown",
                s => s.Category == "Electronics",
                s => s.UnitPrice > 500m
            );
            Console.WriteLine($"Downtown Electronics priced > 500: Found {adHocResults.Count} records.");

            Console.WriteLine("\n--- BONUS: PROMOTION USAGE REPORT (SelectMany Flattening) ---");
            foreach (var p in engine.PromotionUsageReport())
            {
                Console.WriteLine($"Promotion Code: {p.PromotionCode,-12} | Times Applied: {p.UsageCount}");
            }
        }
    }
}