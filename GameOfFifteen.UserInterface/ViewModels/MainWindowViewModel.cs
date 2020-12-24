using GameOfFifteen.CommandCenter.Abstract;
using GameOfFifteen.CommandCenter.Impl.Commands;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Entities;
using GameOfFifteen.UserInterface.Mappers.Abstract;
using GameOfFifteen.UserInterface.Utilities;
using GameOfFifteen.UserInterface.Utilities.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GameOfFifteen.UserInterface.ViewModels
{
    public class MainWindowViewModel
    {
        private IGame _currentGame;
        private IGameCreator _gameCreator;
        private IManipulator _manipulator;
        private ICommandHistory _history;
        private IMapFrom<GameSettings, GameSettingsViewModel> _settingsMapper;
        private IBoardWorker _boardWorker;

        private IMessageBoxService _messageBoxService;
        public ObservableCollection<FrameViewModel> Frames { get; set; } = new ObservableCollection<FrameViewModel>();

        private GameSettingsViewModel _gameSettings;
        public GameSettingsViewModel GameSettings
        {
            get => _gameSettings;
            set
            {
                _gameSettings = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<Level> Levels
        {
            get
            {
                return Enum.GetValues(typeof(Level)).Cast<Level>();
            }
        }

        public IEnumerable<FrameType> FrameTypes
        {
            get
            {
                return Enum.GetValues(typeof(FrameType)).Cast<FrameType>();
            }
        }

        public System.Windows.Input.ICommand NewGameCommand
        {
            get
            {
                return new DelegateCommand(
                    (context) =>
                    new StartGameCommand(_gameCreator, _manipulator, _settingsMapper.MapFrom(GameSettings)).Execute(),
                    (context) => GameSettings.IsValid()
                    );
            }
        }

        public System.Windows.Input.ICommand UndoCommand
        {
            get
            {
                return new DelegateCommand(
                    (context) =>
                    new UndoCommand(_history).Execute()
                    );
            }
        }

        public MainWindowViewModel(IGameCreator gameCreator, IManipulator manipulator, ICommandHistory history,
                                    IMapFrom<GameSettings, GameSettingsViewModel> settingsMapper,
                                    IBoardWorker boardWorker,
                                    IMessageBoxService messageBoxService)
        {
            _settingsMapper = settingsMapper;
            _boardWorker = boardWorker;
            _messageBoxService = messageBoxService;

            _gameCreator = gameCreator;
            _manipulator = manipulator;
            _history = history;

            _gameCreator.NotifyOnGameCreated += Creator_NotifyOnGameCreated;
            _manipulator.NotifyOnPlayfieldChange += OnPlayfieldChanged;

            GameSettings = new GameSettingsViewModel(4, Level.Easy, FrameType.Normal, false);
        }

        private void Creator_NotifyOnGameCreated(IGame game)
        {
            _currentGame = game;
            _boardWorker.CurrentGame = _currentGame;
            _currentGame.NotifyOnGameWon += OnCurrentGameWon;
            _currentGame.NotifyOnPlayfieldChange += OnPlayfieldChanged;
            _history.Clear();
        }

        private void OnPlayfieldChanged(Frame[,] board)
        {
            _boardWorker.FillBoard(Frames, board);
        }

        private void OnCurrentGameWon()
        {
            _messageBoxService.ShowMessage("Congratulations!\nYou won!", "Victory");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
