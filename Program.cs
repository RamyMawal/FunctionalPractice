// See https://aka.ms/new-console-template for more information

namespace FunctionalPractice
{
    class Program
    {
        public static void Main()
        {
            List<Order> OrdersToProcess = GetOrders();

            foreach (var order in OrdersToProcess)
            {
                Console.WriteLine($"Id: {order.Id}");
                Console.WriteLine($"Price: {order.Price}");
                Console.WriteLine($"Almost Expired: {order.AlmostExpired}");
                Console.WriteLine($"Type: {order.Type}");

            }
            Console.ReadLine();


            var OrdersWithDiscount = OrdersToProcess.Select(x => GetOrderWithDiscount(x, GetDiscountRules));

            Console.WriteLine("Processing...");
            Console.WriteLine();
            
            foreach (var order in OrdersWithDiscount)
            {
                Console.WriteLine($"Id: {order.Id}");
                Console.WriteLine($"Price: {order.Price}");
                Console.WriteLine($"Discount: {order.Discount}");
                Console.WriteLine($"Price After Discount: {order.PriceAfterDiscount}");
                Console.WriteLine();
            }

            Console.ReadLine();
        }

        public static Order GetOrderWithDiscount(Order order,
            Func<List<(Func<Order, bool> QualifyingCondition, Func<Order, decimal> GetDiscount)>> GetRules)
        {
            var Rules = GetRules();

            var discountList = Rules
                .Where((r) => r.QualifyingCondition(order))
                .Select(r => r.GetDiscount(order))
                .OrderBy(o => o);

            decimal discount = discountList.Count() == 0 ? 0 :
                discountList
                .Take(3)
                .Average();

            var newOrder = new Order(order);

            newOrder.Discount = discount;
            
            return newOrder;
        }

        public static List<(Func<Order, bool> QualifyingCondition, Func<Order,decimal> GetDiscount)> GetDiscountRules()
        {
            return new()
            {
                (IsExpireDiscountQualified, ExpireDiscount),
                (IsFoodDiscountQualified, FoodDiscount),
                (IsUtilityDiscountQualified, UtilityDiscount),
            };
        }

        #region Rules

        public static bool IsExpireDiscountQualified(Order order)
        {
            return order.AlmostExpired;
        }
        public static decimal ExpireDiscount(Order order)
        {
            return (decimal) order.Price * 0.2m;
        }
        public static bool IsFoodDiscountQualified(Order order)
        {
            return order.Type switch
            {
                OrderType.Food => true,
                OrderType.Beverage => true,
                _ => false
            };
        }
        public static decimal FoodDiscount(Order order)
        {
            return (decimal) order.Price * 0.1m;
        }
        public static bool IsUtilityDiscountQualified(Order order)
        {
            return order.Type switch
            {
                OrderType.Materials => true,
                OrderType.Cards => true,
                _ => false,
            };
        }
        public static decimal UtilityDiscount(Order order)
        {
            return (decimal) order.Price * 0.02m;
        }
        #endregion

        #region Order Generator
        public static List<Order> GetOrders()
        {
            List<Order> orders = new();

            var randomizer = new Random();

            for (int i = 0; i < 5; i++)
            {
                var order = new Order()
                {
                    Id = i,
                    Price = randomizer.NextInt64(0, 200),
                    Type = (OrderType)randomizer.NextInt64(0, 5),
                    AlmostExpired = true
                };
                orders.Add(order);
            }

            for (int i = 5; i < 10; i++)
            {
                var order = new Order()
                {
                    Id = i,
                    Price = randomizer.NextInt64(0, 150),
                    Type = (OrderType)randomizer.NextInt64(0, 5),
                    AlmostExpired = false
                };
                orders.Add(order);
            }

            return orders;
        }

        #endregion
    }
}

