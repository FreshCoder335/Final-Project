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
        bool itExists = Search(root, caloriesToSearch);
        Console.WriteLine($"\nDoes a candy bar with {caloriesToSearch} calories exist? {itExists}");

        // Remove a candy bar with specific calorie count
        int caloriesToRemove = 250;
        // Your code here

        // Display the updated list after removal
        Console.WriteLine("\nUpdated sorted candy bars by calorie count:");
        InOrderTraversal(root);
    }

    class BSTNode
{
    // Definition of a Binary Search Tree (BST) node
    public string CandyBar; // Name of the candy bar
    public int Calories;    // Calories in the candy bar
    public BSTNode Left, Right; // References to left and right child nodes

    // Constructor to initialize a BST node with candy bar name and calories
    public BSTNode(string candyBar, int calories)
    {
        CandyBar = candyBar;
        Calories = calories;
        Left = Right = null; // Initializing left and right child nodes as null
    }
}

// Function to add a candy bar to the BST
static void AddCandyBar(ref BSTNode root, string candyBar, int calories)
{
    // If the tree is empty, create a new node as the root
    if (root == null)
    {
        root = new BSTNode(candyBar, calories);
        return;
    }

    // If calories of the candy bar is less than the current node's calories, go to the left subtree
    // Otherwise, go to the right subtree
    if (calories < root.Calories)
        AddCandyBar(ref root.Left, candyBar, calories);
    else
        AddCandyBar(ref root.Right, candyBar, calories);
}

// Function to perform an in-order traversal of the BST
static void InOrderTraversal(BSTNode root)
{
    
    if (root != null)
    {
        InOrderTraversal(root.Left); 
        Console.WriteLine($"{root.CandyBar} - {root.Calories} calories"); 
        InOrderTraversal(root.Right); 
    }
}

// Function to search for a candy bar with specific calorie count in the BST
static bool Search(BSTNode root, int calories)
{
    // Recursive function to search for a node with specific calorie count
    if (root == null)
        return false; 

    if (root.Calories == calories)
        return true; 

    // If target calories are less than current node's calories, search in left subtree
    // Otherwise, search in right subtree
    if (calories < root.Calories)
        return Search(root.Left, calories);
    else
        return Search(root.Right, calories);
}

// Function to remove a candy bar with specific calorie count from the BST
static void RemoveCandyBar(ref BSTNode root, int calories)
{
    // Your code here for removing a candy bar with specific calorie count
}

// Function to find the successor of a given node in the BST (in-order successor)
static BSTNode FindSuccessor(BSTNode node)
{
    // Function to find the leftmost node in the right subtree of the given node
    BSTNode current = node;
    while (current.Left != null)
    {
        current = current.Left; 
    }
    return current; 
}


