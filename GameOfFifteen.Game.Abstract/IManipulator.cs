using GameOfFifteen.Game.Entities;

namespace GameOfFifteen.Game.Abstract
{
    public interface IManipulator : IRedrawNotificator
    { 
        void ShuffleBoard(IPlayfield playfield, Level level);
        bool MakeMove(IPlayfield playfield, Direction direction, bool withRandomMoves);
    }
}