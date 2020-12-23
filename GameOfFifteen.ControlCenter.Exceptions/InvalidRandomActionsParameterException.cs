using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GameOfFifteen.CommandCenter.Exceptions
{
    [Serializable]
    public class InvalidRandomActionsParameterException : Exception
    {
        public InvalidRandomActionsParameterException() 
        {
        }
        public InvalidRandomActionsParameterException(string message) : base(message) 
        {
        }
        public InvalidRandomActionsParameterException(string message, Exception inner) : base(message, inner) 
        {
        }

        protected InvalidRandomActionsParameterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

    }

}
