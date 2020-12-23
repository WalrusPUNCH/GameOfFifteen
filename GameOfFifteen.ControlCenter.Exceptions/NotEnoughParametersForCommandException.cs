using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GameOfFifteen.CommandCenter.Exceptions
{
    [Serializable]
    public class NotEnoughParametersForCommandException : Exception
    {
        public NotEnoughParametersForCommandException() 
        {
        }
        public NotEnoughParametersForCommandException(string message) : base(message) 
        {
        }
        public NotEnoughParametersForCommandException(string message, Exception inner) : base(message, inner) 
        {
        }
        protected NotEnoughParametersForCommandException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
