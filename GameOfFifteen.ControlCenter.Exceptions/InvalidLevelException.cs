using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GameOfFifteen.CommandCenter.Exceptions
{
    [Serializable]
    public class InvalidLevelException : Exception
    {
        public InvalidLevelException() 
        {
        }
        public InvalidLevelException(string message) : base(message) 
        {
        }
        public InvalidLevelException(string message, Exception inner) : base(message, inner) 
        { 
        }

        protected InvalidLevelException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

    }
}
