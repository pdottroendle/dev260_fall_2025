using System;
using System.Collections;
using System.Collections.Generic;

namespace Week4DoublyLinkedLists.Core
{
    /*
     * ========================================
     * ASSIGNMENT 3: DOUBLY LINKED LIST IMPLEMENTATION
     * ========================================
     * 
     * 🎯 IMPLEMENTATION GUIDE:
     * Step 1: Node<T> class (already provided below)
     * Step 2: Basic DoublyLinkedList<T> structure (already provided below)
     * Step 3: Add Methods (AddFirst, AddLast, Insert) - START HERE
     * Step 4: Traversal Methods (DisplayForward, DisplayBackward, ToArray)
     * Step 5: Search Methods (Contains, Find, IndexOf)
     * Step 6: Remove Methods (RemoveFirst, RemoveLast, Remove, RemoveAt)
     * Step 7: Advanced Operations (Clear, Reverse)
     * 
     * 💡 TESTING STRATEGY:
     * - Implement each step completely before moving to the next
     * - Use the CoreListDemo to test each step as you complete it
     * - Focus on pointer manipulation - draw diagrams if helpful
     * - Handle edge cases: empty list, single element, etc.
     * 
     * 📚 KEY RESOURCES:
     * - GeeksforGeeks Doubly Linked List: https://www.geeksforgeeks.org/dsa/doubly-linked-list/
     * - Each TODO comment includes specific reference links
     * 
     * 🚀 START WITH: Step 3 (Add Methods) - look for "STEP 3A" below
     */
    /// <summary>
    /// STEP 1: Node class for doubly linked list (✅ COMPLETED)
    /// Contains data and pointers to next and previous nodes
    /// 📚 Reference: https://www.geeksforgeeks.org/dsa/doubly-linked-list/#node-structure
    /// </summary>
    /// <typeparam name="T">Type of data stored in the node</typeparam>
    public class Node<T>
    {
        /// <summary>
        /// Data stored in this node
        /// </summary>
        public T Data { get; set; }
        
        /// <summary>
        /// Reference to the next node in the list
        /// </summary>
        public Node<T>? Next { get; set; }
        
        /// <summary>
        /// Reference to the previous node in the list
        /// </summary>
        public Node<T>? Previous { get; set; }
        
        /// <summary>
        /// Constructor to create a new node with data
        /// </summary>
        /// <param name="data">Data to store in the node</param>
        public Node(T data)
        {
            Data = data;
            Next = null;
            Previous = null;
        }
        
        /// <summary>
        /// String representation of the node for debugging
        /// </summary>
        /// <returns>String representation of the node's data</returns>
        public override string ToString()
        {
            return Data?.ToString() ?? "null";
        }
    }
    
    /// <summary>
    /// STEP 2: Generic doubly linked list implementation (✅ STRUCTURE COMPLETED)
    /// Supports forward and backward traversal with efficient insertion/deletion
    /// 📚 Reference: https://www.geeksforgeeks.org/dsa/doubly-linked-list/
    /// 
    /// 🎯 YOUR TASK: Implement the methods marked with TODO in Steps 3-7
    /// </summary>
    /// <typeparam name="T">Type of elements stored in the list</typeparam>
    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        #region Private Fields
        
        private Node<T>? head;     // First node in the list
        private Node<T>? tail;     // Last node in the list
        private int count;         // Number of elements in the list
        
        #endregion
        
        #region Public Properties
        
        /// <summary>
        /// Gets the number of elements in the list
        /// </summary>
        public int Count => count;
        
        /// <summary>
        /// Gets whether the list is empty
        /// </summary>
        public bool IsEmpty => count == 0;
        
        /// <summary>
        /// Gets the first node in the list (readonly)
        /// </summary>
        public Node<T>? First => head;
        
        /// <summary>
        /// Gets the last node in the list (readonly)
        /// </summary>
        public Node<T>? Last => tail;
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Initialize an empty doubly linked list
        /// </summary>
        public DoublyLinkedList()
        {
            head = null;
            tail = null;
            count = 0;
        }

        #endregion

        #region Step 3: Add Methods - TODO: Students implement these step by step

        /// <summary>
        /// STEP 3A: Add an item to the beginning of the list
        /// Time Complexity: O(1)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/introduction-and-insertion-in-a-doubly-linked-list/#insertion-at-the-beginning-in-doubly-linked-list
        /// </summary>
        /// <param name="item">Item to add</param>
        public void AddFirst(T item)
        {
            var newNode = new Node<T>(item);

            if (IsEmpty)
            {
                head = tail = newNode;
            }
            else
            {
                newNode.Next = head;
                head.Previous = newNode;
                head = newNode;
            }

            count++;
        }

