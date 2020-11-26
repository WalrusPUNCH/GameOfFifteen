namespace GameOfFifteen.Game.Entities
{
    public class GameSettings
    {
        public readonly int  Size;
        public readonly Level Level;
        public readonly FrameType FrameType;
        public readonly bool IsRandomActionsEnabled;

        public GameSettings(int size, Level level, FrameType frameType, bool withRandomActions)
        {
            Size = size;
            Level = level;
            FrameType = frameType;
            IsRandomActionsEnabled = withRandomActions;
        }
    }
}