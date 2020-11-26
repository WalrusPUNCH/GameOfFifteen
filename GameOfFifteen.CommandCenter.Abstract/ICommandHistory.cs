namespace GameOfFifteen.CommandCenter.Abstract
{
    public interface ICommandHistory
    {
        void SaveCommand(IUndoableCommand command);
        void Undo();
        void Clear();
    }
}