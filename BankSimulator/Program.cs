using System;


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
            int totalNumberOfCounters = 3, totalNumberOfCustomers = 10;
            ServiceTime bankWorkingHours, customerServiceTimeRange;
            bankWorkingHours.Start = new TimeSpan(7, 0, 0);
            bankWorkingHours.End = new TimeSpan(9, 0, 0);
            customerServiceTimeRange.Start = new TimeSpan(0, 3, 0);
            customerServiceTimeRange.End = new TimeSpan(0, 30, 0);

            Bank bank = new Bank(totalNumberOfCounters, totalNumberOfCustomers, bankWorkingHours, customerServiceTimeRange);

            bank.GenerateTestCustomers();
            bank.SortCustomersByArrivalTime();
            bank.StartSimulation();
            bank.PrintSimulationInfo();

            Console.ReadKey();
        }

    }
}