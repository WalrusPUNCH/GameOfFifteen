using NUnit.Framework;
using Moq;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Impl;
using GameOfFifteen.Game.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GameOfFifteen.Game.Tests
{
    [TestFixture]
    public class ManipulatorTests
    {
        [TestCase(Direction.Right)]
        [TestCase(Direction.Down)]
        public void MoveFrameOutOfNewPlayfield_RightAndDown_ReturnsFalse(Direction direction)
        {
            // arrange
            IPlayfield playfield = new Playfield(FrameType.Normal, 3);

            // act
            bool testResult = Manipulator.GetInstance().MakeMove(playfield, direction, false);
            // assert
            Assert.IsFalse(testResult);
        }

        [TestCase(Direction.Left)]
        [TestCase(Direction.Up)]
        public void MoveFrameOnNewPlayfield_UpAndLeft_ReturnsTrue(Direction direction)
        {
            // arrange
            IPlayfield playfield = new Playfield(FrameType.Normal, 3);

            // act
            bool testResult = Manipulator.GetInstance().MakeMove(playfield, direction, false);
            // assert
            Assert.IsTrue(testResult);
        }

        [Test]
        public void MoveFrameOnNewPlayfield_ToTheLeft_MatchesExpectedResult()
        {
            // arrange
            IPlayfield playfield = new Playfield(FrameType.Normal, 3);
            var temp = playfield.Board[2, 1];
            playfield.Board[2, 1] = playfield.Board[2, 2];
            playfield.Board[2, 2] = temp;
            IPlayfield testPlayfield = new Playfield(FrameType.Normal, 3);

            // act
            bool testResult = Manipulator.GetInstance().MakeMove(testPlayfield, Direction.Left, false);
            // assert

            Assert.IsTrue(testResult);

            for (int i = 0; i < playfield.Board.GetLength(0); i++)
            {
                for (int j = 0; j < playfield.Board.GetLength(1); j++)
                {
                    if (playfield.Board[i, j] == null && testPlayfield.Board[i, j] == null)
                        continue;
                    Assert.IsTrue(playfield.Board[i, j].Content == testPlayfield.Board[i, j].Content);
                }
            }
        }

    }
}
