# Trees

## Introduction

A tree is a hierarchical data structure consisting of nodes connected by edges. It is widely used in computer science and serves as the foundation for various algorithms and data structures. Trees are recursive structures, where each node may have zero or more child nodes, forming a branching structure resembling a real-world tree.

## Structure

A tree consists of nodes where each node contains:

1. Data: This component holds the value of the node.
2. Parent Pointer: This component points to the parent node (except for the root node).
3. Child Pointers: These components point to the child nodes.

A tree has a root node, which serves as the entry point, and each node in the tree is connected by edges.

## Documentaion Definiton
a tree is a hierarchical data structure consisting of nodes connected by edges. Each node in a tree has a value (referred to as data) and zero or more child nodes, forming a branching structure resembling a real-world tree. The topmost node in a tree is called the root, and each node may have a parent node except for the root, which has no parent. Nodes with no children are called leaf nodes, and nodes with at least one child are called internal nodes.

## Usage

Trees are used in various applications, including:

- Hierarchical data representation: Trees are commonly used to represent hierarchical structures such as file systems, organizational charts, and XML/HTML documents.
- Searching and sorting: Binary search trees are efficient for searching, inserting, and deleting elements.
- Decision making: Decision trees are used in machine learning and artificial intelligence for decision-making processes.
- Routing algorithms: Trees are used in network routing algorithms to efficiently route data packets.

## Problems Solved

### 1. Hierarchical Representation

Trees provide a natural and efficient way to represent hierarchical data structures, allowing for easy traversal and manipulation.

### 2. Efficient Searching

Binary search trees enable efficient searching, insertion, and deletion operations with an average time complexity of O(log n).

### 3. Decision Making

Decision trees provide a visual representation of decision-making processes, aiding in understanding and optimizing complex decision scenarios.

## Code Examples

### 1. Creating a Tree Node

```csharp
public class TreeNode<T>
{
    public T Data { get; set; }
    public TreeNode<T> Parent { get; set; }
    public List<TreeNode<T>> Children { get; set; }

    public TreeNode(T data)
    {
        Data = data;
        Parent = null;
        Children = new List<TreeNode<T>>();
    }
}
```
2. Creating a Binary Search Tree and Inserting Elements
```csharp

public class BinarySearchTree<T> where T : IComparable<T>
{
    private TreeNode<T> root;

    public BinarySearchTree()
    {
        root = null;
    }

    public void Insert(T data)
    {
        if (root == null)
        {
            root = new TreeNode<T>(data);
        }
        else
        {
            InsertRecursively(root, data);
        }
    }

    private void InsertRecursively(TreeNode<T> node, T data)
    {
        if (data.CompareTo(node.Data) < 0)
        {
            if (node.Children.Count == 0)
            {
                TreeNode<T> newNode = new TreeNode<T>(data);
                newNode.Parent = node;
                node.Children.Add(newNode);
            }
            else
            {
                InsertRecursively(node.Children[0], data);
            }
        }
        else
        {
            if (node.Children.Count < 2)
            {
                TreeNode<T> newNode = new TreeNode<T>(data);
                newNode.Parent = node;
                node.Children.Add(newNode);
            }
            else
            {
                InsertRecursively(node.Children[1], data);
            }
        }
    }
}
```

## Visual Example:

![Tree Diagram and Documentation](https://www.c-sharpcorner.com/article/tree-data-structure/)





## Conclusion
Trees are versatile data structures with numerous applications in computer science and beyond. Understanding the structure and operations of trees is essential for designing efficient algorithms and solving various computational problems.