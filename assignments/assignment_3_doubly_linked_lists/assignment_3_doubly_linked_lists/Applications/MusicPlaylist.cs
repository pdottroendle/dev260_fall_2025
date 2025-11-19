using System;
using System.Linq;
using Week4DoublyLinkedLists.Core;

namespace Week4DoublyLinkedLists.Applications
{
    /// <summary>
    /// Represents a song in the music playlist
    /// Contains all metadata about a musical track
    /// </summary>
    public class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public TimeSpan Duration { get; set; }
        public string Album { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        
        public Song(string title, string artist, TimeSpan duration, string album = "", int year = 0, string genre = "")
        {
            Title = title;
            Artist = artist;
            Duration = duration;
            Album = album;
            Year = year;
            Genre = genre;
        }
        
        public override string ToString()
        {
            return $"{Title} by {Artist} ({Duration:mm\\:ss})";
        }
        
        public string ToDetailedString()
        {
            return $"{Title} - {Artist} [{Album}, {Year}] ({Duration:mm\\:ss}) [{Genre}]";
        }
    }
    
    /// <summary>
    /// Music playlist manager using doubly linked list for efficient navigation
    /// Demonstrates practical application of doubly linked lists
    /// 
    /// PART B IMPLEMENTATION GUIDE:
    /// Step 8: Song class (already provided above)
    /// Step 9: Playlist core structure (implement below)
    /// Step 10: Playlist management (AddSong, RemoveSong, Navigation)
    /// Step 11: Display and basic management
    /// </summary>
    public class MusicPlaylist
    {
        #region Step 9: Playlist Core Structure - TODO: Students implement these properties
        
        private DoublyLinkedList<Song> playlist;
        private Node<Song>? currentSong;     // Currently selected song node
        
        // Basic playlist properties
        public string Name { get; set; }
        public int TotalSongs => playlist.Count;
        public bool HasSongs => playlist.Count > 0;
        public Song? CurrentSong => currentSong?.Data;
        
        /// <summary>
        /// Initialize a new music playlist
        /// </summary>
        /// <param name="name">Name of the playlist</param>
        public MusicPlaylist(string name = "My Playlist")
        {
            Name = name;
            playlist = new DoublyLinkedList<Song>();
            currentSong = null;
        }

        #endregion

        #region Step 10: Playlist Management - TODO: Students implement these step by step

        #region Step 10a: Adding Songs (5 points)

        public void AddSong(Song song)
        {
            playlist.AddLast(song);
            if (currentSong == null)
            {
                currentSong = playlist.Last;
            }
        }


        /// <summary>
        /// Add a song at a specific position in the playlist
        /// Time Complexity: O(n) for position-based insertion
        /// 📚 Reference: https://www.geeksforgeeks.org/applications-of-linked-list-data-structure/
        /// </summary>
        /// <param name="position">Position to insert at (0-based)</param>
        /// <param name="song">Song to add</param>
        public void AddSongAt(int position, Song song)
        {
            if (position < 0 || position > playlist.Count)
            {
                Console.WriteLine("Invalid position.");
                return;
            }

            playlist.Insert(position, song);

            // If this is the first song added, set it as current
            if (currentSong == null)
            {
                currentSong = playlist.First;
            }
        }
        #endregion

        #region Step 10b: Removing Songs (5 points)

        /// <summary>
        /// Remove a specific song from the playlist
        /// Time Complexity: O(n) due to search operation
        /// 📚 Reference: https://www.geeksforgeeks.org/applications-of-linked-list-data-structure/
        /// </summary>
        /// <param name="song">Song to remove</param>
        /// <returns>True if song was found and removed</returns>
        public bool RemoveSong(Song song)
        {
            if (!HasSongs)
                return false;

            bool removed = playlist.Remove(song);

            // If the removed song was the current song, update currentSong
            if (currentSong != null && currentSong.Data.Equals(song))
            {
                currentSong = currentSong.Next ?? playlist.Last;
            }

            return removed;
        }

