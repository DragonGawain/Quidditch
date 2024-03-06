using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum NodeState
{
    NONE,
    RUNNING,
    SUCCESS,
    FAILURE
}


// The parent class of all behaviour tree nodes.
abstract public class Node
{
    // VARIABLES
    protected NodeState myState;

    protected Node myParentNode;
    public Node MyParentNode { get; set; }


    // CONSTRUCTORS
    public Node(Node parentNode = null)
    {
        myParentNode = parentNode;
    }


    // METHODS

    // Nodes of all types can return success, failure, or still running. 
    // The meaning of those depends on their type: is a condition true? Was the action successful? Was the sequence fully executed? Etc.
    abstract public NodeState Execute(); // Should this be public?


    // To do: implement the blackboard info stuff.


    // Subtypes:
    // - Condition (leaf)
    // - Action (leaf)
    // - Composite: (branch, have children)
    // -> Selector
    // -> Sequence
}

// Parent class of composite nodes.
abstract public class BranchNode : Node
{
    // VARIABLES
    protected bool anyChildIsRunning;

    protected List<Node> myChildNodes;


    // CONSTRUCTOR
    public BranchNode(List<Node> childNodes = null, Node parentNode = null) : base(parentNode)
    {
        myChildNodes = new List<Node>();
        foreach (Node child in childNodes)
        {
            AttachChild(child);
        }
    }


    // METHODS
    public void AttachChild(Node child)
    {
        myChildNodes.Add(child);
        child.MyParentNode = this;
    }
    // To do: add methods for node insertion, deletion, ordering as needed.
}