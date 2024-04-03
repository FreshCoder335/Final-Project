using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Stack<Apple> appleStack = new Stack<Apple>();

        // Push some apples onto the stack
        appleStack.Push(new Apple("Red"));
        appleStack.Push(new Apple("Green"));
        appleStack.Push(new Apple("Yellow"));
        appleStack.Push(new Apple("Green"));
        appleStack.Push(new Apple("Red"));
        appleStack.Push(new Apple("Yellow"));

        // Sort the apples
        Stack<Apple> sortedApples = SortApples(appleStack);

        // Display sorted apples
        Console.WriteLine("Sorted Apples:");
        while (sortedApples.Count > 0)
        {
            Console.WriteLine(sortedApples.Pop().Color);
        }
    }

    static Stack<Apple> SortApples(Stack<Apple> unsortedApples)
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

        // Sorted stack to hold the apples
        Stack<Apple> sortedApples = new Stack<Apple>();

        // Push apples onto the sorted stack based on color count
        foreach (var kvp in colorCount)
        {
            for (int i = 0; i < kvp.Value; i++)
            {
                sortedApples.Push(new Apple(kvp.Key));
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
