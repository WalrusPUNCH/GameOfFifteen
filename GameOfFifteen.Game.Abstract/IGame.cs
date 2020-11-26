using System;
using GameOfFifteen.Game.Entities;

namespace GameOfFifteen.Game.Abstract
{
    public interface IGame : IRedrawNotificator
    {
        delegate void GameWon();
        public event IGame.GameWon NotifyOnGameWon;
        int Moves { get; }
        IPlayfield Playfield { get; }
        GameSettings Settings { get; }
        void IncrementMovesNumber();
        void RestoreFromMemento(IGameMemento memento);
        IGameMemento SaveGameMemento();
        bool IsSolved();

    }
}