using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epam_test_evdochenko
{
    abstract class Item : IComparable<Item>
    {
        public string Name { get; private set; }
        public byte Count { get; private set; }
        public byte DayOfMonth { get; private set; }
        public decimal Price { get; private set; }
        public decimal TotalPrice
        {
            get
            {
                if (Discount == null)
                    return Price * Count;
                else
                    return Discount.SetPrice(Price, Count);
            }
            private set { _ = Price * Count; }
        }

        public IDiscount Discount;

        protected Item(string name, byte count, byte date, decimal price)
        {
            Name = name;
            Count = count;
            DayOfMonth = date;
            Price = price;
        }

        protected Item(string name, byte count, byte date, decimal price, IDiscount discount)
        {
            Name = name;
            Count = count;
            DayOfMonth = date;
            Price = price;
            Discount = discount;
        }

        public void SetDiscount(IDiscount discount)
        {
            Discount = discount;
        }

        public int CompareTo(Item other)
        {
            return DayOfMonth.CompareTo(other.DayOfMonth);
        }


        public override string ToString()
        {
            return string.Format("Name: {0}|\t Count: {1}|\t Price: {2} BYR|\t Date of the month: {3}|\t Total Price: {4} ",
                Name, Count, Price, DayOfMonth, TotalPrice) + (Discount == null ? "" : Discount.ToString());
        }
    }

    class Product : Item
    {
        public Product(byte count, byte date, decimal price, IDiscount discount) : base("Product_1", count, date, price, discount)
        {
        }

        public Product(byte count, byte date, decimal price) : base("Product_1", count, date, price)
        {
        }
    }

    internal interface IDiscount
    {
        decimal SetPrice(decimal price, byte count);
    }

    class PercentDiscount : IDiscount
    {
        private readonly int Percent;

        public PercentDiscount(int percent)
        {
            Percent = percent;
        }

        public decimal SetPrice(decimal price, byte count)
        {
            return price * count * (1 - Percent / 100);
        }

        public override string ToString()
        {
            return string.Format("The discount is: {0}%", Percent);
        }
    }

    class DeliveryDiscount : IDiscount
    {
        private readonly int DeliveryCost;

        public DeliveryDiscount(int deliverycost)
        {
            DeliveryCost = deliverycost;
        }

        public decimal SetPrice(decimal price, byte count)
        {

            return (price * count) - DeliveryCost;
        }

        public override string ToString()
        {

            return string.Format("The discount for shipping is: {0} BYR ", DeliveryCost);
        }
    }

    class BonusDiscount : IDiscount
    {

        private readonly int Bonus;

        public BonusDiscount(int bonus)
        {
            Bonus = bonus;
        }

        public decimal SetPrice(decimal price, byte count)
        {

            return (price * count) - Bonus;
        }

        public override string ToString()
        {
            return string.Format("The discount is equal to the value of the bonus item: {0} BYR", Bonus);
        }
    }
}
