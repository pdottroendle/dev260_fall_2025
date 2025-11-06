
public class Match
{
    public Player Player1 { get; set; }
    public Player Player2 { get; set; }
    public GameMode Mode { get; set; }

    public Match(Player p1, Player p2, GameMode mode)
    {
        Player1 = p1;
        Player2 = p2;
        Mode = mode;
    }
}
