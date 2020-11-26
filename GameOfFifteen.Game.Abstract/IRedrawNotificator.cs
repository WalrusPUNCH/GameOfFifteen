using GameOfFifteen.Game.Entities;

namespace GameOfFifteen.Game.Abstract
{
    public interface IRedrawNotificator
    {
        delegate void RedrawField(Frame[,] board);
        event  RedrawField NotifyOnPlayfieldChange;
    }
}