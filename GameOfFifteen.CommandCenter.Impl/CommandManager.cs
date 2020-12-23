using System;
using GameOfFifteen.CommandCenter.Abstract;
using GameOfFifteen.CommandCenter.Impl.Commands;
using GameOfFifteen.CommandCenter.Exceptions;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Entities;

namespace GameOfFifteen.CommandCenter.Impl
{
    public class CommandManager : ICommandManager
    {
        public IGame Game { private get; set; }
        
        private readonly IGameCreator _gameCreator;
        private readonly IManipulator _manipulator;
        private readonly ICommandHistory _history;
        private readonly IGameSettingsCreator _settingsCreator;

        public CommandManager(IGameCreator creator, IManipulator manipulator, ICommandHistory history)
        {
            _gameCreator = creator;
            _manipulator = manipulator;
            _history = history;
        }

        
        public ICommand GetCommand(string[] parameters)
        {
            if (parameters == null || parameters.Length < 1)
                throw new NotExistingCommandException("You entered non existing command. Try again");
            var command = CreateCommand(parameters);
            if (command == null)
                throw new NotExistingCommandException("You entered non existing command. Try again");
            return command;
        }

        private ICommand CreateCommand(string[] parameters)
        {
            ICommand command;
            switch (parameters[0])
            {
                case "start":
                    command = GetStartGameCommand(parameters);
                    break;
                case "up":
                case "w":
                    command = GetMoveCommand(Direction.Up);
                    break;

                case "right":
                case "d":
                    command = GetMoveCommand(Direction.Right);
                    break;

                case "down":
                case "s":
                    command = GetMoveCommand(Direction.Down);
                    break;

                case "left":
                case "a":
                    command = GetMoveCommand(Direction.Left);
                    break;

                case "undo":
                case "u":
                    command = GetUndoCommand();
                    break;
                default:
                    command = null;
                    break;
            }
            return command;
        }

        private ICommand GetUndoCommand()
        {
            return new UndoCommand(_history);
        }

        private ICommand GetMoveCommand(Direction direction)
        {
            if (Game == null)
                throw new GameIsNotStartedException("You can't make moves until the game has started");
            return new MoveCommand(Game, direction, _manipulator, _history);
        }

        private ICommand GetStartGameCommand(string[] parameters)
        {
            var settings = _settingsCreator.CreateGameSettings(parameters);
            ICommand command = new StartGameCommand(_gameCreator, _manipulator, settings);
            return command;
        }
    }
}