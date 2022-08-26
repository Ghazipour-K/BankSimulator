using System;
using System.Collections.Generic;

namespace BankSimulator
{
    class Counter
    {
        private Queue<Customer> customerQueue = new Queue<Customer>();
        private TimeSpan nextAvailableTime = default;
        private TimeSpan firstAvailableTime = default;
        public TimeSpan TotalServiceTime { get; set; }
        public TimeSpan TotalRestTime { get; set; }
        private int totalNumberOfServedCustomers = 0;
        public int QueueLength
        {
            get { return customerQueue.Count; }
        }

        public void PrintInfo()
        {
            Console.WriteLine("NumberOfCustomers: {0} - ServiceTime: {1} - RestTime: {2}", totalNumberOfServedCustomers, TotalServiceTime, TotalRestTime);
        }

        public Counter(ServiceTime bankWorkingTime)
        {
            TotalRestTime = bankWorkingTime.End - bankWorkingTime.Start;
            firstAvailableTime = bankWorkingTime.Start;
            nextAvailableTime = bankWorkingTime.Start;
        }

        public void Update(TimeSpan clock)
        {
            if (QueueLength > 0) ServeNextCustomer(clock);
        }

        public int GetTotalNumberOfServedCustomers()
        {
            return totalNumberOfServedCustomers;
        }

        public void Add(Customer customer)
        {
            customerQueue.Enqueue(customer);
            CalculateStatistics(customer);
        }

        private void CalculateStatistics(Customer customer)
        {
            totalNumberOfServedCustomers++;
            nextAvailableTime = (customer.ArrivalTime >= nextAvailableTime) ? (customer.ArrivalTime + customer.ServiceTimeInMinutes) : (nextAvailableTime + customer.ServiceTimeInMinutes);
            TotalServiceTime += customer.ServiceTimeInMinutes;
            TotalRestTime -= TotalServiceTime;
        }

        public void ServeNextCustomer(TimeSpan clock)
        {
            while (QueueLength > 0 && clock >= nextAvailableTime)
            {
                Customer nextCustomer = customerQueue.Peek();
                if (clock >= nextCustomer.ArrivalTime + nextCustomer.ServiceTimeInMinutes)
                {
                    Customer customer = customerQueue.Dequeue();
                    customer.ServiceStartTime = (customer.ArrivalTime >= firstAvailableTime) ? customer.ArrivalTime : firstAvailableTime;
                    firstAvailableTime= (customer.ArrivalTime >= firstAvailableTime) ? (customer.ArrivalTime + customer.ServiceTimeInMinutes) : (firstAvailableTime + customer.ServiceTimeInMinutes);
                    customer.ExitTime = customer.ServiceStartTime + customer.ServiceTimeInMinutes;
                    customer.WaitingTime = customer.ServiceStartTime.Subtract(customer.ArrivalTime);
                    customer.GotServiceInLegalTime = (customer.ExitTime <= new TimeSpan(10, 0, 0));
                }
            }
        }
    }
}
