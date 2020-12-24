using GameOfFifteen.CommandCenter.Impl.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameOfFifteen.UserInterface.ViewModels
{
    public class FrameViewModel
    {
        public string Content { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public ICommand MoveFrameCommand { get; set; }


        public FrameViewModel(string content, int row = 0, int column = 0, ICommand command = null)
        {
            Content = content;
            Row = row;
            Column = column;
            MoveFrameCommand = command;
        }


    }
}
