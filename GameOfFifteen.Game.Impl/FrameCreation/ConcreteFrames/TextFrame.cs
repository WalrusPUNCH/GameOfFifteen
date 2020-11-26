using System.Drawing;
using GameOfFifteen.Game.Entities;

namespace GameOfFifteen.Game.Impl.FrameCreation.ConcreteFrames
{
    public class TextFrame : Frame
    {
        public TextFrame(string content, Point finishPoint) : base(content, finishPoint)
        {

        }
        
        public override string Render()
        {
            return Content;
        }
    }
}