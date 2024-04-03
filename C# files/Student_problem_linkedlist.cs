using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        LinkedList<string> candyBarList = new LinkedList<string>();

        // Add some candy bars to the linked list
        candyBarList.AddLast("Snickers");
        candyBarList.AddLast("KitKat");
        candyBarList.AddLast("Twix");
        candyBarList.AddLast("Milky Way");

        // Display the current list of candy bars
        Console.WriteLine("Current list of candy bars:");
        DisplayList(candyBarList);

        // Add some more candy bars to the linked list
        candyBarList.AddLast("Hershey's");
        candyBarList.AddLast("Reese's");

        // Display the updated list
        Console.WriteLine("\nUpdated list of candy bars:");
        DisplayList(candyBarList);

        // Search for a specific candy bar in the list
        string candyBarToSearch = "Twix";
        bool exists = Search(candyBarList, candyBarToSearch);
        Console.WriteLine($"\nDoes {candyBarToSearch} exist in the list? {exists}");

        // Remove a specific candy bar from the list
        string candyBarToRemove = "KitKat";
        // Your code here

        // Display the updated list after removal
        Console.WriteLine("\nUpdated list of candy bars after removal:");
        DisplayList(candyBarList);
    }

    static void DisplayList(LinkedList<string> list)
    {
        foreach (string candyBar in list)
        {
            Console.WriteLine(candyBar);
        }
    }

    static bool Search(LinkedList<string> list, string candyBar)
    {
        foreach (string cb in list)
        {
            if (cb == candyBar)
            {
                return true;
            }
        }
        return false;
    }

    // Your code here for removing a specific candy bar from the list
}
