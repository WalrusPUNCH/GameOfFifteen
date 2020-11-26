using GameOfFifteen.CommandCenter.Abstract;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Entities;

namespace GameOfFifteen.CommandCenter.Impl.Commands
{
    public class StartGameCommand : ICommand
    {
        private readonly IGameCreator _gameCreator;
        private readonly IManipulator _manipulator;
        private readonly GameSettings _gameSettings;

        public StartGameCommand(IGameCreator gameCreator, IManipulator manipulator, GameSettings gameSettings)
        {
            _gameCreator = gameCreator;
            _manipulator = manipulator;
            _gameSettings = gameSettings;
        }
        
        public void Execute()
        {
            _gameCreator.CreateGame(_gameSettings, _manipulator);
        }
    }
}