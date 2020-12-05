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


        [TestCase((object)new [] { "unknown", "command" })]
        [TestCase((object)new[] { "unknownygdsyofhfbhdsyuhg73yr27838t8wtfh8he0t8ewg87fhg8h87gtf6d5tfyuihgyft54deftygbunubygf6y" })]
        [TestCase((object)new[] { "unknown", "command", "unknown", "command", "unknown", "command" , "unknown"})]
        [TestCase((object)new[] { "start", "xsize", "easy", })]
        public void GetCommand_InvalidKeyWords_ReturnsNull(string[] parameters)
        {
            //arrange
                // setup method call
            //act
            var returnedCommand = testedCommandManager.GetCommand(parameters);

            //assert
            Assert.Null(returnedCommand, null);
        }


        
    }
}