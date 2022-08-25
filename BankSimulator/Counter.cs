using System;
using System.Collections.Generic;

namespace BankSimulator
{
    class Counter
    {
        private Queue<Customer> customerQueue = new Queue<Customer>();
        private DateTime nextAvailableTime = default;
        public int TotalServiceTime { get; set; }
        public int TotalRestTime { get; set; }
        private int totalNumberOfServedCustomers = 0;
        public int QueueLength
        {
            get { return customerQueue.Count; }
        }

        public void PrintInfo()
        {
            Console.WriteLine("NumberOfCustomers: {0} - ServiceTime: {1} - RestTime: {2}", totalNumberOfServedCustomers, TotalServiceTime, TotalRestTime);
        }

        public Counter(DateTime bankStartWorkingTime)
        {
            nextAvailableTime = bankStartWorkingTime;
        }

        public void Update(DateTime clock)
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
            totalNumberOfServedCustomers++;
            nextAvailableTime.AddMinutes(customer.ServiceTimeInMinutes);
            TotalServiceTime += customer.ServiceTimeInMinutes;
        }

        public void ServeNextCustomer(DateTime clock)
        {

            while (QueueLength > 0 && clock >= nextAvailableTime)
            {
                Customer item = customerQueue.Dequeue();
                item.ServiceStartTime = (item.ArrivalTime >= nextAvailableTime) ? item.ArrivalTime : nextAvailableTime; // Math.Max(item.ArrivalTime, nextAvailableTime);
                item.ExitTime = item.ServiceStartTime.AddMinutes(item.ServiceTimeInMinutes);
                item.WaitingTime = item.ExitTime.Subtract(item.ArrivalTime);
                item.GotServiceInLegalTime = (item.ExitTime.Hour > 13) && (item.ExitTime.Minute > 0);
                //nextAvailableTime = item.ServiceStartTime.AddMinutes(item.ServiceTimeInMinutes);//must test using Gant diagram
                //if(item.ArrivalTime<)
            }
            //Code to indicate Queue statistics goes here...
            //TotalRestTime = TotalWorkTimeInMinute - TotalServiceTime;
        }


    }
}
