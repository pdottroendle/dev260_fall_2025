# Fall 2025 Bellevue College DEV 260 Assignment 9: 

# 

# BST File System Navigator - Implementation Notes

========================================================================================================================

**Name:** Peter Paul Troendle

**Supervisor**: Zak





# Quick Demos to Try in Your Console

Load samples
11 → Prepopulates directories/files.

Find all .cs files (name target)
13 ^.\*.cs$ name
(Matches app.cs if loaded.)

Find items beginning with doc (both name or extension)
13 ^doc.\* both
(E.g., document.pdf.)

Match extension only (include directories = no)
13 ^.mp3$ extension n
(Finds song.mp3.)

Include directories and match names starting with P
13 ^P.\* name y
(Will include Pictures/ if present.)

Tip: Remember to escape backslashes in regex when typing in code, but in your console input you can type them normally (e.g., ^app.\*.cs$).



# Binary Search Tree Pattern Understanding

How BST operations work for file system navigation:

BST provides O(log n) search time for balanced trees, which is far faster than linear scans. Each insertion maintains sorted order automatically, so files and directories are always organized without extra sorting steps. In-order traversal guarantees alphabetical listing while respecting the custom rule (directories before files). This hierarchical structure mirrors real file systems, making navigation efficient and intuitive.

Answer:

BST acts like a self-organizing index. Every insert positions the node so that searches follow left/right pointers instead of scanning everything. In-order traversal gives a sorted view without extra work, and the tree naturally models folder/file hierarchy. This combination makes operations like search, filter, and range queries efficient and elegant.



# Challenges and Solutions

Biggest challenge faced:

Implementing complex BST deletion with all three cases (leaf, one child, two children) while preserving tree integrity.

Answer:

Deletion was tricky because replacing a node with its inorder successor requires careful pointer updates to avoid breaking the BST property.

How you solved it:

I reviewed standard BST deletion algorithms, drew diagrams for each case, and tested edge cases (root deletion, two children). Debugging with the tree display feature helped confirm correctness.

Most confusing concept:

Recursive thinking for traversal combined with filtering logic—especially ensuring base cases and avoiding null reference errors.

Answer:

I overcame this by writing small helper methods (TraverseAndCollect) and testing them incrementally.



# Code Quality

What you're most proud of:

Clean, reusable helpers for traversal and filtering, plus the extra-credit regex and wildcard features that integrate seamlessly without breaking core logic.

What you would improve if you had more time:

Add better error messages, optimize deletion for large trees, and implement directory size aggregation for full hierarchy analysis.



# Real-World Applications

How this relates to actual file systems:

Modern file explorers and database indexes use tree-like structures for fast lookups and sorted views. My implementation mimics how directories and files are organized internally, similar to NTFS or B-tree indexes in databases.

What you learned about tree algorithms:

Recursive algorithms are powerful but require careful base-case handling. I learned how traversal patterns (in-order, pre-order) directly affect output order and performance.



# Stretch Features - Regex and wildcard search

Regex and wildcard search implemented. Regex allows advanced pattern matching, while wildcard provides user-friendly glob syntax mapped to regex internally.



Design choice: Kept regex on top of in-order traversal to preserve sorted output and avoid breaking BST invariants.
Performance: Regex complexity applies per visited node; still linear in tree size for the traversal, but practical for your dataset.
Robustness: Invalid patterns are caught (ArgumentException) and produce an empty result, keeping the app stable.
Flexibility: target selector and includeDirectories flag make the feature adaptable to different search scenarios.

Wildcard Support: Implemented WildcardToRegex(string pattern) and FindFilesByWildcard(...) wrapper to map \* → .\* and ? → . for glob-like searches.

# Time Spent

**Total time:** 5-7 hours

**Breakdown:**

* Understanding BST concepts and assignment requirements: 3 hours
* Implementing the 8 core TODO methods: 1 hours
* Testing with different file scenarios: 1/2 hours
* Debugging recursive algorithms and BST operations: 1/2 hours
* Writing these notes: 2

**Most time-consuming part:** Familiarising with the BST approach in software, all the different aspects

