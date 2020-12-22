using NUnit.Framework;
using Moq;
using GameOfFifteen.CommandCenter.Impl;
using GameOfFifteen.CommandCenter.Abstract;
using GameOfFifteen.CommandCenter.Exceptions;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.CommandCenter.Impl.Commands;
using GameOfFifteen.Game.Entities;

namespace GameOfFifteen.CommandCenter.Tests
{
    [TestFixture]
    public class CommandManagerTests
    {

        Mock<IGameCreator> _gameCreatorStub;
        Mock<IManipulator> _manipulatorStub;
        Mock<ICommandHistory> _historyStub;
        [SetUp]
        public void Setup()
        {
            _gameCreatorStub = new Mock<IGameCreator>();
            _manipulatorStub = new Mock<IManipulator>();
            _historyStub = new Mock<ICommandHistory>();
        }

        [TestCase((object)new[] { "unknown" })]
        [TestCase((object)new[] { "unknown", "start", "3", "easy", "normal"})]
        [TestCase((object)new[] { "move", "up" })]
        [TestCase((object)new[] { ""})]
        [TestCase((object)new[] { " " })]
        [TestCase(null)]
        public void GetCommand_InvalidKeyWords_ThrowsNotExistingCommandException(string[] parameters)
        {
            //arrange
            // setup method call
            CommandManager testedCommandManager = new CommandManager(_gameCreatorStub.Object, _manipulatorStub.Object, _historyStub.Object);

            //act
            TestDelegate testDelegate = () => testedCommandManager.GetCommand(parameters);

            //assert
            var ex = Assert.Throws<NotExistingCommandException>(testDelegate);
            Assert.That(ex.Message, Is.EqualTo("You entered non existing command. Try again"));
        }

        [TestCase((object)new[] { "start" })]
        [TestCase((object)new[] { "start", "3" })]
        [TestCase((object)new[] { "start", "3", "easy" })]
        public void GetCommand_NotEnoughParametersForStartGameCommand_ThrowsNotEnoughParametersForCommandException(string[] parameters)
        {
            //arrange
            CommandManager testedCommandManager = new CommandManager(_gameCreatorStub.Object, _manipulatorStub.Object, _historyStub.Object);
            //act
            TestDelegate testDelegate = () => testedCommandManager.GetCommand(parameters);

            //assert
            Assert.Throws<NotEnoughParametersForCommandException>(() => testedCommandManager.GetCommand(parameters));
            //Assert.That(ex.Message, Is.EqualTo("You entered non existing command. Try again"));
        }

        [Test]
        //every parameter value for map size except [3;9] should throw InvalidMapSizeException
        public void GetCommand_ValidMapSizeParameterForStartGameCommand_ReturnsStartGameCommand([Range(3,9)]int mapSizeParameter)
        {
            //arrange
            CommandManager testedCommandManager = new CommandManager(_gameCreatorStub.Object, _manipulatorStub.Object, _historyStub.Object);
            string[] testInput = { "start", mapSizeParameter.ToString(), "easy", "normal" };

            //act
            ICommand returnedCommand = testedCommandManager.GetCommand(testInput);

            //assert
            Assert.AreEqual(returnedCommand.GetType(), typeof(StartGameCommand));
        }

        [Test]
        //every parameter value for difficulty level except values of Level enumeration should throw InvalidLevelException
        public void GetCommand_ValidLevelParameterForStartGameCommand_ReturnsStartGameCommand([Values] Level level)
        {
            //arrange
            CommandManager testedCommandManager = new CommandManager(_gameCreatorStub.Object, _manipulatorStub.Object, _historyStub.Object);
            string[] testInput = { "start", "3", level.ToString(), "normal" };

            //act
            ICommand returnedCommand = testedCommandManager.GetCommand(testInput);

            //assert
            Assert.AreEqual(returnedCommand.GetType(), typeof(StartGameCommand));
        }

        [Test]
        //every parameter value for frame type except values of FrameType enumeration should throw InvalidFrameTypeException
        public void GetCommand_ValidFrameTypeParameterForStartGameCommand_ReturnsStartGameCommand([Values] FrameType frameType)
        {
            //arrange
            CommandManager testedCommandManager = new CommandManager(_gameCreatorStub.Object, _manipulatorStub.Object, _historyStub.Object);
            string[] testInput = { "start", "3", "easy", frameType.ToString() };

            //act
            ICommand returnedCommand = testedCommandManager.GetCommand(testInput);

            //assert
            Assert.AreEqual(returnedCommand.GetType(), typeof(StartGameCommand));
        }

        [TestCase("up")]
        [TestCase("left")]
        [TestCase("down")]
        [TestCase("right")]
        [TestCase("w")]
        [TestCase("a")]
        [TestCase("s")]
        [TestCase("d")]
        public void GetCommand_ValidParametersForMoveCommand_ReturnsMoveCommand(string direction)
        {
            //arrange
            CommandManager testedCommandManager = new CommandManager(_gameCreatorStub.Object, _manipulatorStub.Object, _historyStub.Object);
            Mock<IGame> gameStub = new Mock<IGame>();
            testedCommandManager.Game = gameStub.Object;
            string[] testInput = { direction };

            //act
            ICommand returnedCommand = testedCommandManager.GetCommand(testInput);

            //assert
            Assert.AreEqual(returnedCommand.GetType(), typeof(MoveCommand));
        }

        [TestCase("undo")]
        [TestCase("u")]
        public void GetCommand_ValidParametersForUndoCommand_ReturnsUndoCommand(string textCommand)
        {
            //arrange
            CommandManager testedCommandManager = new CommandManager(_gameCreatorStub.Object, _manipulatorStub.Object, _historyStub.Object);
            string[] testInput = { textCommand };

            //act
            ICommand returnedCommand = testedCommandManager.GetCommand(testInput);

            //assert
            Assert.AreEqual(returnedCommand.GetType(), typeof(UndoCommand));
        }
    }
}