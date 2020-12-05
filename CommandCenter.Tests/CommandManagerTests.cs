using NUnit.Framework;
using Moq;
using GameOfFifteen.CommandCenter.Impl;
using GameOfFifteen.CommandCenter.Abstract;
using GameOfFifteen.CommandCenter.Exceptions;
using GameOfFifteen.Game.Abstract;


namespace GameOfFifteen.CommandCenter.Tests
{
    [TestFixture]
    public class CommandManagerTests
    {

        private CommandManager testedCommandManager;

        [SetUp]
        public void Setup()
        {
            Mock<IGameCreator> gameCreatorMock = new Mock<IGameCreator>(MockBehavior.Strict);
            Mock<IManipulator> manipulatorMock = new Mock<IManipulator>(MockBehavior.Strict);
            Mock<ICommandHistory> historyMock = new Mock<ICommandHistory>(MockBehavior.Strict);
            testedCommandManager = new CommandManager(gameCreatorMock.Object, manipulatorMock.Object, historyMock.Object);
        }

        [TestCase((object)new[] { "unknown" })]
        [TestCase((object)new[] { "unknown", "start", "3", "easy", "normal"})]
        [TestCase((object)new[] { "bigstringbigstringbigstringbigstringbigstringbigstringbigstringbigstringbigstringbigstringbigstring" })]
        [TestCase((object)new[] { "move", "up" })]
        [TestCase((object)new[] { ""})]
        [TestCase((object)new[] { " " })]
        [TestCase(null)]
        public void GetCommand_InvalidKeyWords_ThrowsNotExistingCommandException(string[] parameters)
        {
            //arrange
            // setup method call

            //act
            TestDelegate testDelegate = () => testedCommandManager.GetCommand(parameters);

            //assert
            Assert.Throws<NotExistingCommandException>(testDelegate);
        }

        [TestCase((object)new[] { "start" })]
        [TestCase((object)new[] { "start", "3" })]
        [TestCase((object)new[] { "start", "3", "easy" })]
        public void GetCommand_NotEnoughParametersForStartGameCommand_ThrowsNotEnoughParametersForCommandException(string[] parameters)
        {
            //arrange
            // setup method call

            //act
            TestDelegate testDelegate = () => testedCommandManager.GetCommand(parameters);

            //assert
            Assert.Throws<NotEnoughParametersForCommandException>(() => testedCommandManager.GetCommand(parameters));
        }

    }
}