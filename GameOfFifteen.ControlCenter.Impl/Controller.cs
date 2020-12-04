using System;
using GameOfFifteen.CommandCenter.Abstract;
using GameOfFifteen.CommandCenter.Exceptions;
using GameOfFifteen.ControlCenter.Abstract;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Entities;

namespace GameOfFifteen.ControlCenter.Impl
{
    public class Controller : IController
    {
        private IGame _currentGame;
        private ICommand _currentCommand;
        private bool _keepGameActive = true;

        private readonly ICommandHistory _history;
        private readonly ICommandManager _commandManager;
        private readonly IGameMessageHolder _gameMessageHolder;
        private readonly IConsoleManager _console;

        public Controller(IGameCreator gameCreator, IManipulator manipulator, ICommandHistory history, 
            ICommandManager commandManager, IGameMessageHolder gameMessageHolder, IConsoleManager console)
        {
            gameCreator.NotifyOnGameCreated += OnNewGameCreated;
            manipulator.NotifyOnPlayfieldChange += OnPlayfieldChanged;
            
            _history = history;
            _commandManager = commandManager;
            _gameMessageHolder = gameMessageHolder;
            _console = console;
        }
        
        public void ServeGame()
        {
            _console.ShowText(_gameMessageHolder.GetGeneralGameInformation());
            string[] input;
            do
            {
                input = _console.GetProcessedInput();
                if (input.Length < 1 || string.IsNullOrWhiteSpace(input[0]))
                    continue;
                try
                {
                    _currentCommand = _commandManager.GetCommand(input);
                }
                catch (Exception ex)
                {
                    if (ex is InvalidMapSizeException ||
                        ex is InvalidLevelException ||
                        ex is InvalidFrameTypeException ||
                        ex is InvalidRandomActionsParameterException)
                    {
                        _console.ShowText(ex.Message);
                    }
                }

                _currentCommand?.Execute();
            }
            while (input?[0] != "quit" && input?[0] != "q");
        }
        
        private void OnNewGameCreated(IGame game)
        {
            _currentGame = game;
            _currentGame.NotifyOnGameWon += OnCurrentGameWon;
            _currentGame.NotifyOnPlayfieldChange += OnPlayfieldChanged;
            _commandManager.Game = game;
            _history.Clear();
           
        }
        private void OnPlayfieldChanged(Frame[,] board)
        {
            _console.Clear();
            _console.ShowText(_gameMessageHolder.GetShortGameInformation(_currentGame.Moves));
            _console.DrawField(board);
        }
        private void OnCurrentGameWon()
        {
            //_keepGameActive = false;
            _console.ShowText(_gameMessageHolder.GetVictoriousMessage(_currentGame.Moves));
        }

    }
}