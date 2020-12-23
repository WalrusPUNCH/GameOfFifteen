using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Entities;
using GameOfFifteen.Game.Impl;
using GameOfFifteen.Game.Impl.FrameCreation.ConcreteFrames;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameOfFifteen.Game.Tests
{
    [TestFixture]
    public class GameCreatorTests
    {
        private GameSettings _defaultSettings;
        private Mock<IManipulator> _manipulatorMock;
        private Mock<IPlayfield> _playfieldStub;
        private Frame[,] _defaultBoard3x3;

        [SetUp]
        public void Setup()
        {
            _defaultSettings = new GameSettings(3, Level.Easy, FrameType.Normal, false);
            _defaultBoard3x3 = new Frame[3, 3]
                    {
                        {new TextFrame("1", new Point(0,0)), new TextFrame("2", new Point(1,0)), new TextFrame("3", new Point(2,0))},
                        {new TextFrame("4", new Point(0,1)), new TextFrame("5", new Point(1,1)), new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), new TextFrame("8", new Point(1,2)), null}
                    };

            _playfieldStub = new Mock<IPlayfield>();
            _playfieldStub.SetupGet(p => p.Board).Returns(_defaultBoard3x3);

            _manipulatorMock = new Mock<IManipulator>();

        }

        [Test]
        public void CreateGame_VadidSettings_GameCreatedEventInvokation()
        {
            // arrange
            GameCreator testGameCreator = new GameCreator();
            bool wasEventCalled = false;
            IGame newCreatedGame = null;
            testGameCreator.NotifyOnGameCreated += (newGame) => { wasEventCalled = true; newCreatedGame = newGame; };

            // act
            testGameCreator.CreateGame(_defaultSettings, _manipulatorMock.Object);

            // assert
            _manipulatorMock.Verify(m => m.ShuffleBoard(newCreatedGame.Playfield, _defaultSettings.Level), Times.Once());
            Assert.IsTrue(wasEventCalled);
        }

    }
}
