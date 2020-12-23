using GameOfFifteen.Game.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfFifteen.Game.Abstract
{
    public interface IGameSettingsCreator
    {
        GameSettings CreateGameSettings(string[] parameters);
    }
}
