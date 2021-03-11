using System;
using System.Collections.Generic;
using System.Text;
using Telephony.Contracts;

namespace Telephony.IO
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
