using GameOfFifteen.UserInterface.Utilities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GameOfFifteen.UserInterface.Utilities
{
    public class MessageBoxService : IMessageBoxService
    {
        void IMessageBoxService.ShowMessage(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
