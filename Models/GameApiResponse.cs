using GameBrainExplorer.Models;

public class GameApiResponse
{
    public int Total_Result { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
    public List<GameConsole> Results { get; set; }
}