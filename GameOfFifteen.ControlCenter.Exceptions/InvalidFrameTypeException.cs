using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GameOfFifteen.CommandCenter.Exceptions
{
    [Serializable]
    public class InvalidFrameTypeException : Exception
    {
        public InvalidFrameTypeException() 
        {
        }
        public InvalidFrameTypeException(string message) : base(message) 
        { 
        }
        public InvalidFrameTypeException(string message, Exception inner) : base(message, inner) 
        { 
        }
        protected InvalidFrameTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
