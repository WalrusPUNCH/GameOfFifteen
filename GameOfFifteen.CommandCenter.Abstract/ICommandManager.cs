using GameOfFifteen.Game.Abstract;

namespace GameOfFifteen.CommandCenter.Abstract
{
    public interface ICommandManager
    {
        IGame Game { set; }
        ICommand GetCommand(string[] keyWords);
    }
}