using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telephony.Contracts;
using Telephony.Exceptions;

namespace Telephony.Models
{
    public class StationaryPhone : ICallable
    {
        public StationaryPhone()
        {

        }
        public string Call(string number)
        {
            if (!number.Any(ch=>char.IsDigit(ch)))
            {
                throw new InvalidNumberException();
            }
            return $"Dialing... {number}";
        }
    }
}
