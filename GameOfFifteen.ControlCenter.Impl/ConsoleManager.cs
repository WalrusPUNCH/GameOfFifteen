using System;
using System.Linq;
using GameOfFifteen.ControlCenter.Abstract;
using GameOfFifteen.Game.Entities;

namespace GameOfFifteen.ControlCenter.Impl
{
    public class ConsoleManager : IConsoleManager
    {
        public string[] GetProcessedInput()
        {
            Console.Write("> ");
            string input = Console.ReadLine()?.Trim().ToLower();
            string[] processedInput = new string[] {input};
            if (string.IsNullOrWhiteSpace(input) == false)
            {
                processedInput = input.Split(' ').Where(x => !string.IsNullOrEmpty(x))
                    .Select(x => x.ToLower())
                    .ToArray();
            }
            return processedInput;
        }
        
        

        public void DrawField(Frame[,] field)
        {
            string renderedBoard = "";
            string partOfBarrier = "";
            for (int i = 0; i < field.GetLength(0); i++)
            {
                partOfBarrier += "-------+";
            }
            string barrier = $"+{partOfBarrier}\n";

            for (int i=0; i<= field.GetLength(0) - 1; i++)
            {
                renderedBoard += barrier;
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    renderedBoard += string.Format("|  {0, -5}", field[i, j]?.Render());
                }
                renderedBoard += "|\n";
            }

            renderedBoard += barrier;
            ShowText(renderedBoard);
        }

        public void ShowText(string text)
        {
            Console.WriteLine(text);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}