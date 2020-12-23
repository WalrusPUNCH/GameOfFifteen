using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GameOfFifteen.CommandCenter.Exceptions
{
    [Serializable]
    public class NotExistingCommandException : Exception
    {
        public NotExistingCommandException() 
        {
        }
        public NotExistingCommandException(string message) : base(message) 
        {
        }
        public NotExistingCommandException(string message, Exception inner) : base(message, inner) 
        {
        }

        protected NotExistingCommandException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

    }

}
