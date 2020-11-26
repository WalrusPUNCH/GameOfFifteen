using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Entities;

namespace GameOfFifteen.Game.Impl
{
    public class GameMemento : IGameMemento
    {
        public readonly GameSettings Settings;
        public readonly int Moves;
        public readonly IPlayfield Playfield;

        public GameMemento(GameSettings settings, int moves, IPlayfield playfield)
        {
            Settings = settings;
            Moves = moves;
            Playfield = playfield;
        }
    }
}