        /// <summary>
        /// STEP 3B: Add an item to the end of the list
        /// Time Complexity: O(1)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/introduction-and-insertion-in-a-doubly-linked-list/#insertion-at-the-end-in-doubly-linked-list
        /// </summary>
        /// <param name="item">Item to add</param>
        public void AddLast(T item)
        {
            var newNode = new Node<T>(item);

            if (IsEmpty)
            {
                head = tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Previous = tail;
                tail = newNode;
            }

            count++;
        }


        /// <summary>
        /// Convenience method - calls AddLast
        /// </summary>
        /// <param name="item">Item to add</param>
        public void Add(T item) => AddLast(item);

        /// <summary>
        /// STEP 3C: Insert an item at a specific index
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/introduction-and-insertion-in-a-doubly-linked-list/#insertion-after-a-given-node-in-doubly-linked-list
        /// </summary>
        /// <param name="index">Index to insert at (0-based)</param>
        /// <param name="item">Item to insert</param>
        public void Insert(int index, T item)
        {
            if (index < 0 || index > count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");

            if (index == 0)
            {
                AddFirst(item);
                return;
            }

            if (index == count)
            {
                AddLast(item);
                return;
            }

            var current = GetNodeAt(index);
            var newNode = new Node<T>(item);

            newNode.Previous = current.Previous;
            newNode.Next = current;

            if (current.Previous != null)
                current.Previous.Next = newNode;

            current.Previous = newNode;

            count++;
        }


        #endregion

        #region Step 4: Traversal and Display Methods - TODO: Students implement these

        /// <summary>
        /// STEP 4A: Display the list in forward direction  
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/#forward-traversal
        /// </summary>
        public void DisplayForward()
        {
            if (IsEmpty)
            {
                Console.WriteLine("⚠️ List is empty.");
                return;
            }

            Console.WriteLine("🔽 Forward Traversal:");
            var current = head;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }
        /// <summary>
        /// STEP 4B: Display the list in backward direction
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/#backward-traversal
        /// </summary>
        public void DisplayBackward()
        {
            if (IsEmpty)
            {
                Console.WriteLine("⚠️ List is empty.");
                return;
            }

            Console.WriteLine("🔼 Backward Traversal:");
            var current = tail;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Previous;
            }
        }

        /// <summary>
        /// STEP 4C: Convert the list to an array
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        /// <returns>Array containing all list elements</returns>
        public T[] ToArray()
        {
            T[] array = new T[count];
            var current = head;
            int index = 0;

            while (current != null)
            {
                array[index++] = current.Data;
                current = current.Next;
            }

            return array;
        }

        #endregion

        #region Step 5: Search Methods - TODO: Students implement these

