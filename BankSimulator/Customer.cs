using System;
using System.Collections.Generic;

namespace BankSimulator
{

    class Customer
    {
        public TimeSpan ArrivalTime { get; set; }
        public TimeSpan ServiceTimeInMinutes { get; set; }
        public TimeSpan WaitingTime { get; set; }
        public TimeSpan ServiceStartTime { get; set; }
        public TimeSpan ExitTime { get; set; }
        public bool GotServiceInLegalTime { get; set; }
        public void PrintInfo() 
        {
            Console.WriteLine("Arrival - StartServiceTime - ServiceTime - WaitingTime - ExitTime - GotServiceInLegalTime");
            Console.WriteLine(ArrivalTime.ToString()+" : " + ServiceStartTime.ToString() + " : " + ServiceTimeInMinutes + " : " + WaitingTime.TotalMinutes + " : " + ExitTime.ToString() + " : " + GotServiceInLegalTime.ToString());
            //Console.WriteLine("Arrival: {0} - StartServiceTime: {1} - ServiceTime: {2} - WaitingTime: {3} - ExitTime: {4} - GotServiceInLegalTime: {5}", ArrivalTime.ToShortTimeString(), ServiceStartTime.ToShortTimeString(), ServiceTimeInMinutes, WaitingTime.TotalMinutes, ExitTime.ToShortTimeString(), GotServiceInLegalTime.ToString());
        }
        public Customer() { }

    }
}
