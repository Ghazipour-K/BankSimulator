using System;
using System.Collections.Generic;

namespace BankSimulator
{
    class Counter
    {
        private readonly Queue<Customer> customers = new Queue<Customer>();
        private DateTime nextAvailableTime = default;
        public ServiceTime ServiceTime { get; set; } //Must be moved to Bank class
        public DateTime TotalServiceTime { get; set; }
        public DateTime TotalRestTime { get; set; }
        private int totalNumberOfServedCustomers = 0;
        public int QueueLength
        {
            get { return customers.Count; }
        }


        public Counter() { }

        public int GetTotalNumberOfServedCustomers()
        {
            return totalNumberOfServedCustomers;
        }

        public void Add(Customer customer)
        {
            customers.Enqueue(customer);
            totalNumberOfServedCustomers++;
        }

        public void StartServing(DateTime clock)
        {
            nextAvailableTime = customers.Peek().ArrivalTime;

            while (customers.Count > 0 && clock >= nextAvailableTime)
            {

                var item = customers.Dequeue();
                TimeSpan time = new TimeSpan();
                TotalServiceTime.AddMinutes(item.ServiceTimeInMinutes);
                ////TotalServiceTime += item.ServiceTime;
                item.ServiceStartTime = (item.ArrivalTime >= nextAvailableTime) ? item.ArrivalTime : nextAvailableTime; // Math.Max(item.ArrivalTime, nextAvailableTime);
                nextAvailableTime = item.ServiceStartTime.AddMinutes(item.ServiceTimeInMinutes);//must test using Gant diagram
                //if(item.ArrivalTime<)
            }
            //Code to indicate Queue statistics goes here...
            //TotalRestTime = TotalWorkTimeInMinute - TotalServiceTime;
        }


    }
}
