using System;

namespace GameOfFifteen.CommandCenter.Exceptions
{
    public class InvalidMapSizeException : Exception
    {
        public InvalidMapSizeException() { }
        public InvalidMapSizeException(string message) : base(message) { }
        public InvalidMapSizeException(string message, Exception inner) : base(message, inner) { }
        
    }

}
