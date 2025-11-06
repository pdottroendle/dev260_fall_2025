
using System.Collections.Generic;

public class Player
{
    public string Username { get; set; }
    public int SkillRating { get; set; } // 1 to 10
    public int Wins { get; set; }
    public int Losses { get; set; }
    public List<string> RecentOpponents { get; set; }

    public Player(string username, int skillRating)
    {
        Username = username;
        SkillRating = skillRating;
        Wins = 0;
        Losses = 0;
        RecentOpponents = new List<string>();
    }
}
