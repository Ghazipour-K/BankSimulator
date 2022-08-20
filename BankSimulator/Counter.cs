using System;
using System.Collections.Generic;

namespace BankSimulator
{
     class Counter
    {
        private readonly Queue<Customer> customers = new Queue<Customer>();
        private int nextAvailableTime = 0;
        public int TotalServiceTime { get; set; }
        public int TotalRestTime { get; set; }
        public int TotalNumberOfServedCustomers { get; set; }
        public int QueueLength
        {
            get { return customers.Count; }
        }


        public Counter() { TotalServiceTime = TotalRestTime = TotalNumberOfServedCustomers = 0; }

        public void Add(Customer customer)
        {
            customers.Enqueue(customer);
            TotalNumberOfServedCustomers++;
        }

        public void StartServing()
        {
            nextAvailableTime = customers.Peek().ArrivalTime;

            while (customers.Count > 0)
            {
                var item = customers.Dequeue();
                TotalServiceTime += item.ServiceTime;
                item.ServiceStartTime = Math.Max(item.ArrivalTime, nextAvailableTime);
                nextAvailableTime = item.ServiceStartTime + item.ServiceTime;//must test using Gant diagram
                //if(item.ArrivalTime<)
            }
            //Code to indicate Queue statistics goes here...
            //TotalRestTime = TotalWorkTimeInMinute - TotalServiceTime;
        }


    }
}
