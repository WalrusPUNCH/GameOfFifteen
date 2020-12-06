using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfFifteen.CommandCenter.Exceptions
{
    public class GameIsNotStartedException : Exception
    {
        public GameIsNotStartedException() { }
        public GameIsNotStartedException(string message) : base(message) { }
        public GameIsNotStartedException(string message, Exception inner) : base(message, inner) { }
       
    }
}
