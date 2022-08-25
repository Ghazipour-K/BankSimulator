using System;

namespace BankSimulator
{
    class Bank
    {
        private DateTime clock;
        private ServiceTime bankWorkingHours;
        private ServiceTime customerServiceTime;
        private Customer[] customers;
        private Counter[] counters;

        public Bank(int totalNumberOfCounters, int totalNumberOfCustomers, ServiceTime bankWorkingHours, ServiceTime customerServiceTime)
        {
            this.bankWorkingHours = bankWorkingHours;
            this.customerServiceTime = customerServiceTime;
            clock = bankWorkingHours.Start.ToDateTime();
            customers = new Customer[totalNumberOfCustomers];
            counters = new Counter[totalNumberOfCounters];

            GenerateCutomerObjects();
            GenerateCounterObjects();
        }

        private void ApplyClockToCounters(DateTime clock)
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
                if (counters[i].QueueLength > index) index = i;
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
                customers[i].ArrivalTime = new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    random.Next(bankWorkingHours.Start, bankWorkingHours.End),//Generating Hour
                    random.Next(60), 0);//Generating Minute
                customers[i].ServiceTimeInMinutes = random.Next(customerServiceTime.Start, customerServiceTime.End + 1);
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

        private void CalculateStatistics()
        {
            foreach (Counter counter in counters)
            {
                counter.TotalRestTime = (bankWorkingHours.End - bankWorkingHours.Start) * 60 - counter.TotalServiceTime;
            }
        }
        
        public void PrintSimulationInfo()
        {
            PrintCustomers();
            Console.WriteLine("-------------------------------------------------");
            CalculateStatistics();
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
                counters[i] = new Counter(bankWorkingHours.Start.ToDateTime());
            }
        }

    }
}
