using GameOfFifteen.CommandCenter.Abstract;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Entities;

namespace GameOfFifteen.CommandCenter.Impl.Commands
{
    public class MoveCommand : ICommand, IUndoableCommand
    {
        private readonly IGame _game;
        private readonly Direction _direction;
        private readonly IManipulator _manipulator;
        private readonly ICommandHistory _history;
        private IGameMemento _savedMemento;


        public MoveCommand(IGame game, Direction direction, IManipulator manipulator, ICommandHistory history)
        {
            _game = game;
            _direction = direction;
            _manipulator = manipulator;
            _history = history;
        }


        public void Execute()
        {
            _savedMemento = _game.SaveGameMemento();

            bool isMoveValid =
                _manipulator.MakeMove(_game.Playfield, _direction, _game.Settings.IsRandomActionsEnabled);
            if (isMoveValid)
            {
                _history.SaveCommand(this);
                _game.IncrementMovesNumber();
                _game.IsSolved();
            }
        }

        public void Undo()
        {
           _game.RestoreFromMemento(_savedMemento);
        }
    }
}