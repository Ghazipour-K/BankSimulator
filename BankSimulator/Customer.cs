using System;
using System.Collections.Generic;

namespace BankSimulator
{

    class Customer
    {
        public int ArrivalTime { get; set; }
        public int ServiceTime { get; set; }
        public int WaitingTime { get; set; }
        public int ServiceStartTime { get; set; }
        public int ExitTime { get; set; }
        public bool GotServiceInLegalTime { get; set; }

        public Customer() { }

    }
}
