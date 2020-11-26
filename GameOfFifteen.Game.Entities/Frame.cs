using System.Drawing;

namespace GameOfFifteen.Game.Entities
{
    public abstract class  Frame
    {
        public readonly string Content;
        public readonly Point FinishPoint;

        public Frame(string content, Point point)
        {
            Content = content;
            FinishPoint = point;
        }
        public abstract string Render();
        
    }
}