using GameOfFifteen.CommandCenter.Abstract;
using GameOfFifteen.CommandCenter.Impl;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Entities;
using GameOfFifteen.Game.Impl;
using GameOfFifteen.UserInterface.Mappers;
using GameOfFifteen.UserInterface.Mappers.Abstract;
using GameOfFifteen.UserInterface.Utilities;
using GameOfFifteen.UserInterface.Utilities.Abstract;
using GameOfFifteen.UserInterface.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace GameOfFifteen.UserInterface
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IServiceProvider serviceProvider = CreateServiceProvider();

            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindow.Show();

        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IManipulator, Manipulator>();
            services.AddScoped<IGameCreator, GameCreator>();
            services.AddScoped<ICommandHistory, CommandHistory>();

            services.AddScoped<IMapFrom<GameSettings, GameSettingsViewModel>, GameSettingsMapper>();

            services.AddScoped<IMessageBoxService, MessageBoxService>();
            services.AddScoped<IBoardWorker, BoardWorker>();

            services.AddScoped<MainWindowViewModel>();

            return services.BuildServiceProvider();
        }
    }

}
