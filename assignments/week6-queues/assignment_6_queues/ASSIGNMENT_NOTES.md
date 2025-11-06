
# Assignment Notes - Game Matchmaking System (Updated)
Peter Paul Troendle Bellevue College
DEV 260 - Advanced C# Programming
Zak


Game Matchmaking System
Overview
This project simulates a matchmaking system for online games using C# and Queue<T>. It supports multiple game modes, skill-based matching, and includes an extra credit feature to avoid recent opponents. The system is designed as a console application and demonstrates real-world queue management and match processing logic.
Features

## Implementation Summary
- Core matchmaking system using Queue<T> for each game mode
- Skill-based filtering for Ranked and QuickPlay
- Match outcome based on skill probability
- Added 'Avoid Recent Opponents' feature to prevent rematches


Player Management: Create players with usernames and skill ratings (1–10), track wins/losses, and adjust skill ratings based on match outcomes.
Multi-Queue Matchmaking:Casual Mode: Simple FIFO matching.
Ranked Mode: Matches players within a ±2 skill rating range.
QuickPlay Mode: Prefers skill-based matches but allows broader matching for speed.
Match Processing: Simulates match outcomes using skill-based probability and updates player statistics.
Queue Display: Shows current queue status for all game modes.
Player Stats Display: Outputs detailed player statistics.
Estimated Wait Time: Provides a basic estimate of wait time based on queue size.
Extra Credit Feature: Avoid Recent Opponents SystemTracks recent opponents for each player.
Prevents rematches within a cooldown period (last 3 matches).
How to Run

Open the project in Visual Studio 2022.
Ensure the target framework is set to .NET 8.0.
Build the solution (Ctrl+Shift+B).
Run the application (F5).
File Structure
assignment_6_queues_updated/
├── Assignment6.csproj
├── Program.cs
├── MatchmakingSystem.cs
├── Player.cs
├── Match.cs
├── GameMode.cs
├── ASSIGNMENT_NOTES.md

## Testing Scenarios
- Created players with skill levels: 4, 8
- Added to Ranked queue
- Verified match creation and stat updates
- Verified recent opponent tracking and filtering

## Challenges
- Filtering out recent opponents during match creation
- Managing recent opponent list size and updates


Sample Output
Match abgeschlossen: Alicia gewinnt!
Spieler: Alicia
Skill Rating: 9
Siege: 1, Niederlagen: 0
Spieler: Teddy
Skill Rating: 6
Siege: 0, Niederlagen: 1
Casual Queue:
Ranked Queue:
- Teddy (Skill: 6)
QuickPlay Queue:


## Reflection
This update demonstrates how real-world matchmaking systems avoid repetitive pairings to improve fairness and variety. It also shows how to extend basic queue logic with additional constraints.


This project demonstrates how queues can be used to manage matchmaking in games. The extra credit feature adds realism by preventing repetitive matchups, similar to systems used in competitive online games. The implementation also reinforces concepts like filtering, probability-based outcomes, and object-oriented design.

## Extra Credit
Implemented: Avoid Recent Opponents System

Author
Peter-Paul Troendle
License
MIT License (or as per course requirements)
