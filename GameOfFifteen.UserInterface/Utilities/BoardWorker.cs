using GameOfFifteen.CommandCenter.Abstract;
using GameOfFifteen.CommandCenter.Impl.Commands;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Entities;
using GameOfFifteen.UserInterface.Utilities;
using GameOfFifteen.UserInterface.Utilities.Abstract;
using GameOfFifteen.UserInterface.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GameOfFifteen.UserInterface
{
    public class BoardWorker : IBoardWorker
    {
        public IGame CurrentGame { private get; set; }

        private IManipulator _manipulator;
        private ICommandHistory _history;

        public BoardWorker(IManipulator manipulator, ICommandHistory history)
        {
            _manipulator = manipulator;
            _history = history;
        }

        public void FillBoard(ObservableCollection<FrameViewModel> board, Frame[,] model)
        {
            int emptyFrameIndex = 0;
            board.Clear();
            for (int i = 0; i < model.GetLength(0); i++)
            {
                for (int j = 0; j < model.GetLength(1); j++)
                {
                    if (model[i, j] == null)
                    {
                        emptyFrameIndex = board.Count;
                        continue;
                    }
                    else
                        board.Add(new FrameViewModel(model[i, j].Render(), i, j));
                }
            }

            SetCommandForMovableFrames(board, emptyFrameIndex, model.GetLength(0));
        }


        private void SetCommandForMovableFrames(ObservableCollection<FrameViewModel> board, int emptyFrameIndex, int boardSize)
        {
            SetCommand(board, emptyFrameIndex - 1, Direction.Left);
            SetCommand(board, emptyFrameIndex, Direction.Right);
            SetCommand(board, emptyFrameIndex - boardSize, Direction.Up);
            SetCommand(board, emptyFrameIndex + boardSize - 1, Direction.Down);
        }

        private void SetCommand(ObservableCollection<FrameViewModel> board, int index, Direction moveDirection)
        {
            try
            {
                board[index].MoveFrameCommand = new DelegateCommand(
                                                                    (context) =>
                                                                    new MoveCommand(CurrentGame, moveDirection, _manipulator, _history).Execute());
            }
            catch (ArgumentOutOfRangeException exception)
            {

            }
        }
    }
}
