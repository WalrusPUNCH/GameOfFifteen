using System;
using System.Collections.Generic;
using System.Linq;
using GameOfFifteen.CommandCenter.Abstract;

namespace GameOfFifteen.CommandCenter.Impl
{
    public class CommandHistory : ICommandHistory
    {
        private readonly Stack<IUndoableCommand> _history;

        public CommandHistory()
        {
            _history = new Stack<IUndoableCommand>();
        }
        public void SaveCommand(IUndoableCommand command)
        {
            _history.Push(command);
        }

        public void Undo()
        {
            if (_history.Count != 0)
            {
                IUndoableCommand command = _history.Pop();
                command.Undo();
            }
        }

        public void Clear()
        {
            _history.Clear();
        }
    }
}