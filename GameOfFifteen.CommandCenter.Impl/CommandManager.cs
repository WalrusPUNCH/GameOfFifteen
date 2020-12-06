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

        public CommandManager(IGameCreator creator, IManipulator manipulator, ICommandHistory history)
        {
            _gameCreator = creator;
            _manipulator = manipulator;
            _history = history;
        }

        
        public ICommand GetCommand(string[] parameters)
        {
            ICommand command = null;
            if (parameters == null || parameters.Length < 1)
                throw new NotExistingCommandException("You entered non existing command. Try again");

            string keyWord = parameters[0];

            switch (keyWord)
            {
                case "start":
                    command =  GetStartGameCommand(parameters);
                    break;
                case "up":
                case "w":
                    command =  GetMoveCommand(Direction.Up);
                    break;
            
                case "right":
                case "d":    
                    command =  GetMoveCommand(Direction.Right);
                    break;
              
                case "down":
                case "s":    
                    command =  GetMoveCommand(Direction.Down);
                    break;
                
                case "left":
                case "a":    
                    command =  GetMoveCommand(Direction.Left);
                    break;
                
                case "undo":
                case "u":
                    command = GetUndoCommand();
                    break;
            }
            if (command == null)
                throw new NotExistingCommandException("You entered non existing command. Try again");
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
            if (parameters.Length < 4)
                throw new NotEnoughParametersForCommandException("Not enough parameters for command");

            if (int.TryParse(parameters[1], out int fieldSize) == false)
                throw new InvalidMapSizeException("You entered invalid map size. Map size should be higher than 2 and lower than 10.");

            Level level;
            FrameType frameType;
            bool isRandomActionsEnabled = false;
           
            if(fieldSize <= 2 || fieldSize >= 10)
            {
                throw new InvalidMapSizeException("You entered invalid map size. Map size should be higher than 2 and lower than 10.");
            }

            if (Enum.TryParse<Level>(parameters[2], true, out level) == false)
            {
                throw new InvalidLevelException("You entered invalid level. Available levels are 'Easy', 'Medium' and 'Hard'");
            }
                        
            if(Enum.TryParse<FrameType>(parameters[3], true, out frameType) == false)
            {
                throw new InvalidFrameTypeException("You entered invalid frame type. Available types are 'Normal' and 'Boarded'");
            }

            if (parameters.Length >= 5) // optional parameter
            {
                if (bool.TryParse(parameters[4], out isRandomActionsEnabled) == false)
                {
                    throw new InvalidRandomActionsParameterException("You entered invalid value for activating random actions. Only 'true' or 'false' available");
                }

            }

            GameSettings settings = new GameSettings(fieldSize, level, frameType, isRandomActionsEnabled);
            ICommand command = new StartGameCommand(_gameCreator, _manipulator, settings);
            return command;
        }
    }
}