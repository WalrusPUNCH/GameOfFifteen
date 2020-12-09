using GameOfFifteen.CommandCenter.Abstract;
using GameOfFifteen.CommandCenter.Impl.Commands;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Entities;
using GameOfFifteen.Game.Impl.FrameCreation.ConcreteFrames;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;

namespace GameOfFifteen.CommandCenter.Tests
{
    [TestFixture]
    public class MoveCommandTests
    {
        private GameSettings _defaultSettings;
        private Mock<IPlayfield> _playfieldStub;
        private Mock<IGame> _gameMock;
        private Mock<IManipulator> _manipulatorMock;
        private Mock<ICommandHistory> _historyMock;

        public static IEnumerable<TestCaseData> ValidMoveDirectionAndStartingBoardTestCases
        {
            get
            {
                yield return new TestCaseData(Direction.Up,
                    new Frame[3, 3]
                    {
                        {new TextFrame("1", new Point(0,0)), new TextFrame("2", new Point(1,0)), new TextFrame("3", new Point(2,0))},
                        {new TextFrame("4", new Point(0,1)), null, new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), new TextFrame("5", new Point(1,1)), new TextFrame("8", new Point(1,2))}
                    });

                yield return new TestCaseData(Direction.Down,
                    new Frame[3, 3]
                    {
                        {new TextFrame("1", new Point(0,0)), new TextFrame("2", new Point(1,0)), new TextFrame("3", new Point(2,0))},
                        {new TextFrame("4", new Point(0,1)), null, new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), new TextFrame("5", new Point(1,1)), new TextFrame("8", new Point(1,2))}
                    });
            }
        }


        public static IEnumerable<TestCaseData> InvalidMoveDirectionAndStartingBoardTestCases
        {
            get
            {
                yield return new TestCaseData(Direction.Up,
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
                        {new TextFrame("4", new Point(0,1)), new TextFrame("5", new Point(1,1)), new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), null, new TextFrame("8", new Point(1,2))}
                    });
            }
        }

        [SetUp]
        public void Setup()
        {
            _defaultSettings = new GameSettings(3, Level.Easy, FrameType.Normal, false);

            _playfieldStub = new Mock<IPlayfield>();
            _gameMock = new Mock<IGame>();
            _manipulatorMock = new Mock<IManipulator>();
            _historyMock = new Mock<ICommandHistory>();
        }

        [TestCaseSource("ValidMoveDirectionAndStartingBoardTestCases")]
        public void CommandExecution_ValidMove_CommandSavedAndMoveMadeAndMovesIncrementedAndCheckedIsSolved(Direction direction, Frame[,] board)
        {
            // arrange
            _playfieldStub.SetupGet(p => p.Board).Returns(board);

            _gameMock.SetupGet(g => g.Playfield).Returns(_playfieldStub.Object);
            _gameMock.SetupGet(g => g.Settings).Returns(_defaultSettings);

            _manipulatorMock.Setup(m => m.MakeMove(_playfieldStub.Object, direction, false)).Returns(true);

            MoveCommand testCommand = new MoveCommand(_gameMock.Object, direction, _manipulatorMock.Object, _historyMock.Object);

            // act
            testCommand.Execute();

            // assert

            _gameMock.Verify(g => g.SaveGameMemento(), Times.Once());
            _gameMock.Verify(g => g.IncrementMovesNumber(), Times.Once());
            _gameMock.Verify(g => g.IsSolved(), Times.Once());
            _historyMock.Verify(h => h.SaveCommand(testCommand), Times.Once());
            _manipulatorMock.Verify(m => m.MakeMove(_playfieldStub.Object, direction, false), Times.Once());
        }


        [TestCaseSource("InvalidMoveDirectionAndStartingBoardTestCases")]
        public void CommandExecution_InvalidMove_NoAction(Direction direction, Frame[,] board)
        {
            // arrange
            _playfieldStub.SetupGet(p => p.Board).Returns(board);

            _gameMock.SetupGet(g => g.Playfield).Returns(_playfieldStub.Object);
            _gameMock.SetupGet(g => g.Settings).Returns(_defaultSettings);

            _manipulatorMock.Setup(m => m.MakeMove(_playfieldStub.Object, direction, false)).Returns(false);

            MoveCommand testCommand = new MoveCommand(_gameMock.Object, direction, _manipulatorMock.Object, _historyMock.Object);

            // act
            testCommand.Execute();

            // assert
            _gameMock.Verify(g => g.SaveGameMemento(), Times.Once());
            _manipulatorMock.Verify(m => m.MakeMove(_playfieldStub.Object, direction, false), Times.Once());
        }

    }
}
