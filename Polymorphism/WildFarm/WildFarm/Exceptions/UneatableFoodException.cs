using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace WildFarm.Exceptions
{
    public class UneatableFoodException : Exception
    {
        
        public UneatableFoodException(string message) : base(message)
        {

        }
    }
}
