using System.Drawing;
using GameOfFifteen.Game.Entities;
using GameOfFifteen.Game.Impl.FrameCreation.ConcreteFrames;

namespace GameOfFifteen.Game.Impl.FrameCreation.ConcreteFrameCreators
{
    public class BoardedFrameCreator : FrameCreator
    {
        public override Frame CreateFrame(string content, Point finishPoint)
        {
            return new BoardedFrame(content, finishPoint);
        }
    }
}