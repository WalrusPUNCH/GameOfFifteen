using System;

namespace GameOfFifteen.CommandCenter.Impl.Exceptions
{
    public class InvalidMapSizeException : Exception
    {
        public InvalidMapSizeException(string message) : base(message)
        {
            
        }
    }
}