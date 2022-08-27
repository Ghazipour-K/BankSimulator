using System;
using System.Collections.Generic;

namespace BankSimulator
{
    class Counter
    {
        private Queue<Customer> customerQueue = new Queue<Customer>();
        private TimeSpan nextAvailableTime = default;
        //private TimeSpan firstAvailableTime = default;
        private TimeSpan totalServiceTime = default;
        private TimeSpan totalRestTime = default;
        private int totalNumberOfServedCustomers = default;
        private ServiceTime bankWorkingTime = default;
        public int QueueLength
        {
            get { return customerQueue.Count; }
        }

        public void PrintInfo()
        {
            Console.WriteLine("NumberOfCustomers: {0} - ServiceTime: {1} - RestTimeInWorkingHours: {2}",
                totalNumberOfServedCustomers,
                totalServiceTime,
                totalRestTime);
        }

        public Counter(ServiceTime bankWorkingTime)
        {
            totalRestTime = bankWorkingTime.End - bankWorkingTime.Start;
            //firstAvailableTime = bankWorkingTime.Start;
            nextAvailableTime = bankWorkingTime.Start;
            this.bankWorkingTime = bankWorkingTime;
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
            customer.ServiceStartTime = (customer.ArrivalTime >= nextAvailableTime) ? customer.ArrivalTime : nextAvailableTime;
            //firstAvailableTime = (customer.ArrivalTime >= firstAvailableTime) ? (customer.ArrivalTime + customer.ServiceTimeInMinutes) : (firstAvailableTime + customer.ServiceTimeInMinutes);
            customer.ExitTime = customer.ServiceStartTime + customer.ServiceTime;
            customer.WaitingTime = customer.ServiceStartTime.Subtract(customer.ArrivalTime);
            customer.GotServiceInLegalTime = (customer.ExitTime <= bankWorkingTime.End);

            totalNumberOfServedCustomers++;
            nextAvailableTime = (customer.ArrivalTime >= nextAvailableTime) ? (customer.ArrivalTime + customer.ServiceTime) : (nextAvailableTime + customer.ServiceTime);
            totalServiceTime += customer.ServiceTime;
            //totalRestTime -= customer.ServiceTime; //this calculates a possible negative time where amount of negative time indicates overtime work

            //Calculating rest time olny in bank working hours.
            if (customer.ExitTime <= bankWorkingTime.End)
            {
                totalRestTime -= customer.ServiceTime;
            }
            else
            if (customer.ExitTime > bankWorkingTime.End && customer.ServiceStartTime < bankWorkingTime.End)
            {
                totalRestTime -= (bankWorkingTime.End - customer.ServiceStartTime);
            }
        }

        public void ServeNextCustomer(TimeSpan clock)
        {
            while (QueueLength > 0 && clock >= nextAvailableTime)
            {
                Customer nextCustomer = customerQueue.Peek();
                if (clock >= nextCustomer.ArrivalTime + nextCustomer.ServiceTime)
                {
                    customerQueue.Dequeue();
                    //Customer customer = customerQueue.Dequeue();
                    //customer.ServiceStartTime = (customer.ArrivalTime >= firstAvailableTime) ? customer.ArrivalTime : firstAvailableTime;
                    //firstAvailableTime= (customer.ArrivalTime >= firstAvailableTime) ? (customer.ArrivalTime + customer.ServiceTimeInMinutes) : (firstAvailableTime + customer.ServiceTimeInMinutes);
                    //customer.ExitTime = customer.ServiceStartTime + customer.ServiceTimeInMinutes;
                    //customer.WaitingTime = customer.ServiceStartTime.Subtract(customer.ArrivalTime);
                    //customer.GotServiceInLegalTime = (customer.ExitTime <= bankWorkingTime.End);
                }
            }
        }
    }
}
