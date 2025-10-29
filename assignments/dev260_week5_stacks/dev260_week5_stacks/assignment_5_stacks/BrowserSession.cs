using System;
using System.Collections.Generic;

namespace Assignment5
{
    public class BrowserSession
    {
        private Stack<WebPage> backStack;
        private Stack<WebPage> forwardStack;
        private WebPage? currentPage;

        public WebPage? CurrentPage => currentPage;
        public int BackHistoryCount => backStack.Count;
        public int ForwardHistoryCount => forwardStack.Count;
        public bool CanGoBack => backStack.Count > 0;
        public bool CanGoForward => forwardStack.Count > 0;

        public BrowserSession()
        {
            backStack = new Stack<WebPage>();
            forwardStack = new Stack<WebPage>();
            currentPage = null;
        }

        public void VisitUrl(string url, string title)
        {
            if (currentPage != null)
            {
                backStack.Push(currentPage);
            }
            forwardStack.Clear();
            currentPage = new WebPage(url, title);
        }

        public bool GoBack()
        {
            if (!CanGoBack) return false;

            if (currentPage != null)
            {
                forwardStack.Push(currentPage);
            }
            currentPage = backStack.Pop();
            return true;
        }

        public bool GoForward()
        {
            if (!CanGoForward) return false;

            if (currentPage != null)
            {
                backStack.Push(currentPage);
            }
            currentPage = forwardStack.Pop();
            return true;
        }

        public void DisplayBackHistory()
        {
            Console.WriteLine("📚 Back History (most recent first):");
            if (backStack.Count == 0)
            {
                Console.WriteLine(" (No back history)");
                return;
            }

            int index = 1;
            foreach (var page in backStack)
            {
                Console.WriteLine($" {index}. {page.Title} ({page.Url})");
                index++;
            }
        }

        public void DisplayForwardHistory()
        {
            Console.WriteLine("📖 Forward History (next page first):");
            if (forwardStack.Count == 0)
            {
                Console.WriteLine(" (No forward history)");
                return;
            }

            int index = 1;
            foreach (var page in forwardStack)
            {
                Console.WriteLine($" {index}. {page.Title} ({page.Url})");
                index++;
            }
        }

public void ClearHistory()
{
    int totalCleared = backStack.Count + forwardStack.Count;
    backStack.Clear();
    forwardStack.Clear();
    Console.WriteLine($" Cleared {totalCleared} pages from navigation history.");
	currentPage = null;
}

        public string GetNavigationStatus()
        {
            var status = $"📊 Navigation Status:\n";
            status += $" Back History: {BackHistoryCount} pages\n";
            status += $" Forward History: {ForwardHistoryCount} pages\n";
            status += $" Can Go Back: {(CanGoBack ? "✅ Yes" : "❌ No")}\n";
            status += $" Can Go Forward: {(CanGoForward ? "✅ Yes" : "❌ No")}";
            return status;
        }
    }
}