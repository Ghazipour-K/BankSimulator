using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSimulator
{
    internal class Program
    {

        //private static void GetSimulationParametters(out ServiceTime bankWorkingHours, out ServiceTime customerServiceTime, out int totalNumberOfCounters, out int totalNumberOfCustomers)
        //{
        //    Console.Write("Enter bank working hours range: ");
        //    bankWorkingHours.Start = int.Parse(Console.ReadLine());
        //    bankWorkingHours.End = int.Parse(Console.ReadLine());

        //    Console.Write("Enter total number of counters: ");
        //    totalNumberOfCounters = int.Parse(Console.ReadLine());

        //    Console.Write("Enter total number of customer: ");
        //    totalNumberOfCustomers = int.Parse(Console.ReadLine());

        //    Console.Write("Enter customer service time range  in minutes: ");
        //    customerServiceTime.Start = int.Parse(Console.ReadLine());
        //    customerServiceTime.End = int.Parse(Console.ReadLine());
        //}



        static void Main(string[] args)
        {
            int totalNumberOfCounters = 5, totalNumberOfCustomers = 20;
            ServiceTime bankWorkingHours, customerServiceTime;
            bankWorkingHours.Start = 7;
            bankWorkingHours.End = 13;
            customerServiceTime.Start = 3;
            customerServiceTime.End = 30;

            Bank bank = new Bank(totalNumberOfCounters, totalNumberOfCustomers, bankWorkingHours, customerServiceTime);

            bank.GenerateTestCustomers();
            bank.SortCustomersByArrivalTime();
            bank.StartSimulation();
            bank.PrintSimulationInfo();

            Console.ReadKey();
        }

    }
}