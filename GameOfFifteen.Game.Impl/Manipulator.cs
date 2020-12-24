using System;
using System.Drawing;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Entities;

namespace GameOfFifteen.Game.Impl
{
    public class Manipulator : IManipulator
    {
        public event IRedrawNotificator.RedrawField NotifyOnPlayfieldChange;

        private static Manipulator _instance;
        public Manipulator()
        {
            
        }
        
        public static Manipulator GetInstance()
        {
            if (_instance == null)
                _instance = new Manipulator();
            return _instance;
        }
        

        public void ShuffleBoard(IPlayfield playfield, Level level)
        {
            switch (level)
            {
                case Level.Easy:
                    MakeRandomMoves(playfield, 5);
                    break;
                
                case Level.Medium:
                    MakeRandomMoves(playfield, 20);
                    break;
                
                case  Level.Hard:
                    MakeRandomMoves(playfield, 100);
                    break;
            }
            NotifyOnPlayfieldChange?.Invoke(playfield.Board);
        }

        public bool MakeMove(IPlayfield playfield, Direction direction, bool withRandomMoves)
        {
            bool isMoveValid = MoveEmptyFrame(playfield, direction);
            if (isMoveValid)
            {
                if (withRandomMoves)
                {
                    Random random = new Random();
                    MakeRandomMoves(playfield, random.Next(1, 6)); // [1;6)
                }
                NotifyOnPlayfieldChange?.Invoke(playfield.Board);
            }
            return isMoveValid;
        }


        private bool MoveEmptyFrame(IPlayfield playfield, Direction direction)
        {
            bool isMoveValid = false;
            Point emptyFrameLocation = FindEmptyFrame(playfield);
            Frame[,] board = playfield.Board;
            switch (direction)
            {
                case Direction.Up:
                    if (emptyFrameLocation.Y < 1)
                        break;
                    var temp = board[emptyFrameLocation.Y - 1, emptyFrameLocation.X]; // frame above the empty frame
                    board[emptyFrameLocation.Y - 1, emptyFrameLocation.X] = null;
                    board[emptyFrameLocation.Y, emptyFrameLocation.X] = temp;
                    isMoveValid = true;
                    break;
                
                case Direction.Right:
                    if (emptyFrameLocation.X == board.GetLength(0) - 1)
                        break;
                    var temp1 = board[emptyFrameLocation.Y, emptyFrameLocation.X + 1]; // frame right to the empty frame
                    board[emptyFrameLocation.Y , emptyFrameLocation.X + 1] = null;
                    board[emptyFrameLocation.Y, emptyFrameLocation.X] = temp1;
                    isMoveValid = true;
                    break;
                
                case Direction.Down:
                    if (emptyFrameLocation.Y == board.GetLength(1) - 1)
                        break;
                    var temp2 = board[emptyFrameLocation.Y + 1, emptyFrameLocation.X]; // frame under the empty frame
                    board[emptyFrameLocation.Y + 1, emptyFrameLocation.X] = null;
                    board[emptyFrameLocation.Y, emptyFrameLocation.X] = temp2;
                    isMoveValid = true;
                    break;

                case Direction.Left:
                    if (emptyFrameLocation.X < 1)
                        break;
                    var temp3 = board[emptyFrameLocation.Y, emptyFrameLocation.X - 1]; // frame left to the empty frame
                    board[emptyFrameLocation.Y, emptyFrameLocation.X - 1] = null;
                    board[emptyFrameLocation.Y, emptyFrameLocation.X] = temp3;
                    isMoveValid = true;
                    break;
                
            }
            
            return isMoveValid;
        }

        private Point FindEmptyFrame(IPlayfield playfield)
        {
            Point emptyFrame = new Point();
            for (int i = 0; i < playfield.Board.GetLength(0); i++)
            {
                for (int j = 0; j < playfield.Board.GetLength(1); j++)
                {
                    if (playfield.Board[i, j] == null)
                    {
                        emptyFrame.X = j;
                        emptyFrame.Y = i;
                    }
                }
            }

            return emptyFrame;
        }
        private void MakeRandomMoves(IPlayfield playfield, int numberOfMoves)
        {
            Random random = new Random();
            var values = Enum.GetValues(typeof(Direction));
            for (int i = 0; i < numberOfMoves; i++)
            {
                int index = random.Next(values.Length);
                Direction randomDirection = (Direction) values.GetValue(index);
                bool isMoveValid = MoveEmptyFrame(playfield, randomDirection);
                if (isMoveValid == false)
                    i--;
            }
            
        }
    }
}