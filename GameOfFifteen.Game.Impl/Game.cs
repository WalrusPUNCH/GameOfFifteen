using System;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Entities;
using GameOfFifteen.Game.Impl.FrameCreation;
using GameOfFifteen.Game.Impl.FrameCreation.ConcreteFrameCreators;

namespace GameOfFifteen.Game.Impl
{
    public class Game : IGame
    {
        public event IGame.GameWon NotifyOnGameWon;
        public event IRedrawNotificator.RedrawField NotifyOnPlayfieldChange;
        public IPlayfield Playfield { get; private set; }
        public GameSettings Settings { get; private set;}
        public int Moves { get; private set; }

        public Game(GameSettings settings)
        {
            Settings = settings;
            Moves = 0;
            Playfield = new Playfield(settings.FrameType, settings.Size);
        }

        public void IncrementMovesNumber()
        {
            Moves++;
        }

        public void RestoreFromMemento(IGameMemento memento)
        {
            var newMemento = (GameMemento) memento;
            Playfield = newMemento.Playfield;
            Settings = newMemento.Settings;
            Moves = newMemento.Moves;
            NotifyOnPlayfieldChange?.Invoke(Playfield.Board);
        }

        public IGameMemento SaveGameMemento()
        {
            return new GameMemento(Settings, Moves, (Playfield)Playfield.Clone());
        }

        public bool IsSolved()
        {
            var emptyBoard = Playfield.Board[Playfield.Board.GetLength(0) - 1, Playfield.Board.GetLength(1) - 1];
            if (emptyBoard != null)
                return false;
            for (int i = 0; i < Playfield.Board.GetLength(0); i++)
            {
                for (int j = 0; j < Playfield.Board.GetLength(1); j++)
                {
                    if (j == Playfield.Board.GetLength(1) - 1 && j == i)
                    {
                        continue;
                    }
                    
                    if (Playfield.Board[i, j].FinishPoint.Y != i 
                        || Playfield.Board[i, j].FinishPoint.X != j)
                    {
                        return false;
                    }
                }
            }
            NotifyOnGameWon?.Invoke();
            return true;
        }
    }
}