using GameOfFifteen.Game.Entities;

namespace GameOfFifteen.Game.Abstract
{
    public interface IGameCreator
    {
        delegate void GameCreated(IGame game);
        public event GameCreated NotifyOnGameCreated;

        void CreateGame(GameSettings settings, IManipulator manipulator);
        
    }
}