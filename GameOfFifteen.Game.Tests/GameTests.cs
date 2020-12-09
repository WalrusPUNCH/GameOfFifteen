using NUnit.Framework;
using Moq;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Impl;
using GameOfFifteen.Game.Entities;
using System.Collections.Generic;
using GameOfFifteen.Game.Impl.FrameCreation.ConcreteFrames;
using System.Drawing;

namespace GameOfFifteen.Game.Tests
{
    public class GameTests
    {
        public static IEnumerable<TestCaseData> SolvedBoardsTestCases
        {
            get
            {
                yield return new TestCaseData(new Frame[3, 3]
                    {
                        {new TextFrame("1", new Point(0,0)), new TextFrame("2", new Point(1,0)), new TextFrame("3", new Point(2,0))},
                        {new TextFrame("4", new Point(0,1)), new TextFrame("5", new Point(1,1)), new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), new TextFrame("8", new Point(1,2)), null}
                    });

                yield return new TestCaseData(new Frame[4, 4]
                    {
                        {new TextFrame("1", new Point(0,0)), new TextFrame("2", new Point(1,0)), new TextFrame("3", new Point(2,0)), new TextFrame("4", new Point(3,0))},
                        {new TextFrame("5", new Point(0,1)), new TextFrame("6", new Point(1,1)), new TextFrame("7", new Point(2,1)), new TextFrame("8", new Point(3,1))},
                        {new TextFrame("9", new Point(0,2)), new TextFrame("10", new Point(1,2)), new TextFrame("11", new Point(2,2)), new TextFrame("12", new Point(3,2))},
                        {new TextFrame("13", new Point(0,3)), new TextFrame("14", new Point(1,3)), new TextFrame("15", new Point(2,3)), null},
                    });
            }
        }

        public static IEnumerable<TestCaseData> UnsolvedBoardsTestCases
        {
            get
            {
                yield return new TestCaseData(new Frame[3, 3]
                    {
                        { new TextFrame("2", new Point(1,0)), new TextFrame("1", new Point(0,0)), new TextFrame("3", new Point(2,0))},
                        {new TextFrame("4", new Point(0,1)), new TextFrame("5", new Point(1,1)), new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), new TextFrame("8", new Point(1,2)), null}
                    });

                yield return new TestCaseData(new Frame[4, 4]
                    {
                        {new TextFrame("2", new Point(1,0)), new TextFrame("1", new Point(0,0)), new TextFrame("3", new Point(2,0)), new TextFrame("4", new Point(3,0))},
                        {new TextFrame("5", new Point(0,1)), new TextFrame("6", new Point(1,1)), new TextFrame("7", new Point(2,1)), new TextFrame("8", new Point(3,1))},
                        {new TextFrame("9", new Point(0,2)), new TextFrame("10", new Point(1,2)), new TextFrame("11", new Point(2,2)), new TextFrame("12", new Point(3,2))},
                        {new TextFrame("13", new Point(0,3)), new TextFrame("14", new Point(1,3)), new TextFrame("15", new Point(2,3)), null},
                    });
            }
        }

        private GameSettings _defaultGameSettings;
        private GameSettings _testGameSettings;
        [SetUp]
        public void Setup()
        {
            _defaultGameSettings = new GameSettings(3, Level.Easy, FrameType.Normal, false);
            _testGameSettings = new GameSettings(5, Level.Hard, FrameType.Boarded, true);
        }
        

        [Test]
        public void RestoreMovesFromMemento_15_MovesInGameRestoredFromMemento()
        {
            // arrange
            Mock<IPlayfield> playfieldStub = new Mock<IPlayfield>();
            GameMemento testMemento = new GameMemento(_testGameSettings, 15, playfieldStub.Object);
            IGame testedGame = new Impl.Game(_defaultGameSettings);

            // act
            testedGame.RestoreFromMemento(testMemento);

            // assert
            Assert.That(testedGame.Moves, Is.EqualTo(testMemento.Moves));
        }

        [Test]
        public void RestoreSettingsFromMemento_TestSettings_SettingsInGameRestoredFromMemento()
        {
            // arrange
            Mock<IPlayfield> playfieldStub = new Mock<IPlayfield>();
            GameMemento testMemento = new GameMemento(_testGameSettings, 15, playfieldStub.Object);
            IGame testedGame = new Impl.Game(_defaultGameSettings);

            // act
            testedGame.RestoreFromMemento(testMemento);

            // assert
            Assert.That(testedGame.Settings, Is.EqualTo(testMemento.Settings));
        }

        [TestCaseSource("UnsolvedBoardsTestCases")]
        public void RestorePlayfieldFromMemento_TestPlayfield_PlayfieldInGameRestoredFromMemento(Frame[,] unsolvedBoard)
        {
            // arrange
            Mock<IPlayfield> playfieldStub = new Mock<IPlayfield>();
            playfieldStub.SetupGet(x => x.Board).Returns(unsolvedBoard);
            GameMemento mementoStub = new GameMemento(_defaultGameSettings, 15, playfieldStub.Object);

            IGame testedGame = new Impl.Game(_defaultGameSettings);

            // act
            testedGame.RestoreFromMemento(mementoStub);

            // assert
            Assert.That(testedGame.Playfield, Is.EqualTo(mementoStub.Playfield));
        }

        [TestCaseSource("SolvedBoardsTestCases")]
        public void IsSolved_SolvedPlayfield_ReturnsTrue(Frame[,] solvedBoard)
        {
            // arrange
            IGame testedGame = new Impl.Game(_defaultGameSettings);

            // act
            bool isTestPlayfieldSolved = testedGame.IsSolved();

            // assert
            Assert.IsTrue(isTestPlayfieldSolved);
        }

        [Test]
        public void IsSolved_SolvedPlayfield_ReturnsFalse()
        {
            // arrange
            IGame testedGame = new Impl.Game(_defaultGameSettings);
            Manipulator.GetInstance().MakeMove(testedGame.Playfield, Direction.Up, false);
            // act
            bool isTestPlayfieldSolved = testedGame.IsSolved();

            // assert
            Assert.IsFalse(isTestPlayfieldSolved);
        }

        [Test]
        public void IncrementMovesNumber_NumberBeforeIncrementation_ReturnsNumberBeforeIncrementationPlusOne()
        {
            // arrange

            IGame testedGame = new Impl.Game(_defaultGameSettings);
            int movesBefore = testedGame.Moves;
            int expectedValue = movesBefore + 1;

            // act
            testedGame.IncrementMovesNumber();

            // assert
            Assert.AreEqual(expectedValue, testedGame.Moves);
        }

    }
}