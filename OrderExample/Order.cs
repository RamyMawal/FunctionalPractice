using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalPractice.OrderExample;

public class Order
{
    public Order() { }

    public Order(Order R)
    {
        Id = R.Id;
        Price = R.Price;
        Type = R.Type;
        AlmostExpired = R.AlmostExpired;
        Discount = R.Discount;
    }

    public int Id { get; set; } = 0;
    public double Price { get; set; }
    public double PriceAfterDiscount => Price - (double)Discount;
    public OrderType Type { get; set; } = OrderType.Others;
    public bool AlmostExpired { get; set; }
    public decimal Discount { get; set; } = 0;
}
