using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epam_test_evdochenko
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                Console.SetWindowSize(170, 50);

                List<Item> list = new List<Item>()
            {
                new Product(2, 21, 800 ),
                new Product(4, 10, 750, new PercentDiscount(5)),
                new Product(2, 2, 1000, new DeliveryDiscount(50)),
                new Product(10, 12, 1500, new BonusDiscount(100)),
                new Product(5, 18, 500),
                new Product(1, 10, 950, new PercentDiscount(15)),
                new Product(4, 25, 700, new PercentDiscount(25)),
                new Product(1, 4, 1100, new DeliveryDiscount(50)),
                new Product(15, 15, 600, new BonusDiscount(100)),
                new Product(10, 13, 400, new BonusDiscount(100)),
             };

                int k = 0;

                foreach (var item in list)
                {
                    Console.WriteLine("{0}", item);
                    if (item.DayOfMonth == 10)
                        k++;
                }

                Console.WriteLine();
                Console.WriteLine("There was {0} purchase(s) at 10th of this month", k);

                list.Sort();
                Console.WriteLine();
                Console.WriteLine("Sorting by date:");
                Console.WriteLine();

                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine();
                Console.WriteLine("Sorting by discount:");
                Console.WriteLine();

                var groupBy = list.GroupBy(x => x.Discount != null ? x.Discount.GetType().Name : "No discount").ToList();

                foreach (var v in groupBy)
                {
                    Console.WriteLine("{0} :", v.Key);

                    foreach (var item in v)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine();
                }


                Console.ReadKey();
            }
        }
    }
 }

