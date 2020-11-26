using System;
using GameOfFifteen.CommandCenter.Impl;
using GameOfFifteen.ControlCenter.Abstract;
using GameOfFifteen.ControlCenter.Impl;
using GameOfFifteen.Game.Abstract;
using GameOfFifteen.Game.Impl;

namespace GameOfFifteen
{
    class Program
    {
        static void Main(string[] args)
        {
            GameCreator gameCreator = new GameCreator();
            Manipulator manipulator = Manipulator.GetInstance();
            CommandHistory commandHistory = new CommandHistory();
            CommandManager commandManager = new CommandManager(gameCreator, manipulator, commandHistory);
            GameMessageHolder messageHolder = new GameMessageHolder();
            ConsoleManager consoleManager = new ConsoleManager();
            IController controller = new Controller(gameCreator, manipulator, commandHistory, commandManager,
                                                    messageHolder, consoleManager);
            controller.ServeGame();
        }
    }
}