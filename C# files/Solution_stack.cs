using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Stack<string> candyBarStack = new Stack<string>();

        // Push some candy bars onto the stack
        candyBarStack.Push("Snickers");
        candyBarStack.Push("KitKat");
        candyBarStack.Push("Twix");
        candyBarStack.Push("Milky Way");

        // Display the current stack of candy bars
        Console.WriteLine("Current stack of candy bars:");
        DisplayStack(candyBarStack);

        // Simulate eating the top candy bar
        string eatenCandyBar = candyBarStack.Pop();
        Console.WriteLine($"\nJust ate a {eatenCandyBar}.");

        // Add some more candy bars to the stack
        candyBarStack.Push("Hershey's");
        candyBarStack.Push("Reese's");

        // Display the updated stack after eating
        Console.WriteLine("\nUpdated stack of candy bars:");
        DisplayStack(candyBarStack);

        // Check if a specific candy bar exists in the stack
        string candyBarToCheck = "KitKat";
        bool exists = CheckIfExists(candyBarStack, candyBarToCheck);
        Console.WriteLine($"\nDoes {candyBarToCheck} exist in the stack? {exists}");

        // Remove all instances of a specific candy bar from the stack
        string candyBarToRemove = "Twix";
        RemoveAllInstances(candyBarStack, candyBarToRemove);
        Console.WriteLine($"\nRemoved all instances of {candyBarToRemove} from the stack.");

        // Display the updated stack after removal
        Console.WriteLine("\nUpdated stack of candy bars after removal:");
        DisplayStack(candyBarStack);
    }

    static void DisplayStack(Stack<string> stack)
    {
        foreach (string candyBar in stack)
        {
            Console.WriteLine(candyBar);
        }
    }

    static bool CheckIfExists(Stack<string> stack, string candyBar)
    {
        foreach (string cb in stack)
        {
            if (cb == candyBar)
            {
                return true;
            }
        }
        return false;
    }

    static void RemoveAllInstances(Stack<string> stack, string candyBar)
    {
        Stack<string> tempStack = new Stack<string>();

        while (stack.Count > 0)
        {
            string current = stack.Pop();
            if (current != candyBar)
            {
                tempStack.Push(current);
            }
        }

        while (tempStack.Count > 0)
        {
            stack.Push(tempStack.Pop());
        }
    }
}
