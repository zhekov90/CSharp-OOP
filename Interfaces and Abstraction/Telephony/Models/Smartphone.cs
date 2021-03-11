using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telephony.Contracts;
using Telephony.Exceptions;

namespace Telephony.Models
{
    public class Smartphone : ICallable, IBrowserable
    {

        public string Call(string number)
        {
            if (!number.All(ch => char.IsDigit(ch)))
            {
                throw new InvalidNumberException();
            }
            return $"Calling... {number}";
        }
        public string Browse(string url)
        {
            if (url.Any(ch=>char.IsDigit(ch)))
            {
                throw new InvalidUrlException();
            }
            return $"Browsing: {url}!";
        }
    }
}
