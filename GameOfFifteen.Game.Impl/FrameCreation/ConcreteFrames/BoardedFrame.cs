using System.Drawing;
using GameOfFifteen.Game.Entities;

namespace GameOfFifteen.Game.Impl.FrameCreation.ConcreteFrames
{
    public class BoardedFrame : Frame
    {
        public BoardedFrame(string content, Point finishPoint) : base(content, finishPoint)
        {
 
        }
        
        public override string Render()
        {
            return $"~{Content ?? ""}~";

        }
    }
}