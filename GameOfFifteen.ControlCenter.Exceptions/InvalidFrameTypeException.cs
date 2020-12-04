using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfFifteen.CommandCenter.Exceptions
{
    public class InvalidFrameTypeException : Exception
    {
        public InvalidFrameTypeException() { }
        public InvalidFrameTypeException(string message) : base(message) { }
        public InvalidFrameTypeException(string message, Exception inner) : base(message, inner) { }
    }
}
