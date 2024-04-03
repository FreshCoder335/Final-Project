# Linked Lists

## Introduction

A linked list is a fundamental data structure in computer science used to store a collection of elements. Unlike arrays, linked lists do not store elements in contiguous memory locations. Instead, each element, known as a node, points to the next node in the sequence. This allows for efficient insertion and deletion operations, especially when the size of the list is dynamic and unknown beforehand.

## Structure

A linked list consists of nodes where each node contains two components:

1. Data: This component holds the value of the element.
2. Next Pointer: This component points to the next node in the sequence.

The last node typically points to `null`, indicating the end of the list.

## Usage

Linked lists are commonly used in scenarios where:

- The size of the data structure needs to be dynamic and can change frequently.
- Efficient insertion and deletion operations are required.
- Memory usage needs to be optimized, as linked lists can allocate memory dynamically.

## Problems Solved

### 1. Dynamic Memory Allocation

Linked lists allocate memory dynamically, enabling efficient memory usage as nodes can be allocated and deallocated as needed.

### 2. Insertion and Deletion

Insertion and deletion operations in linked lists have a time complexity of O(1) when performed at the beginning or end of the list, unlike arrays where these operations might have a time complexity of O(n) due to shifting elements.

### 3. Dynamic Size

Linked lists can grow or shrink dynamically without needing to pre-allocate memory, making them suitable for scenarios where the size of the data structure is not known beforehand.

## Code Examples

### 1. Creating a Linked List Node

```csharp
public class Node<T>
{
    public T Data { get; set; }
    public Node<T> Next { get; set; }

    public Node(T data)
    {
        Data = data;
        Next = null;
    }
}
```
2. Creating a Linked List and Adding Elements
```csharp

public class LinkedList<T>
{
    private Node<T> head;

    public LinkedList()
    {
        head = null;
    }

    public void Add(T data)
    {
        Node<T> newNode = new Node<T>(data);
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node<T> current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
    }
}
```

## Visual Example:

![Diagram and Documentation of Linked list](https://www.geeksforgeeks.org/linked-list-implementation-in-c-sharp/)



## Conclusion
Linked lists provide a flexible and efficient way to store and manipulate data, especially in scenarios where dynamic memory allocation and frequent insertions and deletions are required. Understanding the structure and operations of linked lists is essential for every programmer, as they are widely used in various applications and form the basis for more complex data structures.