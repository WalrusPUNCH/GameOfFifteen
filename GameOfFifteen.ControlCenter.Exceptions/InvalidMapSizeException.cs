using System;
using System.Runtime.Serialization;

namespace GameOfFifteen.CommandCenter.Exceptions
{
    [Serializable]
    public class InvalidMapSizeException : Exception
    {
        public InvalidMapSizeException() 
        {
        }
        public InvalidMapSizeException(string message) : base(message)
        {
        }
        public InvalidMapSizeException(string message, Exception inner) : base(message, inner) 
        {
        }

        protected InvalidMapSizeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

}
