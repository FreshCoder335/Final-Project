using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create a BST to organize candy bars by their calorie count
        BSTNode root = null;

        // Add some candy bars to the BST
        AddCandyBar(ref root, "Snickers", 250);
        AddCandyBar(ref root, "KitKat", 200);
        AddCandyBar(ref root, "Twix", 220);
        AddCandyBar(ref root, "Milky Way", 240);

        // Display the sorted candy bars by calorie count
        Console.WriteLine("Sorted candy bars by calorie count:");
        InOrderTraversal(root);

        // Search for a candy bar with specific calorie count
        int caloriesToSearch = 220;
        bool exists = Search(root, caloriesToSearch);
        Console.WriteLine($"\nDoes a candy bar with {caloriesToSearch} calories exist? {exists}");

        // Remove a candy bar with specific calorie count
        int caloriesToRemove = 250;
        // Your code here

        // Display the updated list after removal
        Console.WriteLine("\nUpdated sorted candy bars by calorie count:");
        InOrderTraversal(root);
    }

    class BSTNode
    {
        public string CandyBar;
        public int Calories;
        public BSTNode Left, Right;

        public BSTNode(string candyBar, int calories)
        {
            CandyBar = candyBar;
            Calories = calories;
            Left = Right = null;
        }
    }

    static void AddCandyBar(ref BSTNode root, string candyBar, int calories)
    {
        if (root == null)
        {
            root = new BSTNode(candyBar, calories);
            return;
        }

        if (calories < root.Calories)
            AddCandyBar(ref root.Left, candyBar, calories);
        else
            AddCandyBar(ref root.Right, candyBar, calories);
    }

    static void InOrderTraversal(BSTNode root)
    {
        if (root != null)
        {
            InOrderTraversal(root.Left);
            Console.WriteLine($"{root.CandyBar} - {root.Calories} calories");
            InOrderTraversal(root.Right);
        }
    }

    static bool Search(BSTNode root, int calories)
    {
        if (root == null)
            return false;

        if (root.Calories == calories)
            return true;

        if (calories < root.Calories)
            return Search(root.Left, calories);
        else
            return Search(root.Right, calories);
    }

    static void RemoveCandyBar(ref BSTNode root, int calories)
    {
        // Your code here for removing a candy bar with specific calorie count
    }
    static BSTNode FindSuccessor(BSTNode node)
    {
        BSTNode current = node;
        while (current.Left != null)
        {
            current = current.Left;
        }
        return current;
    }
}

