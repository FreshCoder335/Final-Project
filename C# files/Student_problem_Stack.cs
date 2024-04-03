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
        // RemoveAllInstances(candyBarStack, candyBarToRemove);  // Student's task to implement this method

        // Display the updated stack after removal
        Console.WriteLine("\nUpdated stack of candy bars after removal:");
        DisplayStack(candyBarStack);
    }

    // Function to display the contents of a stack
static void DisplayStack(Stack<string> stack)
{
    // Iterate through each candy bar in the stack
    foreach (string candyBar in stack)
    {
        // Print the name of the candy bar
        Console.WriteLine(candyBar);
    }
}

// Function to check if a specific candy bar exists in the stack
static bool CheckIfExists(Stack<string> stack, string candyBar)
{
    // Iterate through each candy bar in the stack
    foreach (string cb in stack)
    {
        // If the current candy bar matches the target candy bar, return true
        if (cb == candyBar)
        {
            return true;
        }
    }
    // If the target candy bar is not found in the stack, return false
    return false;
}


    static void RemoveAllInstances(stack<string> stack, string candyBar)
    {
        //Your code here
    }
