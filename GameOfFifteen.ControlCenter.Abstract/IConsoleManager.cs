using GameOfFifteen.Game.Entities;

namespace GameOfFifteen.ControlCenter.Abstract
{
    public interface IConsoleManager
    {
        string[] GetProcessedInput();
        void DrawField(Frame[,] field);
        void ShowText(string text);
        void Clear();
    }
}