        /// <summary>
        /// Remove song at a specific position
        /// Time Complexity: O(n) for position-based removal
        /// 📚 Reference: https://www.geeksforgeeks.org/applications-of-linked-list-data-structure/
        /// </summary>
        /// <param name="position">Position to remove (0-based)</param>
        /// <returns>True if song was removed successfully</returns>
        public bool RemoveSongAt(int position)
        {
            if (position < 0 || position >= playlist.Count)
                return false;

            var nodeToRemove = playlist.First;
            for (int i = 0; i < position; i++)
            {
                nodeToRemove = nodeToRemove?.Next;
            }

            if (nodeToRemove == null)
                return false;

            bool wasCurrent = nodeToRemove == currentSong;

            playlist.RemoveAt(position);

            if (wasCurrent)
            {
                currentSong = nodeToRemove.Next ?? playlist.Last;
            }

            return true;
        }

        #endregion

        #region Step 10c: Navigation (5 points)

        /// <summary>
        /// Move to the next song in the playlist
        /// Time Complexity: O(1) due to doubly linked list Next pointer
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        /// <returns>True if moved to next song, false if at end</returns>
        public bool Next()
        {
            if (currentSong?.Next != null)
            {
                currentSong = currentSong.Next;
                return true;
            }

            return false; // Already at end
        }

        /// <summary>
        /// Move to the previous song in the playlist
        /// Time Complexity: O(1) due to doubly linked list Previous pointer
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        /// <returns>True if moved to previous song, false if at beginning</returns>
        public bool Previous()
        {
            if (currentSong?.Previous != null)
            {
                currentSong = currentSong.Previous;
                return true;
            }

            return false; // Already at beginning
        }

        /// <summary>
        /// Jump directly to a song at a specific position
        /// Time Complexity: O(n) for position-based access
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        /// <param name="position">Position to jump to (0-based)</param>
        /// <returns>True if jump was successful</returns>
        public bool JumpToSong(int position)
        {
            if (position < 0 || position >= playlist.Count)
                return false;

            var node = playlist.First;
            for (int i = 0; i < position; i++)
            {
                node = node?.Next;
            }

            if (node != null)
            {
                currentSong = node;
                return true;
            }

            return false;
        }

        #endregion

        #endregion

        #region Step 11: Display and Basic Management (10 points)

        /// <summary>
        /// Display the entire playlist with current song highlighted
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        public void DisplayPlaylist()
        {
            Console.WriteLine($"🎶 Playlist: {Name} ({TotalSongs} songs)");
            Console.WriteLine("----------------------------------------");

            int index = 0;
            foreach (var song in playlist)
            {
                string marker = (currentSong != null && currentSong.Data.Equals(song)) ? "👉" : "  ";
                Console.WriteLine($"{marker} [{index}] {song.ToDetailedString()}");
                index++;
            }

            if (TotalSongs == 0)
            {
                Console.WriteLine("⚠️ Playlist is empty.");
            }
        }

        /// <summary>
        /// Display details of the currently selected song
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        public void DisplayCurrentSong()
        {
            if (currentSong == null)
            {
                Console.WriteLine("⚠️ No song is currently selected.");
            }
            else
            {
                Console.WriteLine("🎵 Now Playing:");
                Console.WriteLine(currentSong.Data.ToDetailedString());
            }
        }

        /// <summary>
        /// Get the currently selected song
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        /// <returns>Currently selected song, or null if no song selected</returns>
        public Song? GetCurrentSong()
        {
            return currentSong?.Data;
        }

        #endregion

        #region Helper Methods for Students

        /// <summary>
        /// Get the position of the current song (1-based for display)
        /// Useful for showing "Song X of Y" information
        /// </summary>
        /// <returns>Position of current song, or 0 if no current song</returns>
        public int GetCurrentPosition()
        {
            if (currentSong == null) return 0;
            
            int position = 1;
            var current = playlist.First;
            while (current != null && current != currentSong)
            {
                position++;
                current = current.Next;
            }
            return current == currentSong ? position : 0;
        }
        
        /// <summary>
        /// Calculate total duration of all songs in the playlist
        /// Demonstrates traversal and aggregate operations
        /// </summary>
        /// <returns>Total duration as TimeSpan</returns>
        public TimeSpan GetTotalDuration()
        {
            TimeSpan total = TimeSpan.Zero;
            foreach (var song in playlist)
            {
                total = total.Add(song.Duration);
            }
            return total;
        }
        
        #endregion
    }
}