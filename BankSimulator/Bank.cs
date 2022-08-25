using System;
using System.Globalization;

namespace BankSimulator
{
    class Bank
    {
        private TimeSpan clock;
        private ServiceTime bankWorkingTime;
        private ServiceTime customerServiceTime;
        private Customer[] customers;
        private Counter[] counters;

        public Bank(int totalNumberOfCounters, int totalNumberOfCustomers, ServiceTime bankWorkingHours, ServiceTime customerServiceTime)
        {
            this.bankWorkingTime = bankWorkingHours;
            this.customerServiceTime = customerServiceTime;
            clock = bankWorkingHours.Start;
            customers = new Customer[totalNumberOfCustomers];
            counters = new Counter[totalNumberOfCounters];

            GenerateCutomerObjects();
            GenerateCounterObjects();
        }

        private void ApplyClockToCounters(TimeSpan clock)
        {
            foreach (Counter counter in counters)
            {
                counter.Update(clock);
            }
        }

        /// <summary>
        ///Selects counter with minimum Q length
        /// </summary>
        private int SelectNextCounter()
        {
            int index = 0;
            for (int i = 0; i < counters.Length; i++)
            {
                if (counters[i].QueueLength < index) index = i;
            }
            return index;
        }

        public void StartSimulation()
        {
            foreach (Customer customer in customers)
            {
                clock = customer.ArrivalTime;
                ApplyClockToCounters(clock);
                DispatchCustomer(customer);
            }
        }

        private void DispatchCustomer(Customer customer)
        {
            counters[SelectNextCounter()].Add(customer);
        }

        public void GenerateTestCustomers()
        {
            Random random = new Random();

            for (int i = 0; i < customers.Length; i++)
            {

                customers[i].ArrivalTime = new TimeSpan(
                    random.Next(bankWorkingTime.Start.Hours, bankWorkingTime.End.Hours),//Generating Hour
                    random.Next(60),//Generating Minute
                    0);
                //customers[i].ArrivalTime = DateTime.ParseExact(customers[i].ArrivalTime.ToString("yyyy/MM/dd HH:mm"));

                //customers[i].ArrivalTime = DateTime.ParseExact(customers[i].ArrivalTime.ToString("yyyy/MM/dd HH:mm"), "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
                //DateTime.TryParseExact(customers[i].ArrivalTime.ToString(), "yyyy:MM:dd:HH:mm",);
                customers[i].ServiceTimeInMinutes = new TimeSpan(0, random.Next(customerServiceTime.Start.Minutes, customerServiceTime.End.Minutes + 1), 0);
            }
        }

        private void PrintCustomers()
        {
            foreach (Customer customer in customers)
            {
                customer.PrintInfo();
            }
        }

        private void PrintCounters()
        {
            foreach (Counter counter in counters)
            {
                counter.PrintInfo();
            }
        }

        //private void CalculateStatistics()
        //{
        //    foreach (Counter counter in counters)
        //    {
        //        counter.TotalRestTime = (new TimeSpan(0, (bankWorkingTime.End - bankWorkingTime.Start).Hours * 60, 0)) - counter.TotalServiceTime;
        //    }
        //}
        
        public void PrintSimulationInfo()
        {
            PrintCustomers();
            Console.WriteLine("-------------------------------------------------");
            //CalculateStatistics();
            PrintCounters();
        }

        public void SortCustomersByArrivalTime()
        {
            Array.Sort<Customer>(customers, (x, y) => x.ArrivalTime.CompareTo(y.ArrivalTime));
        }

        private void GenerateCutomerObjects()
        {
            for (int i = 0; i < customers.Length; i++)
            {
                customers[i] = new Customer();
            }
        }

        private void GenerateCounterObjects()
        {
            for (int i = 0; i < counters.Length; i++)
            {
                counters[i] = new Counter(bankWorkingTime);
            }
        }

    }
}
