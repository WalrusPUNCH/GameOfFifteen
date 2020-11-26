using GameOfFifteen.Game.Abstract;

namespace GameOfFifteen.CommandCenter.Abstract
{
    public interface ICommandManager
    {
        void SetGame(IGame game);
        ICommand GetCommand(string[] keyWords);
    }
}