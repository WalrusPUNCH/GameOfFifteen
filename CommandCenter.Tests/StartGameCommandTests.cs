using GameOfFifteen.CommandCenter.Abstract;
using GameOfFifteen.CommandCenter.Impl.Commands;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Entities;
using GameOfFifteen.Game.Impl;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfFifteen.CommandCenter.Tests
{
    [TestFixture]
    public class StartGameCommandTests
    {
        private GameSettings _defaultSettings;
        private Mock<IManipulator> _manipulatorMock;
        private Mock<IGameCreator> _gameCreatorMock;

        [SetUp]
        public void Setup()
        {
            _defaultSettings = new GameSettings(3, Level.Easy, FrameType.Normal, false);
            _manipulatorMock = new Mock<IManipulator>();
            _gameCreatorMock = new Mock<IGameCreator>();
        }

        [Test]
        public void StartNewGame_ValidGameSettings_GameCreated()
        {
            // arrange
            StartGameCommand testStartCommand = new StartGameCommand(_gameCreatorMock.Object, _manipulatorMock.Object, _defaultSettings);

            // act
            testStartCommand.Execute();

            // assert
            _gameCreatorMock.Verify(c => c.CreateGame(_defaultSettings, _manipulatorMock.Object), Times.Once());

        }
    }
}
