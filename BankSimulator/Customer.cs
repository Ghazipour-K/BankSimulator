using System;

namespace BankSimulator
{

    class Customer
    {
        public TimeSpan ArrivalTime { get; set; }
        public TimeSpan ServiceTime { get; set; }
        public TimeSpan WaitingTime { get; set; }
        public TimeSpan ServiceStartTime { get; set; }
        public TimeSpan ExitTime { get; set; }
        public bool GotServiceInLegalTime { get; set; }
        public int AssignedCounterNo { get; set; }
        public void PrintInfo() 
        {
            //Console.WriteLine("Arrival - StartServiceTime - ServiceTime - WaitingTime - ExitTime - GotServiceInLegalTime");
            Console.WriteLine("Arrival - Start - Service - Waiting - Exit - LegalTime - CounterNo");
            Console.WriteLine(ArrivalTime.ToString() + " : " + ServiceStartTime.ToString() + " : " + ServiceTime + " : " + WaitingTime.TotalMinutes + " : " + ExitTime.ToString() + " : " + GotServiceInLegalTime.ToString() + " : " + AssignedCounterNo);
            //Console.WriteLine("Arrival: {0} - StartServiceTime: {1} - ServiceTime: {2} - WaitingTime: {3} - ExitTime: {4} - GotServiceInLegalTime: {5}", ArrivalTime.ToShortTimeString(), ServiceStartTime.ToShortTimeString(), ServiceTimeInMinutes, WaitingTime.TotalMinutes, ExitTime.ToShortTimeString(), GotServiceInLegalTime.ToString());
        }
        public Customer() { }

    }
}
