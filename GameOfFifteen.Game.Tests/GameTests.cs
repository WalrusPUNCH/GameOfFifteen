using NUnit.Framework;
using Moq;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Impl;
using GameOfFifteen.Game.Entities;
using System.Collections.Generic;

namespace GameOfFifteen.Game.Tests
{
    public class GameTests
    {
        public static IEnumerable<TestCaseData> GameSettingsTestCases
        {
            get
            {
                yield return new TestCaseData(new GameSettings(3, Level.Easy, FrameType.Normal, false));
                yield return new TestCaseData(new GameSettings(4, Level.Easy, FrameType.Normal, false));
                yield return new TestCaseData(new GameSettings(5, Level.Easy, FrameType.Normal, false));
                yield return new TestCaseData(new GameSettings(6, Level.Easy, FrameType.Normal, false));
                yield return new TestCaseData(new GameSettings(7, Level.Easy, FrameType.Normal, false));
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
            Mock<IPlayfield> playfieldStub = new Mock<IPlayfield>(MockBehavior.Strict);
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
            Mock<IPlayfield> playfieldStub = new Mock<IPlayfield>(MockBehavior.Strict);
            GameMemento testMemento = new GameMemento(_testGameSettings, 15, playfieldStub.Object);
            IGame testedGame = new Impl.Game(_defaultGameSettings);

            // act
            testedGame.RestoreFromMemento(testMemento);

            // assert
            Assert.That(testedGame.Settings, Is.EqualTo(testMemento.Settings));
        }

        [Test]
        public void RestorePlayfieldFromMemento_TestPlayfield_PlayfieldInGameRestoredFromMemento()
        {
            // arrange
            IPlayfield testPlayfield = new Playfield(FrameType.Normal, 3);
            testPlayfield.Board[0, 0] = testPlayfield.Board[1, 1];

            GameMemento testMemento = new GameMemento(_testGameSettings, 15, testPlayfield);
            IGame testedGame = new Impl.Game(_defaultGameSettings);

            // act
            testedGame.RestoreFromMemento(testMemento);

            // assert
            Assert.That(testedGame.Playfield, Is.EqualTo(testMemento.Playfield));
        }

        [TestCaseSource("GameSettingsTestCases")]
        public void IsSolved_SolvedPlayfield_ReturnsTrue(GameSettings testGameSettings)
        {
            // arrange
            
            IGame testedGame = new Impl.Game(testGameSettings);

            // act
            bool isTestPlayfieldSolved = testedGame.IsSolved();

            // assert
            Assert.IsTrue(isTestPlayfieldSolved);
        }

    }
}