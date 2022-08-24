using System;
using System.Collections.Generic;

namespace BankSimulator
{

    class Customer
    {
        public DateTime ArrivalTime { get; set; }
        public int ServiceTimeInMinutes { get; set; }
        public DateTime WaitingTime { get; set; }
        public DateTime ServiceStartTime { get; set; }
        public DateTime ExitTime { get; set; }
        public bool GotServiceInLegalTime { get; set; }

        public Customer() { }

    }
}
