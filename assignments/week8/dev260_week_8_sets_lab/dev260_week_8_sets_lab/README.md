DEV 260
SUPERVISION BY ZAK
STUDENT PETER PAUL TROENDLE
BELLEVUE COLLEGE 
FALL 2025 


Week 8 - Lab: 

Lists and HasSets

This lab demonstrates **HashSet<T> operations** and real-world applications like email deduplication, permission management, and enrollment analysis.

### Achieved
- **Email Deduplication** (case-insensitive)
- **Permission Management** with O(1) lookups
- **Enrollment Analysis** (new, dropped, continuing students)
- **Retention Rate Calculation**

### Concepts
- **HashSet<T> vs List<T>**
  - HashSet: O(1) membership checks, automatic uniqueness
  - List: O(n) membership checks, allows duplicates
- **Set Operations**
  - Union: Combine permissions
  - Intersection: Continuing students
  - Difference: New or dropped students

### Completed
1. `DeduplicateEmails()`
2. `HasPermission()`
3. `AddPermissions()`
4. `GetMissingPermissions()`
5. `FindNewStudents()`
6. `FindDroppedStudents()`
7. `FindContinuingStudents()`
8. `CalculateRetentionRate()`

### How to Run
1. Clone the repo:
   ```bash
   git clone gitrepo