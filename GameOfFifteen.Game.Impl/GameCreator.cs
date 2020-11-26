using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Entities;

namespace GameOfFifteen.Game.Impl
{
    public class GameCreator : IGameCreator
    {
        public event IGameCreator.GameCreated NotifyOnGameCreated;

        public void CreateGame(GameSettings settings, IManipulator manipulator)
        {
            IGame newGame = new Game(settings);
            NotifyOnGameCreated?.Invoke(newGame);
            manipulator.ShuffleBoard(newGame.Playfield, settings.Level);
        }
    }
}