using GameOfFifteen.Game.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameOfFifteen.UserInterface.ViewModels
{
    public class GameSettingsViewModel : IDataErrorInfo
    {
        private int size;
        private Level level;
        private FrameType frameType;
        private bool isRandom;


        public int Size
        {
            get => size;
            set
            {
                size = value;
                OnPropertyChanged();
            }
        }
        public Level Level
        {
            get => level;
            set
            {
                level = value;
                OnPropertyChanged();
            }
        }
        public FrameType FrameType
        {
            get => frameType;
            set
            {
                frameType = value;
                OnPropertyChanged();
            }
        }
        public bool IsRandomActionsEnabled
        {
            get => isRandom;
            set
            {
                isRandom = value;
                OnPropertyChanged();
            }
        }

        public GameSettingsViewModel(int size, Level level, FrameType frameType, bool withRandomActions)
        {
            Size = size;
            Level = level;
            FrameType = frameType;
            IsRandomActionsEnabled = withRandomActions;
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Size":
                        if ((Size <= 2) || (Size > 10))
                        {
                            error = "Available board sizes are [3;10]";
                        }
                        break;
                }
                return error;
            }
        }

        public bool IsValid()
        {
            return string.IsNullOrEmpty(this["Size"]);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }

}
