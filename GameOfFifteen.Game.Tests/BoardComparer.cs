using GameOfFifteen.Game.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfFifteen.Game.Tests
{
    public static class BoardComparer
    {
        public static bool AreBoardsEqual(Frame[,] testBoard, Frame[,] expectedBoard)
        {
            if (testBoard == null && expectedBoard == null)
                return true;
            if (testBoard == null || expectedBoard == null)
                return false;

            for (int i = 0; i < testBoard.GetLength(0); i++)
            {
                for (int j = 0; j < testBoard.GetLength(1); j++)
                {
                    if (testBoard[i, j]?.Content != expectedBoard[i, j]?.Content ||
                        testBoard[i, j]?.FinishPoint != expectedBoard[i, j]?.FinishPoint)
                    {
                        return false;
                    }

                }
            }
            return true;
        }

    }
}
