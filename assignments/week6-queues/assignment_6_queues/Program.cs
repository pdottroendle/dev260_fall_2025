
using System;

class Program
{
    static void Main(string[] args)
    {
        MatchmakingSystem system = new MatchmakingSystem();

        Player alice = new Player("Alicia", 8);
        Player teddy = new Player("Teddy", 7);

        system.AddToQueue(alice, GameMode.Ranked);
        system.AddToQueue(teddy, GameMode.Ranked);

        Match match = system.TryCreateMatch(GameMode.Ranked);
        if (match != null)
        {
            system.ProcessMatch(match);
            system.DisplayPlayerStats(alice);
            system.DisplayPlayerStats(teddy);
        }

        system.DisplayQueueStatus();
    }
}
