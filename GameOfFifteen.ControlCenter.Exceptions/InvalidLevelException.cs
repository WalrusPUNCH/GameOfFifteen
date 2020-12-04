using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfFifteen.CommandCenter.Exceptions
{
    public class InvalidLevelException : Exception
    {
        public InvalidLevelException() { }
        public InvalidLevelException(string message) : base(message) { }
        public InvalidLevelException(string message, Exception inner) : base(message, inner) { }
       
    }
}
