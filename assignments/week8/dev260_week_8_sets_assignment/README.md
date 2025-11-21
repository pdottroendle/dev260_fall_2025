Peter Paul Troende
Supervisor Zak

Belleue College Fall 2025

DEV260

WEEK 8 Assignment

Lists vs Hashset

Simple Spell Checker & Vocabulary Explorer
This C# console application demonstrates HashSet operations for fast word lookups, text analysis, and spell checking. It includes:

Dictionary loading from dictionary.txt
Text analysis with normalization and tokenization
Categorization into correct vs misspelled words
Interactive menu system
Extra Credit: Spell Suggestions using Levenshtein Distance


How to Run

Clone the repository:
Shellgit clone https://github.com/pdottroendle/dev260_fall_2025/tree/week8_lab_assignement/assignments/week8/
Show more lines

Open in Visual Studio or run from CLI:
Shelldotnet runShow more lines

Ensure dictionary.txt and sample text files are in the project directory.


Menu Options
┌─ Spell Checker & Vocabulary Explorer ────────────┐
│ 1. Load Dictionary │ 2. Analyze Text │ 3. Categorize │
│ 4. Check Word │ 5. Misspelled │ 6. Unique │
│ 7. Statistics │ 8. Exit │ 9. Levenstein Edit Distamce│
└───────────────────────────────────────────────────┘


Example Workflow
Plain Text1 → Load Dictionary2 sample.txt → Analyze Text3 → Categorize WordsShow more lines

Sample Output
=== Categorize Words ===
✓ SUCCESS: Words categorized!
 - Correctly spelled: 45
 - Misspelled: 18
 - Spelling accuracy: 71.4%

Suggestions for misspelled words:
Misspelled: misspeled → Suggestions: misspelled, misled, misstep
Misspelled: erors → Suggestions: errors, eros, euros
Misspelled: multipel → Suggestions: multiple, multiply, multiplier
Misspelled: becuase → Suggestions: because, case, cause
Misspelled: particulary → Suggestions: particularly, particle, partial
Misspelled: excersize → Suggestions: exercise, excise, expertise


Tech Highlights

HashSet for O(1) lookups and uniqueness
Regex for text normalization
LINQ for sorting and filtering
Levenshtein Distance for spell suggestions