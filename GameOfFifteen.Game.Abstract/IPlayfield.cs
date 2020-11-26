using System;
using GameOfFifteen.Game.Entities;

namespace GameOfFifteen.Game.Abstract
{
    public interface IPlayfield : ICloneable
    {
        Frame[,] Board { get; }
    }
}