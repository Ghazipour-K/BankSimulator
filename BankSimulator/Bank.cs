using System;

namespace BankSimulator
{
    class Bank
    {
        private DateTime clock;
        private ServiceTime bankWorkingTime;
        private ServiceTime customerServiceTime;
        private Customer[] customers;
        private Counter[] counters;

        public Bank(int totalNumberOfCounters, int totalNumberOfCustomers, ServiceTime bankWorkingTime, ServiceTime customerServiceTime)
        {
            this.bankWorkingTime = bankWorkingTime;
            this.customerServiceTime = customerServiceTime;
            customers = new Customer[totalNumberOfCustomers];
            counters = new Counter[totalNumberOfCounters];

            GenerateCutomerObjects();
            GenerateCounterObjects();
        }


        public void UpdateCounters()
        {

        }

        /// <summary>
        ///Selects counter with minimum length
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
                    random.Next(bankWorkingTime.Start, bankWorkingTime.End),//Generating Hour
                    random.Next(60), 0);//Generating Minute
                customers[i].ServiceTimeInMinutes = random.Next(customerServiceTime.Start, customerServiceTime.End + 1);
            }
        }

        public void PrintCustomers()
        {
            foreach (Customer customer in customers)
            {
                Console.WriteLine("Arrival: {0}   |   ServiceTime: {1}", customer.ArrivalTime.ToShortTimeString(), customer.ServiceTimeInMinutes);
            }
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
                counters[i] = new Counter();
            }
        }

    }
}
