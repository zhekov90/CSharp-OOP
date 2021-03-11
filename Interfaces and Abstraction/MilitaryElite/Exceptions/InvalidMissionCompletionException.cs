using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Exceptions
{
    public class InvalidMissionCompletionException : Exception
    {
        private const string DEF_EXC_MSG = "Mission already comleted!";
        public InvalidMissionCompletionException()
        {
        }

        public InvalidMissionCompletionException(string message) : base(message)
        {
        }
    }
}
