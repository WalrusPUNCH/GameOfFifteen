using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfFifteen.CommandCenter.Exceptions
{
    public class NotExistingCommandException : Exception
    {
        public NotExistingCommandException() { }
        public NotExistingCommandException(string message) : base(message) { }
        public NotExistingCommandException(string message, Exception inner) : base(message, inner) { }
        
    }

}
