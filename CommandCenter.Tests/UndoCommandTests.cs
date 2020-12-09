using GameOfFifteen.CommandCenter.Abstract;
using GameOfFifteen.CommandCenter.Impl.Commands;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfFifteen.CommandCenter.Tests
{
    [TestFixture]
    public class UndoCommandTests
    {
        [Test]
        public void ExecuteCommand_NoParams_ExpectedResult()
        {
            // arrange
            Mock<ICommandHistory> _historyMock = new Mock<ICommandHistory>();

            UndoCommand testUndoCommand = new UndoCommand(_historyMock.Object);
            // act

            testUndoCommand.Execute();
            // assert
            _historyMock.Verify(h => h.Undo(), Times.Once());
        }

    }
}
