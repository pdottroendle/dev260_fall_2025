
using System;
using System.Collections.Generic;
using System.Linq;

public class MatchmakingSystem
{
    private Queue<Player> casualQueue = new();
    private Queue<Player> rankedQueue = new();
    private Queue<Player> quickPlayQueue = new();

    public void AddToQueue(Player player, GameMode mode)
    {
        switch (mode)
        {
            case GameMode.Casual:
                casualQueue.Enqueue(player);
                break;
            case GameMode.Ranked:
                rankedQueue.Enqueue(player);
                break;
            case GameMode.QuickPlay:
                quickPlayQueue.Enqueue(player);
                break;
            default:
                Console.WriteLine("invalid gamemode.");
                break;
        }
    }

    public Match TryCreateMatch(GameMode mode)
    {
        Queue<Player> queue = GetQueueByMode(mode);

        if (queue.Count < 2)
        {
            Console.WriteLine("not enough players in the queue.");
            return null;
        }

        Player p1 = queue.Dequeue();
        Player p2 = null;

        switch (mode)
        {
            case GameMode.Casual:
                p2 = queue.Dequeue();
                break;

            case GameMode.Ranked:
                p2 = queue.FirstOrDefault(p =>
                    Math.Abs(p.SkillRating - p1.SkillRating) <= 2 &&
                    !p1.RecentOpponents.Contains(p.Username) &&
                    !p.RecentOpponents.Contains(p1.Username));
                break;

            case GameMode.QuickPlay:
                p2 = queue.FirstOrDefault(p =>
                    Math.Abs(p.SkillRating - p1.SkillRating) <= 2 &&
                    !p1.RecentOpponents.Contains(p.Username) &&
                    !p.RecentOpponents.Contains(p1.Username))
                    ?? queue.FirstOrDefault(p =>
                    !p1.RecentOpponents.Contains(p.Username) &&
                    !p.RecentOpponents.Contains(p1.Username));
                break;
        }

        if (p2 == null)
        {
            Console.WriteLine("no matching palyer.");
            queue.Enqueue(p1);
            return null;
        }

        return new Match(p1, p2, mode);
    }

    public void ProcessMatch(Match match)
    {
        Random rand = new();
        double totalSkill = match.Player1.SkillRating + match.Player2.SkillRating;
        double winChanceP1 = match.Player1.SkillRating / totalSkill;

        bool p1Wins = rand.NextDouble() < winChanceP1;

        if (p1Wins)
        {
            match.Player1.Wins++;
            match.Player2.Losses++;
            match.Player1.SkillRating = Math.Min(10, match.Player1.SkillRating + 1);
            match.Player2.SkillRating = Math.Max(1, match.Player2.SkillRating - 1);
        }
        else
        {
            match.Player2.Wins++;
            match.Player1.Losses++;
            match.Player2.SkillRating = Math.Min(10, match.Player2.SkillRating + 1);
            match.Player1.SkillRating = Math.Max(1, match.Player1.SkillRating - 1);
        }

        AddRecentOpponent(match.Player1, match.Player2.Username);
        AddRecentOpponent(match.Player2, match.Player1.Username);

        Console.WriteLine($"Match finished: {(p1Wins ? match.Player1.Username : match.Player2.Username)} gewinnt!");
    }

    private void AddRecentOpponent(Player player, string opponentUsername)
    {
        player.RecentOpponents.Insert(0, opponentUsername);
        if (player.RecentOpponents.Count > 3)
        {
            player.RecentOpponents.RemoveAt(player.RecentOpponents.Count - 1);
        }
    }

    public void DisplayQueueStatus()
    {
        Console.WriteLine("Casual Queue:");
        foreach (var p in casualQueue) Console.WriteLine($"- {p.Username} (Skill: {p.SkillRating})");

        Console.WriteLine("Ranked Queue:");
        foreach (var p in rankedQueue) Console.WriteLine($"- {p.Username} (Skill: {p.SkillRating})");

        Console.WriteLine("QuickPlay Queue:");
        foreach (var p in quickPlayQueue) Console.WriteLine($"- {p.Username} (Skill: {p.SkillRating})");
    }

    public void DisplayPlayerStats(Player player)
    {
        Console.WriteLine($"player: {player.Username}");
        Console.WriteLine($"Skill Rating: {player.SkillRating}");
        Console.WriteLine($"win: {player.Wins}, defeat: {player.Losses}");
    }

    public int GetQueueEstimate(GameMode mode)
    {
        Queue<Player> queue = GetQueueByMode(mode);
        return queue.Count * 2;
    }

    private Queue<Player> GetQueueByMode(GameMode mode)
    {
        return mode switch
        {
            GameMode.Casual => casualQueue,
            GameMode.Ranked => rankedQueue,
            GameMode.QuickPlay => quickPlayQueue,
            _ => new Queue<Player>()
        };
    }
}
