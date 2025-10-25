using System;
using Week4DoublyLinkedLists.Applications;

namespace Week4DoublyLinkedLists.Applications
{
    /// <summary>
    /// Interactive demonstration of the Music Playlist Manager
    /// Demonstrates practical application of doubly linked lists for media management
    /// 
    /// This class provides a simple interface to test the Part B implementation
    /// Focus: Testing playlist management and navigation features
    /// </summary>
    public class MusicPlaylistManager
    {
        private MusicPlaylist currentPlaylist;
        
        public MusicPlaylistManager()
        {
            currentPlaylist = new MusicPlaylist("Student Demo Playlist");
            LoadSampleSongs();
        }
        
        /// <summary>
        /// Run the interactive music playlist demonstration
        /// Simplified interface focusing on Part B requirements
        /// </summary>
        public void RunPlaylistDemo()
        {
            Console.WriteLine("=== PART B: MUSIC PLAYLIST MANAGER ===");
            Console.WriteLine();
            Console.WriteLine("Demonstrates practical application of doubly linked lists!");
            Console.WriteLine("Test your Step 8-11 implementations here.");
            Console.WriteLine();
            
            bool keepRunning = true;
            
            while (keepRunning)
            {
                DisplayCurrentStatus();
                DisplayMainMenu();
                
                string choice = Console.ReadLine() ?? "";
                Console.WriteLine();
                
                try
                {
                    switch (choice.ToLower())
                    {
                        case "1":
                            TestAddingSongs();
                            break;
                        case "2":
                            TestRemovingSongs();
                            break;
                        case "3":
                            TestNavigation();
                            break;
                        case "4":
                            TestDisplayMethods();
                            break;
                        case "5":
                            AddCustomSong();
                            break;
                        case "6":
                            ResetPlaylist();
                            break;
                        case "7":
                            keepRunning = false;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (NotImplementedException ex)
                {
                    Console.WriteLine($"🚧 Not Implemented Yet: {ex.Message}");
                    Console.WriteLine("💡 This method needs to be implemented in your MusicPlaylist class.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                
                if (keepRunning)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.WriteLine();
                }
            }
        }


        /// <summary>
        /// Display current playlist status - simplified for Part B
        /// </summary>
        private void DisplayCurrentStatus()
        {
            var currentSong = currentPlaylist.GetCurrentSong();
            Console.WriteLine("\n🎵 Current Song:");
            Console.WriteLine(currentSong != null ? currentSong.ToDetailedString() : "No song selected.");
        }

        /// <summary>
        /// Display the main testing menu - focused on Part B requirements
        /// </summary>
        private void DisplayMainMenu()
        {
            Console.WriteLine("\n=== MUSIC PLAYLIST MANAGER ===");
            Console.WriteLine("1. Add Sample Song");
            Console.WriteLine("2. Remove Song");
            Console.WriteLine("3. Navigate Playlist");
            Console.WriteLine("4. Display Playlist");
            Console.WriteLine("5. Add Custom Song");
            Console.WriteLine("6. Reset Playlist");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");
        }

        /// <summary>
        /// Test Step 10a: Adding Songs
        /// </summary>
        private void TestAddingSongs()
        {
            Console.WriteLine("1. Add to end");
            Console.WriteLine("2. Add at position");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    var newSong = new Song("Test Song", "Test Artist", TimeSpan.FromMinutes(3));
                    currentPlaylist.AddSong(newSong);
                    Console.WriteLine(" Song added to end.");
                    break;

                case "2":
                    Console.Write("Enter position: ");
                    if (int.TryParse(Console.ReadLine(), out int position))
                    {
                        var positionSong = new Song("Position Song", "Test Artist", TimeSpan.FromMinutes(2));
                        currentPlaylist.AddSongAt(position, positionSong);
                        Console.WriteLine($" Song added at position {position}.");
                    }
                    else
                    {
                        Console.WriteLine(" Invalid position.");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        /// <summary>
        /// Test Step 10b: Removing Songs
        /// </summary>
        private void TestRemovingSongs()
        {
            Console.WriteLine("=== TESTING STEP 10b: REMOVING SONGS ===");
            Console.WriteLine("📖 Assignment Reference: Step 10b in Part B");
            Console.WriteLine();
            
            if (!currentPlaylist.HasSongs)
            {
                Console.WriteLine("⚠️ Playlist is empty! Add some songs first to test removal.");
                return;
            }
            
            Console.WriteLine("1. Test RemoveSong (remove by song object)");
            Console.WriteLine("2. Test RemoveSongAt (remove by position)");
            Console.WriteLine();
            
            string choice = Console.ReadLine() ?? "";
            
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Testing RemoveSong method:");
                    var songToRemove = currentPlaylist.GetCurrentSong();
                    if (songToRemove != null)
                    {
                        try
                        {
                            bool removed = currentPlaylist.RemoveSong(songToRemove);
                            Console.WriteLine(removed ? $"✅ Successfully removed: {songToRemove}" : "❌ Song not found");
                        }
                        catch (NotImplementedException ex)
                        {
                            Console.WriteLine($"🚧 RemoveSong not implemented: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No current song to remove");
                    }
                    break;
                    
                case "2":
                    Console.WriteLine("Testing RemoveSongAt method:");
                    Console.Write($"Enter position (0 to {currentPlaylist.TotalSongs - 1}): ");
                    if (int.TryParse(Console.ReadLine(), out int position))
                    {
                        try
                        {
                            bool removed = currentPlaylist.RemoveSongAt(position);
                            Console.WriteLine(removed ? $"✅ Successfully removed song at position {position}" : "❌ Invalid position");
                        }
                        catch (NotImplementedException ex)
                        {
                            Console.WriteLine($"🚧 RemoveSongAt not implemented: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"❌ Error: {ex.Message}");
                        }
                    }
                    break;
                    
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
        
        
        /// <summary>
        /// Test Step 10c: Navigation
        /// </summary>
        private void TestNavigation()
        {
            Console.WriteLine("=== TESTING STEP 10c: NAVIGATION ===");
            Console.WriteLine("📖 Assignment Reference: Step 10c in Part B");
            Console.WriteLine("💡 This demonstrates the power of doubly linked lists!");
            Console.WriteLine();
            
            if (!currentPlaylist.HasSongs)
            {
                Console.WriteLine("⚠️ Playlist is empty! Add some songs first to test navigation.");
                return;
            }
            
            Console.WriteLine("1. Test Next() - move to next song");
            Console.WriteLine("2. Test Previous() - move to previous song");
            Console.WriteLine("3. Test JumpToSong() - jump to specific position");
            Console.WriteLine();
            
            string choice = Console.ReadLine() ?? "";
            
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Testing Next method:");
                    try
                    {
                        bool moved = currentPlaylist.Next();
                        Console.WriteLine(moved ? "✅ Moved to next song" : "ℹ️ Already at end of playlist");
                        var current = currentPlaylist.GetCurrentSong();
                        if (current != null)
                            Console.WriteLine($"Current song: {current}");
                    }
                    catch (NotImplementedException ex)
                    {
                        Console.WriteLine($"🚧 Next not implemented: {ex.Message}");
                    }
                    break;
                    
                case "2":
                    Console.WriteLine("Testing Previous method:");
                    try
                    {
                        bool moved = currentPlaylist.Previous();
                        Console.WriteLine(moved ? "✅ Moved to previous song" : "ℹ️ Already at beginning of playlist");
                        var current = currentPlaylist.GetCurrentSong();
                        if (current != null)
                            Console.WriteLine($"Current song: {current}");
                    }
                    catch (NotImplementedException ex)
                    {
                        Console.WriteLine($"🚧 Previous not implemented: {ex.Message}");
                    }
                    break;
                    
                case "3":
                    Console.WriteLine("Testing JumpToSong method:");
                    Console.Write($"Enter position (0 to {currentPlaylist.TotalSongs - 1}): ");
                    if (int.TryParse(Console.ReadLine(), out int position))
                    {
                        try
                        {
                            bool jumped = currentPlaylist.JumpToSong(position);
                            Console.WriteLine(jumped ? $"✅ Jumped to position {position}" : "❌ Invalid position");
                            var current = currentPlaylist.GetCurrentSong();
                            if (current != null)
                                Console.WriteLine($"Current song: {current}");
                        }
                        catch (NotImplementedException ex)
                        {
                            Console.WriteLine($"🚧 JumpToSong not implemented: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"❌ Error: {ex.Message}");
                        }
                    }
                    break;
                    
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
        
        /// <summary>
        /// Test Step 11: Display Methods
        /// </summary>
        private void TestDisplayMethods()
        {
            Console.WriteLine("=== TESTING STEP 11: DISPLAY METHODS ===");
            Console.WriteLine("📖 Assignment Reference: Step 11 in Part B");
            Console.WriteLine();
            
            Console.WriteLine("1. Test DisplayPlaylist() - show full playlist");
            Console.WriteLine("2. Test DisplayCurrentSong() - show current song details");
            Console.WriteLine("3. Test GetCurrentSong() - get current song object");
            Console.WriteLine();
            
            string choice = Console.ReadLine() ?? "";
            
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Testing DisplayPlaylist method:");
                    Console.WriteLine("Expected: Full playlist with current song marked");
                    Console.WriteLine("----------------------------------------");
                    try
                    {
                        currentPlaylist.DisplayPlaylist();
                    }
                    catch (NotImplementedException ex)
                    {
                        Console.WriteLine($"🚧 DisplayPlaylist not implemented: {ex.Message}");
                    }
                    break;
                    
                case "2":
                    Console.WriteLine("Testing DisplayCurrentSong method:");
                    Console.WriteLine("Expected: Detailed current song information");
                    Console.WriteLine("----------------------------------------");
                    try
                    {
                        currentPlaylist.DisplayCurrentSong();
                    }
                    catch (NotImplementedException ex)
                    {
                        Console.WriteLine($"🚧 DisplayCurrentSong not implemented: {ex.Message}");
                    }
                    break;
                    
                case "3":
                    Console.WriteLine("Testing GetCurrentSong method:");
                    try
                    {
                        var current = currentPlaylist.GetCurrentSong();
                        Console.WriteLine($"Current song object: {current?.ToString() ?? "None"}");
                    }
                    catch (NotImplementedException ex)
                    {
                        Console.WriteLine($"🚧 GetCurrentSong not implemented: {ex.Message}");
                    }
                    break;
                    
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
        
        /// <summary>
        /// Add a custom song for testing
        /// </summary>
        private void AddCustomSong()
        {
            Console.WriteLine("=== ADD CUSTOM SONG ===");
            Console.WriteLine();
            
            Console.Write("Song title: ");
            string title = Console.ReadLine() ?? "Untitled";
            
            Console.Write("Artist: ");
            string artist = Console.ReadLine() ?? "Unknown Artist";
            
            Console.Write("Duration (mm:ss): ");
            string durationStr = Console.ReadLine() ?? "3:00";
            
            if (TimeSpan.TryParseExact(durationStr, @"mm\:ss", null, out TimeSpan duration))
            {
                var song = new Song(title, artist, duration);
                try
                {
                    currentPlaylist.AddSong(song);
                    Console.WriteLine($"✅ Added: {song}");
                }
                catch (NotImplementedException ex)
                {
                    Console.WriteLine($"🚧 Cannot add song: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("❌ Invalid duration format. Use mm:ss (e.g., 3:45)");
            }
        }
        
        /// <summary>
        /// Reset playlist for testing
        /// </summary>
        private void ResetPlaylist()
        {
            Console.WriteLine("=== RESET PLAYLIST ===");
            currentPlaylist = new MusicPlaylist("Student Demo Playlist");
            LoadSampleSongs();
            Console.WriteLine("✅ Playlist reset with sample songs");
        }
        
        /// <summary>
        /// Load sample songs for testing - calls the student's AddSong method
        /// </summary>
        private void LoadSampleSongs()
        {
            try
            {
                currentPlaylist.AddSong(new Song("Bohemian Rhapsody", "Queen", TimeSpan.Parse("00:05:55")));
                currentPlaylist.AddSong(new Song("Hotel California", "Eagles", TimeSpan.Parse("00:06:30")));
                currentPlaylist.AddSong(new Song("Imagine", "John Lennon", TimeSpan.Parse("00:03:03")));
                Console.WriteLine("📀 Loaded sample songs for testing");
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("🚧 Sample songs not loaded - AddSong method needs implementation");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error loading sample songs: {ex.Message}");
            }
        }
    }
}