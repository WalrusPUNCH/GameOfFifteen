using NUnit.Framework;
using Moq;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Impl;
using GameOfFifteen.Game.Entities;
using System.Collections.Generic;
using System.Linq;
using GameOfFifteen.Game.Impl.FrameCreation.ConcreteFrames;
using System.Drawing;

namespace GameOfFifteen.Game.Tests
{
    [TestFixture]
    public class ManipulatorTests
    {

        public static IEnumerable<TestCaseData> MoveEmptyFrameDirectionAndStartBoardAndExpectedBoardTestCases
        {
            get
            {
                yield return new TestCaseData(Direction.Up,
                    new Frame[3, 3]
                    {
                        {new TextFrame("1", new Point(0,0)), new TextFrame("2", new Point(1,0)), new TextFrame("3", new Point(2,0))},
                        {new TextFrame("4", new Point(0,1)), null, new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), new TextFrame("5", new Point(1,1)), new TextFrame("8", new Point(1,2))}
                    },
                    new Frame[3, 3]
                    {
                        {new TextFrame("1", new Point(0,0)), null, new TextFrame("3", new Point(2,0))},
                        {new TextFrame("4", new Point(0,1)), new TextFrame("2", new Point(1,0)), new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), new TextFrame("5", new Point(1,1)), new TextFrame("8", new Point(1,2))}
                    });

                yield return new TestCaseData(Direction.Down,
                    new Frame[3, 3]
                    {
                        {new TextFrame("1", new Point(0,0)), new TextFrame("2", new Point(1,0)), new TextFrame("3", new Point(2,0))},
                        {new TextFrame("4", new Point(0,1)), null, new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), new TextFrame("5", new Point(1,1)), new TextFrame("8", new Point(1,2))}
                    },
                    new Frame[3, 3]
                    {
                         {new TextFrame("1", new Point(0,0)), new TextFrame("2", new Point(1,0)), new TextFrame("3", new Point(2,0))},
                        {new TextFrame("4", new Point(0,1)), new TextFrame("5", new Point(1,1)), new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), null, new TextFrame("8", new Point(1,2))}
                    });

                yield return new TestCaseData(Direction.Left,
                    new Frame[3, 3]
                    {
                        {new TextFrame("1", new Point(0,0)), new TextFrame("2", new Point(1,0)), new TextFrame("3", new Point(2,0))},
                        {new TextFrame("4", new Point(0,1)), null, new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), new TextFrame("5", new Point(1,1)), new TextFrame("8", new Point(1,2))}
                    },
                    new Frame[3, 3]
                    {
                        {new TextFrame("1", new Point(0,0)), new TextFrame("2", new Point(1,0)), new TextFrame("3", new Point(2,0))},
                        {null, new TextFrame("4", new Point(0,1)), new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), new TextFrame("5", new Point(1,1)), new TextFrame("8", new Point(1,2))}
                    });

                yield return new TestCaseData(Direction.Right,
                    new Frame[3, 3]
                    {
                        {new TextFrame("1", new Point(0,0)), new TextFrame("2", new Point(1,0)), new TextFrame("3", new Point(2,0))},
                        {new TextFrame("4", new Point(0,1)), null, new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), new TextFrame("5", new Point(1,1)), new TextFrame("8", new Point(1,2))}
                    },
                    new Frame[3, 3]
                    {
                        {new TextFrame("1", new Point(0,0)), new TextFrame("2", new Point(1,0)), new TextFrame("3", new Point(2,0))},
                        {new TextFrame("4", new Point(0,1)), new TextFrame("6", new Point(2,1)), null},
                        {new TextFrame("7", new Point(0,2)), new TextFrame("5", new Point(1,1)), new TextFrame("8", new Point(1,2))}
                    });


            }
        }

        [TestCase(Direction.Right)]
        [TestCase(Direction.Down)]
        public void MoveFrameOutOfNewPlayfield_RightAndDown_ReturnsFalse(Direction direction)
        {
            // arrange
            Frame[,] testBoard = new Frame[3, 3]
                    {
                        {new TextFrame("1", new Point(0,0)), new TextFrame("2", new Point(1,0)), new TextFrame("3", new Point(2,0))},
                        {new TextFrame("4", new Point(0,1)), new TextFrame("5", new Point(1,1)), new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), new TextFrame("8", new Point(1,2)), null}
                    };
            Mock<IPlayfield> playfieldStub = new Mock<IPlayfield>();
            playfieldStub.SetupGet(x => x.Board).Returns(testBoard);

            // act
            bool testResult = Manipulator.GetInstance().MakeMove(playfieldStub.Object, direction, false);
            // assert
            Assert.IsFalse(testResult);
        }

        [TestCase(Direction.Left)]
        [TestCase(Direction.Up)]
        public void MoveFrameOnNewPlayfield_UpAndLeft_ReturnsTrue(Direction direction)
        {
            // arrange
            Frame[,] testBoard = new Frame[3, 3]
                    {
                        {new TextFrame("1", new Point(0,0)), new TextFrame("2", new Point(1,0)), new TextFrame("3", new Point(2,0))},
                        {new TextFrame("4", new Point(0,1)), new TextFrame("5", new Point(1,1)), new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), new TextFrame("8", new Point(1,2)), null}
                    };
            Mock<IPlayfield> playfieldStub = new Mock<IPlayfield>();
            playfieldStub.SetupGet(x => x.Board).Returns(testBoard);

            // act
            bool testResult = Manipulator.GetInstance().MakeMove(playfieldStub.Object, direction, false);
            // assert
            Assert.IsTrue(testResult);
        }

        [TestCaseSource("MoveEmptyFrameDirectionAndStartBoardAndExpectedBoardTestCases")]
        public void MoveFrameOnPlayfield_ToTheLeft_MatchesExpectedResult(Direction direction, Frame[,] startBoard, Frame[,] expectedBoard)
        {
            // arrange           
            Mock<IPlayfield> testplayfieldStub = new Mock<IPlayfield>();
            testplayfieldStub.SetupGet(x => x.Board).Returns(startBoard);

            // act
            bool testResult = Manipulator.GetInstance().MakeMove(testplayfieldStub.Object, direction, false);

            // assert
            Assert.IsTrue(BoardComparer.AreBoardsEqual(startBoard, expectedBoard));
        }
    }
}
