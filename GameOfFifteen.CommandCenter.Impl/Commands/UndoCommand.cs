using GameOfFifteen.CommandCenter.Abstract;

namespace GameOfFifteen.CommandCenter.Impl.Commands
{
    public class UndoCommand : ICommand
    {
        private readonly ICommandHistory _history;

        public UndoCommand(ICommandHistory history)
        {
            _history = history;
        }

        public void Execute()
        {
            _history.Undo();
        }
    }
}