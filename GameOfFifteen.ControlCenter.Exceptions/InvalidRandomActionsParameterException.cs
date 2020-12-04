using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfFifteen.CommandCenter.Exceptions
{
    public class InvalidRandomActionsParameterException : Exception
    {
        public InvalidRandomActionsParameterException() { }
        public InvalidRandomActionsParameterException(string message) : base(message) { }
        public InvalidRandomActionsParameterException(string message, Exception inner) : base(message, inner) { }

    }

}
