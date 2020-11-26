using System.Drawing;
using GameOfFifteen.Game.Entities;
using GameOfFifteen.Game.Impl.FrameCreation.ConcreteFrames;

namespace GameOfFifteen.Game.Impl.FrameCreation.ConcreteFrameCreators
{
    public class TextFrameCreator : FrameCreator
    {
        public override Frame CreateFrame(string content, Point startPoint)
        {
            return new TextFrame(content, startPoint); 
        }
    }
}