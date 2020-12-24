using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfFifteen.UserInterface.Utilities.Abstract
{
    public interface IMessageBoxService
    {
        void ShowMessage(string text, string caption);
    }
}
