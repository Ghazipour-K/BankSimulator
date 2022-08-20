using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSimulator
{
    internal class Program
    {

        private static void GenerateTestCustomers(Customer[] customers, ServiceTime bankWorkingHours, ServiceTime customerServiceTime)
        {
            Random random = new Random();

            for (int i = 0; i < customers.Length; i++)
            {
                customers[i].ArrivalTime = random.Next(bankWorkingHours.Start, bankWorkingHours.End);
                customers[i].ServiceTime = random.Next(customerServiceTime.Start, customerServiceTime.End);
            }
        }

        private static void PrintCustomers(Customer[] customers)
        {
            foreach (var c in customers)
            {
                Console.WriteLine("Arrival: {0}   |   ServiceTime: {1}", c.ArrivalTime, c.ServiceTime);
            }
        }

        private static void SortCustomersByArrivalTime(Customer[] customers)
        {
            Array.Sort<Customer>(customers, (x, y) => x.ArrivalTime.CompareTo(y.ArrivalTime));
        }

        private static void GetSimulationParametters(out ServiceTime bankWorkingHours, out ServiceTime customerServiceTime, out int totalNumberOfCounters, out int totalNumberOfCustomers)
        {
            Console.Write("Enter bank working hours range: ");
            bankWorkingHours.Start = int.Parse(Console.ReadLine());
            bankWorkingHours.End = int.Parse(Console.ReadLine());

            Console.Write("Enter total number of counters: ");
            totalNumberOfCounters = int.Parse(Console.ReadLine());

            Console.Write("Enter total number of customer: ");
            totalNumberOfCustomers = int.Parse(Console.ReadLine());

            Console.Write("Enter customer service time range  in minutes: ");
            customerServiceTime.Start = int.Parse(Console.ReadLine());
            customerServiceTime.End = int.Parse(Console.ReadLine());
        }

        private static void GenerateCutomerObjects(Customer[] customers)
        {
            for (int i = 0; i < customers.Length; i++)
            {
                customers[i] = new Customer();
            }
        }

        static void Main(string[] args)
        {
            ServiceTime bankWorkingHours, customerServiceTime;

            int totalNumberOfCounters, totalNumberOfCustomers;

            GetSimulationParametters(out bankWorkingHours, out customerServiceTime, out totalNumberOfCounters, out totalNumberOfCustomers);
            Customer[] customers = new Customer[totalNumberOfCustomers];

            GenerateCutomerObjects(customers);
            GenerateTestCustomers(customers, bankWorkingHours, customerServiceTime);
            SortCustomersByArrivalTime(customers);


            PrintCustomers(customers);

            Console.ReadKey();
        }

    }
}
