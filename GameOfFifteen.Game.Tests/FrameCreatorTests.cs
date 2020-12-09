using NUnit.Framework;
using Moq;

using GameOfFifteen.Game.Entities;
using GameOfFifteen.Game.Impl.FrameCreation.ConcreteFrameCreators;
using System.Collections.Generic;
using System.Drawing;
using GameOfFifteen.Game.Impl.FrameCreation.ConcreteFrames;
using GameOfFifteen.Game.Impl.FrameCreation;

namespace GameOfFifteen.Game.Tests
{
    [TestFixture]
    class FrameCreatorTests
    {
        public static IEnumerable<TestCaseData> BoardSizeAndExpectedResultTestCases
        {
            get
            {
                yield return new TestCaseData(3,
                    new Frame[3, 3]
                    {
                        {new TextFrame("1", new Point(0,0)), new TextFrame("2", new Point(1,0)), new TextFrame("3", new Point(2,0))},
                        {new TextFrame("4", new Point(0,1)), new TextFrame("5", new Point(1,1)), new TextFrame("6", new Point(2,1))},
                        {new TextFrame("7", new Point(0,2)), new TextFrame("8", new Point(1,2)), null}
                    });

                yield return new TestCaseData(4,
                    new Frame[4, 4]
                    {
                        {new TextFrame("1", new Point(0,0)), new TextFrame("2", new Point(1,0)), new TextFrame("3", new Point(2,0)), new TextFrame("4", new Point(3,0))},
                        {new TextFrame("5", new Point(0,1)), new TextFrame("6", new Point(1,1)), new TextFrame("7", new Point(2,1)), new TextFrame("8", new Point(3,1))},
                        {new TextFrame("9", new Point(0,2)), new TextFrame("10", new Point(1,2)), new TextFrame("11", new Point(2,2)), new TextFrame("12", new Point(3,2))},
                        {new TextFrame("13", new Point(0,3)), new TextFrame("14", new Point(1,3)), new TextFrame("15", new Point(2,3)), null},
                    });
            }
        }

        [Test]
        public void FrameCreatorBoardCreation_BoardSize_SizeOfReturnedArrayMatchesInputSize([Range(3,9)] int boardSize)
        {
            // arrange
            FrameCreator frameCreator = new TextFrameCreator();

            // act
            Frame[,] testResult = frameCreator.CreateBoard(boardSize);

            // assert
            Assert.IsTrue(testResult.GetLength(0) == boardSize);
            Assert.IsTrue(testResult.GetLength(1) == boardSize);           
        }

        [TestCaseSource("BoardSizeAndExpectedResultTestCases")]
        public void FrameCreatorBoardCreation_ValidSettings_ReturnValueMatchesExpectedValue(int boardSize, Frame[,] expectedResult)
        {
            // arrange
            FrameCreator frameCreator = new TextFrameCreator();

            // act
            Frame[,] testResult = frameCreator.CreateBoard(boardSize);

            // assert
            Assert.IsTrue(BoardComparer.AreBoardsEqual(testResult, expectedResult));
        }

    }
}
