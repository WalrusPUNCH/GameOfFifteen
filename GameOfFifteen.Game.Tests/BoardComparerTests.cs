using GameOfFifteen.Game.Entities;
using GameOfFifteen.Game.Impl.FrameCreation.ConcreteFrames;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameOfFifteen.Game.Tests
{
    [TestFixture]
    class BoardComparerTests
    {
        Frame[,] _testBoard;

        [SetUp]
        public void Setup()
        {
            _testBoard = new Frame[3, 3]
                    {
                        {new TextFrame("1", new Point(0,0)), new TextFrame("2", new Point(1,0)), new TextFrame("3", new Point(2,0))},
                        {new TextFrame("4", new Point(0,1)), new TextFrame("5", new Point(1,1)),  new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), new TextFrame("8", new Point(1,2)), null}
                    };

        }
        [Test]
        //every parameter value for map size except [3;9] should throw InvalidMapSizeException
        public void CompareBoards_IdenticalBoards_ReturnsTrue()
        {
            //arrange

            //act

            //assert
            Assert.IsTrue(BoardComparer.AreBoardsEqual(_testBoard, _testBoard));
        }

        [Test]
        public void CompareBoards_OneBoardIsNull_ReturnsFalse()
        {
            //arrange
            Frame[,] differentBoard = null;
            //act

            //assert
            Assert.IsFalse(BoardComparer.AreBoardsEqual(_testBoard, differentBoard));
        }

        [Test]
        public void CompareBoards_DifferentBoards_ReturnsFalse()
        {
            //arrange
            Frame[,] differentBoard = new Frame[3, 3]
                    {
                        {null, null, null},
                        {new TextFrame("4", new Point(0,1)), new TextFrame("5", new Point(1,1)),  new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), new TextFrame("8", new Point(1,2)), null}
                    }; ;
            //act

            //assert
            Assert.IsFalse(BoardComparer.AreBoardsEqual(_testBoard, differentBoard));
        }
    }
}
