using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        LinkedList<Apple> appleList = new LinkedList<Apple>();

        // Add some apples to the linked list
        appleList.AddLast(new Apple("Red"));
        appleList.AddLast(new Apple("Green"));
        appleList.AddLast(new Apple("Yellow"));
        appleList.AddLast(new Apple("Green"));
        appleList.AddLast(new Apple("Red"));
        appleList.AddLast(new Apple("Yellow"));

        // Sort the apples
        LinkedList<Apple> sortedApples = SortApples(appleList);

        // Display sorted apples
        Console.WriteLine("Sorted Apples:");
        foreach (Apple apple in sortedApples)
        {
            Console.WriteLine(apple.Color);
        }
    }

    static LinkedList<Apple> SortApples(LinkedList<Apple> unsortedApples)
    {
        // Dictionary to store frequency of each apple color
        Dictionary<string, int> colorCount = new Dictionary<string, int>();
        foreach (Apple apple in unsortedApples)
        {
            if (colorCount.ContainsKey(apple.Color))
            {
                colorCount[apple.Color]++;
            }
            else
            {
                colorCount[apple.Color] = 1;
            }
        }

        // Sorted linked list to hold the apples
        LinkedList<Apple> sortedApples = new LinkedList<Apple>();

        // Add apples to the sorted linked list based on color count
        foreach (var kvp in colorCount)
        {
            for (int i = 0; i < kvp.Value; i++)
            {
                sortedApples.AddLast(new Apple(kvp.Key));
            }
        }

        return sortedApples;
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
