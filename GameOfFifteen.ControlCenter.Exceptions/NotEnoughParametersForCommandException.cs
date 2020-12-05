using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfFifteen.CommandCenter.Exceptions
{
    public class NotEnoughParametersForCommandException : Exception
    {
        public NotEnoughParametersForCommandException() { }
        public NotEnoughParametersForCommandException(string message) : base(message) { }
        public NotEnoughParametersForCommandException(string message, Exception inner) : base(message, inner) { }
        
    }
}
