namespace GameOfFifteen.ControlCenter.Abstract
{
    public interface IGameMessageHolder
    {
        string GetGeneralGameInformation();
        string GetShortGameInformation(int movesToBeat);
        string GetVictoriousMessage(int movesToBeat);
    }
}