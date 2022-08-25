using System;
using System.Collections.Generic;

namespace BankSimulator
{

    class Customer
    {
        public DateTime ArrivalTime { get; set; }
        public int ServiceTimeInMinutes { get; set; }
        public TimeSpan WaitingTime { get; set; }
        public DateTime ServiceStartTime { get; set; }
        public DateTime ExitTime { get; set; }
        public bool GotServiceInLegalTime { get; set; }
        public void PrintInfo() 
        {
            Console.WriteLine("Arrival - StartServiceTime - ServiceTime - WaitingTime - ExitTime - GotServiceInLegalTime");
            Console.WriteLine(ArrivalTime.ToShortTimeString()+" : " + ServiceStartTime.ToShortTimeString() + " : " + ServiceTimeInMinutes + " : " + WaitingTime.TotalMinutes + " : " + ExitTime.ToShortTimeString() + " : " + GotServiceInLegalTime.ToString());
            //Console.WriteLine("Arrival: {0} - StartServiceTime: {1} - ServiceTime: {2} - WaitingTime: {3} - ExitTime: {4} - GotServiceInLegalTime: {5}", ArrivalTime.ToShortTimeString(), ServiceStartTime.ToShortTimeString(), ServiceTimeInMinutes, WaitingTime.TotalMinutes, ExitTime.ToShortTimeString(), GotServiceInLegalTime.ToString());
        }
        public Customer() { }

    }
}
