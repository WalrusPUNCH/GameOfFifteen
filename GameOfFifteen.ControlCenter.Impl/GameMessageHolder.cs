using GameOfFifteen.ControlCenter.Abstract;

namespace GameOfFifteen.ControlCenter.Impl
{
    public class GameMessageHolder : IGameMessageHolder
    {
        public string GetGeneralGameInformation()
        {
            return "Welcome to the 15 puzzle game!\n\n" +
                   
                   "Available moves for the empty cell:\n" +
                   "up       or w     to move the empty cell up\n" +
                   "left     or a     to move the empty cell to the left\n" +
                   "down     or s     to move the empty cell down\n" +
                   "right    or d     to move the empty cell to the right\n\n" +
                   
                   "To start a new game :\n" +
                   "start *map size* *difficulty* *frame type* *random actions*, for example 'start 3 easy normal true'\n" +
                   "*difficulty* could be 'easy', 'medium' or 'hard'\n" +
                   "*frame type* could be 'normal' or 'boarded'\n" +
                   "*random actions* true or false. Default: false\n\n" +
                   "undo     or u     to undo\n" +
                   "quit     or q     to quit the game\n\n" +
                   "Let's start the game!\n start 3 easy normal";
        }

        public string GetShortGameInformation(int movesToBeat)
        {
            return $"Game has started. You already made {movesToBeat} moves\n\n" +
                   
                   "Available moves for the empty cell:\n" +
                   "up       or w     to move the empty cell up\n" +
                   "left     or a     to move the empty cell to the left\n" +
                   "down     or s     to move the empty cell down\n" +
                   "right    or d     to move the empty cell to the right\n" +
                   "undo     or u     to undo\n" +
                   "quit     or q     to quit the game\n\n";
        }

        public string GetVictoriousMessage(int movesToBeat)
        {
            return $"\n\nCongratulations! You solved the puzzle in {movesToBeat} moves!";
        }
    }
}