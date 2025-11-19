# Observations: Reality vs Theory
# P TROENDLE - DEV 260 - ZACK - ARRAY SORT WEEK 2 BELLEVUE COLLEGE FALL 2026

## Benchmark Results vs Predictions
The measured benchmark times for membership checks closely matched the predicted Big-O complexities:
- `List<int>` showed linear time growth (O(n)) as expected.
- `HashSet<int>` and `Dictionary<int, bool>` maintained near-constant time (O(1)) even for large N.

## Surprises and Insights
- For small N (e.g., 1,000), `List<int>` was surprisingly fast due to low constant overhead.
- JIT compilation and CPU caching may have contributed to faster-than-expected performance.
- Console output and GC behavior can affect timing if not carefully managed.

## Recommendation for Large Datasets
For large datasets and frequent membership checks, `HashSet<int>` or `Dictionary<int, bool>` are strongly recommended due to their consistent O(1) performance and scalability.