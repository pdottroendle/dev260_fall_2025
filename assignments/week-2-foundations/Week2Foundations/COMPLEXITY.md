# Big-O Complexity Predictions
# P TROENDLE - DEV 260 - ZACK - ARRAY SORT WEEK 2 BELLEVUE COLLEGE FALL 2026

| Structure         | Operation                    | Big-O (Avg) | One-sentence rationale                                      |
|------------------|------------------------------|-------------|-------------------------------------------------------------|
| Array            | Access by index              | O(1)        | Direct index access is constant time due to fixed memory layout. |
| Array            | Search (unsorted)            | O(n)        | Linear search is required since elements are not ordered.       |
| List<T>          | Add at end                   | O(1)        | Adding at the end is typically constant unless resizing occurs. |
| List<T>          | Insert at index              | O(n)        | Inserting requires shifting elements to make space.             |
| Stack<T>         | Push / Pop / Peek            | O(1)        | Stack operations are constant time due to top-only access.     |
| Queue<T>         | Enqueue / Dequeue / Peek     | O(1)        | Queue operations are constant time with linked or circular buffer. |
| Dictionary<K,V>  | Add / Lookup / Remove        | O(1)        | Hash table operations are constant time on average.            |
| HashSet<T>       | Add / Contains / Remove      | O(1)        | HashSet uses hashing for constant time membership checks.      |