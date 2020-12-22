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

        private Mock<IPlayfield> _playfieldStub;
        Frame[,] _testBoardBeforeMoveAttempt;
        Frame[,] _testBoard;

       [SetUp]
        public void Setup()
        {
            _playfieldStub = new Mock<IPlayfield>();
            _testBoardBeforeMoveAttempt = new Frame[3, 3]
                    {
                        {new TextFrame("1", new Point(0,0)), new TextFrame("2", new Point(1,0)), new TextFrame("3", new Point(2,0))},
                        {new TextFrame("4", new Point(0,1)), new TextFrame("5", new Point(1,1)), new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), new TextFrame("8", new Point(1,2)), null}
                    };

            _testBoard = new Frame[3, 3]
                    {
                        {new TextFrame("1", new Point(0,0)), new TextFrame("2", new Point(1,0)), new TextFrame("3", new Point(2,0))},
                        {new TextFrame("4", new Point(0,1)), new TextFrame("5", new Point(1,1)), new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), new TextFrame("8", new Point(1,2)), null}
                    };
            _playfieldStub.SetupGet(x => x.Board).Returns(_testBoardBeforeMoveAttempt);

        }

        [TestCase(Direction.Right)]
        [TestCase(Direction.Down)]
        public void MoveFrameOutOfNewPlayfield_RightAndDown_ReturnsFalse(Direction direction)
        {
            // arrange         
            Manipulator testManipulator = Manipulator.GetInstance();

            // act
            bool testResult = testManipulator.MakeMove(_playfieldStub.Object, direction, false);

            // assert
            Assert.IsFalse(testResult); 
        }

        [TestCase(Direction.Right)]
        [TestCase(Direction.Down)]
        public void MoveFrameOutOfNewPlayfield_RightAndDown_BoardUnchanged(Direction direction)
        {
            // arrange
            Manipulator testManipulator = Manipulator.GetInstance();

            // act
            bool testResult = testManipulator.MakeMove(_playfieldStub.Object, direction, false);

            // assert
            Assert.IsTrue(BoardComparer.AreBoardsEqual(_testBoard, _playfieldStub.Object.Board));
            //Assert.AreEqual(_testBoard, _playfieldStub.Object.Board);
        }

        [TestCase(Direction.Right)]
        [TestCase(Direction.Down)]
        public void MoveFrameOutOfNewPlayfield_RightAndDown_NoPlayfieldChangedEventInvokation(Direction direction)
        {
            // arrange           
            Manipulator testManipulator = Manipulator.GetInstance();

            bool wasEventCalled = false;
            testManipulator.NotifyOnPlayfieldChange += (board) => wasEventCalled = true;
            // act
            bool testResult = testManipulator.MakeMove(_playfieldStub.Object, direction, false);

            // assert
            Assert.IsFalse(wasEventCalled);
        }

        [TestCase(Direction.Left)]
        [TestCase(Direction.Up)]
        public void MoveFrameOnNewPlayfield_UpAndLeft_ReturnsTrue(Direction direction)
        {
            // arrange
            
            // act
            bool testResult = Manipulator.GetInstance().MakeMove(_playfieldStub.Object, direction, false);

            // assert
            Assert.IsTrue(testResult);
        }

        [TestCase(Direction.Left)]
        [TestCase(Direction.Up)]
        public void MoveFrameOnNewPlayfield_UpAndLeft_BoardChanges(Direction direction)
        {
            // arrange
            
            // act
            bool testResult = Manipulator.GetInstance().MakeMove(_playfieldStub.Object, direction, false);

            // assert
            Assert.IsFalse(BoardComparer.AreBoardsEqual(_testBoard, _testBoardBeforeMoveAttempt));
            //Assert.AreNotEqual(_testBoard, _testBoardBeforeMoveAttempt);

        }

        [TestCase(Direction.Left)]
        [TestCase(Direction.Up)]
        public void MoveFrameOnNewPlayfield_UpAndLeft_PlayfieldChangedEventInvokation(Direction direction)
        {
            // arrange
            bool wasEventCalled = false;

            Manipulator testManipulator = Manipulator.GetInstance();
            testManipulator.NotifyOnPlayfieldChange += (board) => wasEventCalled = true;

            // act
            bool testResult = Manipulator.GetInstance().MakeMove(_playfieldStub.Object, direction, false);

            // assert
            Assert.IsTrue(wasEventCalled);
        }

        [TestCaseSource("MoveEmptyFrameDirectionAndStartBoardAndExpectedBoardTestCases")]
        public void MoveFrameOnPlayfield_InDirection_MatchesExpectedResult(Direction direction, Frame[,] startBoard, Frame[,] expectedBoard)
        {
            // arrange           
            _playfieldStub.SetupGet(x => x.Board).Returns(startBoard);

            // act
            bool testResult = Manipulator.GetInstance().MakeMove(_playfieldStub.Object, direction, false);

            // assert
            Assert.IsTrue(BoardComparer.AreBoardsEqual(startBoard, expectedBoard));
            //Assert.AreEqual(startBoard, expectedBoard);

        }
    }
}
