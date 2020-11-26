using System.Drawing;
using GameOfFifteen.Game.Entities;

namespace GameOfFifteen.Game.Impl.FrameCreation
{
    public abstract class FrameCreator
    {
        public abstract Frame CreateFrame(string content, Point finishPoint);

        public Frame[,] CreateBoard(int size)
        {
            Frame[,] board = new Frame[size, size];
            int x = 0;
            int y = 0;
            int number = 1;
            for (int i=0; i<= board.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = CreateFrame(number.ToString(), new Point(x, y));
                    x++;
                    number++;
                }
                y++;
                x = 0;
            }
            board[size - 1, size - 1] = null;
            return board;
        }
    }
}