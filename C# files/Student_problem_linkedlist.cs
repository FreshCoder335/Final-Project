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

    // Function to display the contents of a linked list
static void DisplayList(LinkedList<string> list)
{
    // Iterate through each candy bar in the linked list
    foreach (string candyBar in list)
    {
        
        Console.WriteLine(candyBar);
    }
}

// Function to search for a specific candy bar in the linked list
static bool Search(LinkedList<string> list, string candyBar)
{
    // Iterate through each candy bar in the linked list
    foreach (string cb in list)
    {
        // If the current candy bar matches the target candy bar, return true
        if (cb == candyBar)
        {
            return true;
        }
    }
    // If the target candy bar is not found in the linked list, return false
    return false;
}

    static void RemoveCandyBar(LinkedList<string> list, string candyBar)
    {
        // Your code here for removing a specific candy bar from the list
    }
}
