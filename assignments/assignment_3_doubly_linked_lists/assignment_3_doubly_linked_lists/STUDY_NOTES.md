STUDY_NOTES.md

####Peter Paul Troendle
####Bellevue College DEV260
####Supervisor Zak
####Fall 2025

**** Setup Instructions

Open the solution in Visual Studio 2022.
Build the project (Ctrl+Shift+B) to verify all components compile.
Run Program.cs to access the interactive menu.
Use options 1–4 to test core list operations and playlist features.


**** Step-by-Step Development Log
- Step 1–2: Node & List Structure

Implemented Node<T> with Data, Next, Previous.
Created DoublyLinkedList<T> with head, tail, count, and public properties.
- Step 3: Add Methods

AddFirst, AddLast, and Insert implemented.
Used GetNodeAt() for optimized traversal.
- Step 4: Traversal

DisplayForward, DisplayBackward, and ToArray implemented.
- Step 5: Search

Implemented Contains, Find, and IndexOf.
 - Step 6: Removal

Implemented RemoveFirst, RemoveLast, Remove, RemoveAt, and helper RemoveNode().
-  Step 7: Advanced Operations

Clear() and Reverse() implemented.
 - Step 8–11: Music Playlist Manager

Created Song class with metadata and formatting.
MusicPlaylist uses DoublyLinkedList<Song>.
Implemented AddSong, AddSongAt, RemoveSong, RemoveSongAt, Next, Previous, JumpToSong.
Display methods show current song and full playlist.


**** Challenges Faced

Initial build errors due to misplaced playlist and currentSong declarations in MusicPlaylistManager instead of MusicPlaylist.
Null reference warnings resolved by validating node access and ensuring proper initialization.
Navigation logic required careful pointer handling to avoid skipping or misreferencing nodes.


**** Performance Reflection
-  100 Elements

List: Insert=0.000008s, Remove=0.000001s, Search=0.000001s
Array: Insert=0.000005s, Remove=0.000001s, Search=0.000001s
DoublyLinkedList: Insert=0.000028s, Remove=0.000003s, Search=0.000004s
-  1000 Elements

List: Insert=0.000046s, Remove=0.000004s, Search=0.000006s
Array: Insert=0.000063s, Remove=0.000004s, Search=0.000006s
DoublyLinkedList: Insert=0.000278s, Remove=0.000018s, Search=0.000031s
-  10000 Elements

List: Insert=0.000481s, Remove=0.000035s, Search=0.000066s
Array: Insert=0.000570s, Remove=0.000035s, Search=0.000066s
DoublyLinkedList: Insert=0.003664s, Remove=0.000226s, Search=0.000366s
**** Insights

Arrays and List outperform linked lists in raw speed for insert/search.
DoublyLinkedList excels in frequent insertions/removals at arbitrary positions.
Ideal for use cases like music playlists, undo/redo stacks, and browser history.


**** Final Notes

Went through all steps trying to reduce the verbose and amount of information, but the sequential incremental approach then was better - still hope I did not let anything out
