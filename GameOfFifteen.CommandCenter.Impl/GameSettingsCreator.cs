using GameOfFifteen.CommandCenter.Exceptions;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfFifteen.CommandCenter.Impl
{
    public class GameSettingsCreator : IGameSettingsCreator
    {
        public GameSettingsCreator() { }
        public GameSettings CreateGameSettings(string[] parameters)
        {
            if (parameters.Length < 4)
                throw new NotEnoughParametersForCommandException("Not enough parameters for command");
            var size = CountFieldSize(parameters[1]);
            var level = CountLevel(parameters[2]);
            var frameType = CountFrameType(parameters[3]);
            bool isRandomActionsEnabled = false;
            if (parameters.Length >= 5)
            {
                isRandomActionsEnabled = CountRandomEnable(parameters[4]);
            }
            return new GameSettings(size, level, frameType, isRandomActionsEnabled);
        }

        private int CountFieldSize(string size)
        {
            if (int.TryParse(size, out int fieldSize) == false)
                throw new InvalidMapSizeException("You entered invalid map size. Map size should be higher than 2 and lower than 10.");

            if (fieldSize <= 2 || fieldSize >= 10)
            {
                throw new InvalidMapSizeException("You entered invalid map size. Map size should be higher than 2 and lower than 10.");
            }
            return fieldSize;
        }

        private Level CountLevel(string levelValue)
        {
            if (Enum.TryParse(levelValue, true, out Level level) == false)
            {
                throw new InvalidLevelException("You entered invalid level. Available levels are 'Easy', 'Medium' and 'Hard'");
            }
            return level;
        }

        private FrameType CountFrameType(string type)
        {
            if (Enum.TryParse(type, true, out FrameType frameType) == false)
            {
                throw new InvalidFrameTypeException("You entered invalid frame type. Available types are 'Normal' and 'Boarded'");
            }
            return frameType;
        }

        private bool CountRandomEnable(string randomValue)
        {
            if (bool.TryParse(randomValue, out bool isRandomActionsEnabled) == false)
            {
                throw new InvalidRandomActionsParameterException("You entered invalid value for activating random actions. Only 'true' or 'false' available");
            }
            return isRandomActionsEnabled;
        }
    }
}
