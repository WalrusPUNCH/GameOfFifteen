using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GameOfFifteen.CommandCenter.Exceptions
{
    [Serializable]
    public class GameIsNotStartedException : Exception
    {
        public GameIsNotStartedException() 
        {
        }

        public GameIsNotStartedException(string message) : base(message) 
        {
        }

        public GameIsNotStartedException(string message, Exception inner) : base(message, inner)
        {
        }

        protected GameIsNotStartedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
       
    }
}
