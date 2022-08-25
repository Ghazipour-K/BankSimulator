using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSimulator
{
    public static class MyExtensions
    {

        /// <summary>
        /// This method creates a DateTime instance with hour value set to specified hour value 
        /// </summary>
        /// <param name="hour">A positive int to specify current hour</param>
        /// <returns>Returns a DateTime instance with current system datetime but hour value replaced by provided value.</returns>
        public static DateTime ToDateTime(this int hour)
        {
            return DateTime.Now.AddHours(-DateTime.Now.Hour).AddMinutes(-DateTime.Now.Minute).AddHours(hour);
        }
    }
}
