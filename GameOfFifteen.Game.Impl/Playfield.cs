using System;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Entities;
using GameOfFifteen.Game.Impl.FrameCreation;
using GameOfFifteen.Game.Impl.FrameCreation.ConcreteFrameCreators;

namespace GameOfFifteen.Game.Impl
{
    public class Playfield : IPlayfield
    { 
       public  Frame[,] Board { get; private set; }
       private readonly FrameCreator _frameCreator;
       
       public Playfield(FrameType frameType, int size)
       {
           switch (frameType)
           {
               case FrameType.Normal:
                   _frameCreator = new TextFrameCreator();
                   break;
               case FrameType.Boarded:
                   _frameCreator = new BoardedFrameCreator();
                   break;
           }

           Board = _frameCreator.CreateBoard(size);
       }

       public object Clone()
       {
           var copiedPlayfield = (Playfield)MemberwiseClone();//new Playfield(_boardSize, _level, _frameType);
           copiedPlayfield.Board = CloneBoard();
           return copiedPlayfield;       
       }

        private Frame[,] CloneBoard()
        {
            Frame[,] boardCopy = new Frame[Board.GetLength(0), Board.GetLength(1)];
            for (int i = 0; i < boardCopy.GetLength(0); i++)
            {
                for (int j = 0; j < boardCopy.GetLength(1); j++)
                {
                    if (Board[i, j] == null)
                    {
                        boardCopy[i, j] = null;
                    }
                    else
                    {
                        boardCopy[i, j] = _frameCreator.CreateFrame(Board[i, j]?.Content, Board[i, j].FinishPoint);
                    }
                }
            }
            return boardCopy;
        }
    }
}