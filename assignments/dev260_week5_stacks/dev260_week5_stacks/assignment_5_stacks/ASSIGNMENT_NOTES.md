# Assignment 5: Browser Navigation System - Implementation Notes
// PP TROENDLE BELLEVUE COLLEGE DEV 260 FALL 2025 SUPERVISOR ZACK

Peter-Paul Troendle

## Dual-Stack Pattern Understanding

**How the dual-stack pattern works for browser navigation:**

The dual-stack pattern uses two stacks—backStack and forwardStack—to simulate browser navigation. When a user visits a new URL, the current page is pushed onto the backStack, and the forwardStack is cleared to invalidate forward history. When navigating back, the current page is pushed onto the forwardStack, and the top of the backStack becomes the new current page. Forward navigation reverses this process. This pattern mimics how browsers allow users to move backward and forward through their browsing history.


## Challenges and Solutions

**Biggest challenge faced:**
Remembering to clear the forwardStack when visiting a new URL after going back.

**How you solved it:**
I reviewed the navigation logic and realized the following points needed to be sorted:

VisitUrl(string url, string title)

Pushes the current page to backStack if it exists.
Clears forwardStack to invalidate forward history.
Sets the new page as currentPage.

GoBack()

Checks if backStack is not empty.
Pushes currentPage to forwardStack.
Pops from backStack and sets it as currentPage.

GoForward()

Checks if forwardStack is not empty.
Pushes currentPage to backStack.
Pops from forwardStack and sets it as currentPage.

DisplayBackHistory() and DisplayForwardHistory()

Uses foreach to iterate through stacks.
Displays history with index, title, and URL.
Handles empty stacks gracefully.

ClearHistory()

Clears both stacks.
Displays confirmation with total pages cleared.

**Most confusing concept:**
Understanding the correct order of operations when moving pages between stacks. Initially, I confused which stack should receive the current page during back and forward navigation.

Issue 1: History Not Cleared Properly
Symptoms:

After choosing option 6 to clear history, old pages still appear in back history.
New pages added after clearing don’t behave as expected.

Likely Cause:

The ClearHistory() method only clears backStack and forwardStack, but does not reset currentPage. So the current page remains, and if you visit new URLs, the old currentPage is pushed to backStack, reintroducing old history.

Fix:
Update your ClearHistory() method to preserve currentPage but prevent it from being pushed back into history unless explicitly navigated away from.
Here’s a revised version:
C#public void ClearHistory(){    int totalCleared = backStack.Count + forwardStack.Count;    backStack.Clear();    forwardStack.Clear();    Console.WriteLine($"✅ Cleared {totalCleared} pages from navigation history.");}Show more lines
This is correct if you want to keep the current page. But if you want to reset everything, including the current page, you can add:
C#currentPage = null;Show more lines
Let me know which behavior you prefer.

public void ClearHistory()
{
    int totalCleared = backStack.Count + forwardStack.Count;
    backStack.Clear();
    forwardStack.Clear();
    Console.WriteLine($"✅ Cleared {totalCleared} pages from navigation history.");
}

currentPage = null;

Issue 2: Navigation Not Working After Visiting New Pages
Symptoms:

After visiting First1, 2, and 1, navigation doesn’t behave correctly.
History shows unexpected entries like http://bing.com/page1 even after clearing.

Likely Cause:

You may be visiting new URLs without going back first, so each new visit pushes the previous page to backStack, even if it was meant to be cleared.

Fix:
Ensure that VisitUrl() only pushes currentPage to backStack if it’s not null and not just recently cleared. You can add a flag like historyJustCleared to prevent pushing the current page immediately after clearing.

Debugging Steps
Try this exact sequence to verify behavior:

Visit http://bing.com/page1 → title Bing
Visit http://First → title First1
Visit http://First → title 2
Visit http://Second → title 1
Show history → should show 3 back entries
Clear history
Show history → should show nothing
Visit http://newsite.com → title New
Show history → should show no back history
Try going back → should say “Cannot go back”

## Code Quality

The clean and readable logic in the VisitUrl, GoBack, and GoForward methods. Each method clearly reflects the intended behavior and handles edge cases gracefully.

**What you would improve if you had more time:**
I would implement the extra credit features like history limits and JSON export/import to enhance session persistence and usability.

## Testing Approach

**How you tested your implementation:**
I manually tested each feature using the provided UI. I visited multiple URLs, navigated back and forward, and verified that history was displayed and cleared correctly.

I tested the following scenarios:

Visiting multiple URLs in sequence.
Navigating back and forward through history.
Attempting back/forward on empty stacks.
Visiting a new URL after going back (confirmed forward history is cleared).
Clearing history and verifying count.

**Issues you discovered during testing:**

Forward history was not cleared after visiting a new URL post-back navigation.
Display formatting needed adjustment for clarity.

## Stretch Features

used ip address instead of domain names to test it did also work
Debuggung Git
Lost files getting them back rollback
git reflog
04c491b HEAD@{0}: commit: Completed Assignment 5: Browser Navigation System – all features tested and working
git reset --hard 04c491b
git pull origin main --rebase
git push origin main



## Time Spent

**Total time:** almost 6 hours

**Breakdown:**

-
Understanding the assignment: 1 hour
Implementing the 6 methods: 2.5 hours
Testing and debugging: 1.5 hours
Writing these notes: 0.5 hours


**Most time-consuming part:** Implementing and testing the VisitUrl method due to its impact on both stacks and the need to handle edge cases correctly.



This assignment helped me understand how stacks can be used to simulate real-world systems like browser navigation. By implementing the dual-stack pattern, I saw firsthand how Stack<T> enables Last-In-First-Out (LIFO) behavior, which is ideal for tracking history in reverse chronological order.


