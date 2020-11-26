using System;
using GameOfFifteen.CommandCenter.Abstract;
using GameOfFifteen.CommandCenter.Impl.Commands;
using GameOfFifteen.CommandCenter.Impl.Exceptions;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Entities;

namespace GameOfFifteen.CommandCenter.Impl
{
    public class CommandManager : ICommandManager
    {
        private IGame _game;
        
        private readonly IGameCreator _gameCreator;
        private readonly IManipulator _manipulator;
        private readonly ICommandHistory _history;

        public CommandManager(IGameCreator creator, IManipulator manipulator, ICommandHistory history)
        {
            _gameCreator = creator;
            _manipulator = manipulator;
            _history = history;
        }

        public void SetGame(IGame game)
        {
            _game = game;
        }

        public ICommand GetCommand(string[] parameters)
        {
            ICommand command = null;
            string keyWord = parameters[0];
            switch (keyWord)
            {
                case "start":
                    return GetStartGameCommand(parameters);
            }

            if (_game != null)
            {
                switch (keyWord)
                {
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
            }
            
            return command;
        }

        private ICommand GetUndoCommand()
        {
            return new UndoCommand(_history);
            }
        private ICommand GetMoveCommand(Direction direction)
        {
            return new MoveCommand(_game, direction, _manipulator, _history);
        }
        private ICommand GetStartGameCommand(string[] parameters)
        {
            ICommand command = null;
            if (parameters.Length < 4)
                        return null;
            int fieldSize;

            if (Int32.TryParse(parameters[1], out fieldSize) == false)
                return null;
                    
            Level lvl;
            FrameType frameType;
            bool isRandomActionsEnabled = false;
            try
            {
                if(fieldSize < 2 || fieldSize >= 10)
                    throw new InvalidMapSizeException("You entered invalid map size. Map size should be higher than 2 and lower than 10.\n");

            }
            catch (InvalidMapSizeException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
                        
            try
            {
                lvl = Enum.Parse<Level>(parameters[2], true);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("You entered invalid level. Available levels are Easy, Medium and Hard\n");
                return null;
            }
                        
            try
            {
                frameType = Enum.Parse<FrameType>(parameters[3], true);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("You entered invalid frame type. Available types are Normal and Boarded\n");
                return null;
            }

            try
            {
                if (parameters.Length >= 5)
                {
                    isRandomActionsEnabled = Convert.ToBoolean(parameters[4]);
                }
            }
                        
            catch (FormatException e)
            {
                Console.WriteLine("You entered invalid value for activating random actions. Only 'true' or 'false' available\n");
                throw;
            }
            GameSettings settings = new GameSettings(fieldSize, lvl, frameType, isRandomActionsEnabled);
            command = new StartGameCommand(_gameCreator,_manipulator, settings);
            return command;
        }
    }
}