        /// <summary>
        /// STEP 5A: Check if the list contains a specific item
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/search-an-element-in-a-doubly-linked-list/
        /// </summary>
        /// <param name="item">Item to check for</param>
        /// <returns>True if item is in the list</returns>
        public bool Contains(T item)
        {
            var current = head;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Data, item))
                    return true;

                current = current.Next;
            }

            return false;
        }

        /// <summary>
        /// STEP 5B: Find the first node containing the specified item
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/search-an-element-in-a-doubly-linked-list/
        /// </summary>
        /// <param name="item">Item to find</param>
        /// <returns>Node containing the item, or null if not found</returns>
        public Node<T>? Find(T item)
        {
            var current = head;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Data, item))
                    return current;

                current = current.Next;
            }

            return null;
        }


        /// <summary>
        /// STEP 5C: Find the index of an item
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/search-an-element-in-a-doubly-linked-list/
        /// </summary>
        /// <param name="item">Item to find</param>
        /// <returns>Index of the item, or -1 if not found</returns>
        public int IndexOf(T item)
        {
            var current = head;
            int index = 0;

            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Data, item))
                    return index;

                current = current.Next;
                index++;
            }

            return -1;
        }

        #endregion

        #region Step 6: Remove Methods - TODO: Students implement these

        /// <summary>
        /// STEP 6A: Remove the first item in the list
        /// Time Complexity: O(1)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/#deletion-at-the-beginning-in-doubly-linked-list
        /// </summary>
        /// <returns>The removed item</returns>
        public T RemoveFirst()
        {
            if (IsEmpty)
                throw new InvalidOperationException("List is empty.");

            T removedData = head.Data;

            head = head.Next;

            if (head != null)
                head.Previous = null;
            else
                tail = null;

            count--;
            return removedData;
        }

        /// <summary>
        /// STEP 6B: Remove the last item in the list
        /// Time Complexity: O(1)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/#deletion-at-the-end-in-doubly-linked-list
        /// </summary>
        /// <returns>The removed item</returns>
        public T RemoveLast()
        {
            if (IsEmpty)
                throw new InvalidOperationException("List is empty.");

            T removedData = tail.Data;

            tail = tail.Previous;

            if (tail != null)
                tail.Next = null;
            else
                head = null;

            count--;
            return removedData;
        }

        /// <summary>
        /// STEP 6C: Remove the first occurrence of an item
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <returns>True if item was found and removed</returns>
        public bool Remove(T item)
        {
            var node = Find(item);
            if (node == null)
                return false;

            RemoveNode(node);
            return true;
        }

        /// <summary>
        /// STEP 6D: Remove item at a specific index
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/#deletion-at-a-specific-position-in-doubly-linked-list
        /// </summary>
        /// <param name="index">Index to remove (0-based)</param>
        /// <returns>The removed item</returns>
        public T RemoveAt(int index)
        {
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (index == 0)
                return RemoveFirst();

            if (index == count - 1)
                return RemoveLast();

            var node = GetNodeAt(index);
            T removedData = node.Data;
            RemoveNode(node);
            return removedData;
        }


        #endregion

        #region Step 7: Advanced Operations - TODO: Students implement these

        /// <summary>
        /// STEP 7A: Remove all items from the list
        /// Time Complexity: O(1)
        /// 📚 Reference: https://docs.microsoft.com/en-us/dotnet/standard/collections/
        /// </summary>
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }


        /// <summary>
        /// STEP 7B: Reverse the list in-place
        /// Time Complexity: O(n)
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/reverse-a-doubly-linked-list/
        /// </summary>
        public void Reverse()
        {
            if (IsEmpty || head == tail)
                return;

            var current = head;
            Node<T>? temp = null;

            while (current != null)
            {
                temp = current.Previous;
                current.Previous = current.Next;
                current.Next = temp;
                current = current.Previous;
            }

            // Swap head and tail
            temp = head;
            head = tail;
            tail = temp;
        }

        #endregion

        #region Helper Methods - TODO: Students may need these for advanced operations

        /// <summary>
        /// Get node at specific index (helper for internal use)
        /// Optimizes traversal by starting from head or tail based on index
        /// Used by Insert, RemoveAt, and other positional operations
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/traversal-in-doubly-linked-list/
        /// </summary>
        /// <param name="index">Index to get node at (0-based)</param>
        /// <returns>Node at the specified index</returns>
        private Node<T> GetNodeAt(int index)
        {
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");

            Node<T>? current;

            if (index < count / 2)
            {
                current = head;
                for (int i = 0; i < index; i++)
                {
                    current = current?.Next;
                }
            }
            else
            {
                current = tail;
                for (int i = count - 1; i > index; i--)
                {
                    current = current?.Previous;
                }
            }

            return current!;
        }


        /// <summary>
        /// Remove a specific node from the list (helper method)
        /// Handles all the pointer manipulation for node removal
        /// Used by Remove, RemoveAt, and other removal operations
        /// 📚 Reference: https://www.geeksforgeeks.org/dsa/delete-a-node-in-a-doubly-linked-list/
        /// </summary>
        /// <param name="node">Node to remove (must not be null)</param>
        private void RemoveNode(Node<T> node)
        {
            if (node == head && node == tail)
            {
                head = tail = null;
            }
            else if (node == head)
            {
                head = head.Next;
                if (head != null)
                    head.Previous = null;
            }
            else if (node == tail)
            {
                tail = tail.Previous;
                if (tail != null)
                    tail.Next = null;
            }
            else
            {
                node.Previous.Next = node.Next;
                node.Next.Previous = node.Previous;
            }

            count--;
        }

        #endregion

        #region IEnumerable Implementation

        /// <summary>
        /// Get enumerator for foreach support
        /// </summary>
        /// <returns>Enumerator for the list</returns>
        public IEnumerator<T> GetEnumerator()
        {
            Node<T>? current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
        
        /// <summary>
        /// Non-generic enumerator implementation
        /// </summary>
        /// <returns>Non-generic enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        #endregion
        
        #region Display Methods for Testing and Debugging
        
        /// <summary>
        /// Display detailed information about the list structure
        /// Perfect for testing and understanding the list state
        /// </summary>
        public void DisplayInfo()
        {
            Console.WriteLine("=== DOUBLY LINKED LIST STATE ===");
            Console.WriteLine($"Count: {Count}");
            Console.WriteLine($"IsEmpty: {IsEmpty}");
            Console.WriteLine($"First: {(head?.Data?.ToString() ?? "null")}");
            Console.WriteLine($"Last: {(tail?.Data?.ToString() ?? "null")}");
            Console.WriteLine();
            
            // Show both traversal directions
            try
            {
                DisplayForward();
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("Forward:  [TODO: Implement DisplayForward in Step 4a]");
            }
            
            try
            {
                DisplayBackward();
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("Backward: [TODO: Implement DisplayBackward in Step 4b]");
            }
            
            Console.WriteLine();
        }
        
        #endregion
    }
}