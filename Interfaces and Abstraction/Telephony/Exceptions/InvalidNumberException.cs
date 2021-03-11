using System;
using System.Collections.Generic;
using System.Text;

namespace Telephony.Exceptions
{
  public   class InvalidNumberException :Exception
    {
        private const string EXC_MSG = "Invalid number!";

        public InvalidNumberException()
            :base(EXC_MSG)
        {

        }
    }
}
