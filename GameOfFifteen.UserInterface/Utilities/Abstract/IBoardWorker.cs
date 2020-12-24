using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Entities;
using GameOfFifteen.UserInterface.ViewModels;
using System.Collections.ObjectModel;

namespace GameOfFifteen.UserInterface.Utilities.Abstract
{
    public interface IBoardWorker
    {
        IGame CurrentGame { set; }
        void FillBoard(ObservableCollection<FrameViewModel> board, Frame[,] model);
    }
}