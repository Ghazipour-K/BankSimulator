using System;
using System.Collections.Generic;

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
            int min = int.MaxValue;
            List<int> list = new List<int>();
            
            //Find minimum length of all counters
            for (int i = 0; i < counters.Length; i++)
            {
                if (counters[i].QueueLength < min)
                {
                    min = counters[i].QueueLength;
                }
            }

            //Find counters with minimum 
            for (int i = 0; i < counters.Length; i++)
            {
                if (counters[i].QueueLength == min) { list.Add(i); }
            }

            //Select and return a random counter among counters with minimum length
            Random random = new Random();
            index = list[random.Next(list.Count)];
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
            int index = SelectNextCounter();
            customer.AssignedCounterNo = index;
            counters[index].Add(customer);
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

                customers[i].ServiceTime = new TimeSpan(0, random.Next(customerServiceTime.Start.Minutes, customerServiceTime.End.Minutes + 1), 0);
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
        
        public void PrintSimulationInfo()
        {
            PrintCustomers();
            Console.WriteLine("-------------------------------------------------");
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
