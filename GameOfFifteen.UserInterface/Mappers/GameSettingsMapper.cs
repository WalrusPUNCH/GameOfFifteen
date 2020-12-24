using GameOfFifteen.Game.Entities;
using GameOfFifteen.UserInterface.Mappers.Abstract;
using GameOfFifteen.UserInterface.ViewModels;
using System;

namespace GameOfFifteen.UserInterface.Mappers
{
    class GameSettingsMapper : IMap<GameSettings, GameSettingsViewModel>
    {
        public GameSettings MapFrom(GameSettingsViewModel viewModel)
        {
            return new GameSettings(viewModel.Size, viewModel.Level, viewModel.FrameType, viewModel.IsRandomActionsEnabled);
        }

        public GameSettingsViewModel MapTo(GameSettings model)
        {
            return new GameSettingsViewModel(model.Size, model.Level, model.FrameType, model.IsRandomActionsEnabled);
        }
    }
}
