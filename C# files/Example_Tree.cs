using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Apple> apples = new List<Apple>
        {
            new Apple("Red"),
            new Apple("Green"),
            new Apple("Yellow"),
            new Apple("Green"),
            new Apple("Red"),
            new Apple("Yellow"),
            new Apple("Red"),
            new Apple("Red")
        };

        // Construct a frequency table using data from web on rarity of apple type
        Dictionary<string, int> colorFrequency = new Dictionary<string, int>();
        foreach (Apple apple in apples)
        {
            if (colorFrequency.ContainsKey(apple.Color))
                colorFrequency[apple.Color]++;
            else
                colorFrequency[apple.Color] = 1;
        }

        // Construct the BST using frequency as the key
        BSTNode root = null;
        foreach (var entry in colorFrequency)
        {
            root = Insert(root, entry.Key, entry.Value);
        }

        // Traverse the BST in-order to get sorted apples
        List<Apple> sortedApples = new List<Apple>();
        InOrderTraversal(root, sortedApples);

        // Display sorted apples
        Console.WriteLine("Sorted Apples (most common to least common):");
        foreach (Apple apple in sortedApples)
        {
            Console.WriteLine(apple.Color);
        }
    }

    // Node for the BST
    class BSTNode
    {
        public string Color;
        public int Frequency;
        public BSTNode Left, Right;

        public BSTNode(string color, int frequency)
        {
            Color = color;
            Frequency = frequency;
            Left = Right = null;
        }
    }

    // Insert a node into the BST
    static BSTNode Insert(BSTNode root, string color, int frequency)
    {
        if (root == null)
            return new BSTNode(color, frequency);

        if (frequency > root.Frequency)
            root.Right = Insert(root.Right, color, frequency);
        else
            root.Left = Insert(root.Left, color, frequency);

        return root;
    }

    // In-order traversal of the BST
    static void InOrderTraversal(BSTNode root, List<Apple> sortedApples)
    {
        if (root != null)
        {
            InOrderTraversal(root.Right, sortedApples);
            for (int i = 0; i < root.Frequency; i++)
            {
                sortedApples.Add(new Apple(root.Color));
            }
            InOrderTraversal(root.Left, sortedApples);
        }
    }
}

class Apple
{
    public string Color { get; }

    public Apple(string color)
    {
        Color = color;
    }
}
