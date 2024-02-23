using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// The parent class of all behaviour tree nodes.
abstract public class Node
{
    // Nodes of all types can return success or failure (true or false). 
    // The meaning of those depends on their type: is a condition true? Was the action successful? Was the sequence fully executed? Etc.
    abstract public bool Execute(); // Should this be public?

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
    protected List<Node> myChildNodes;

    // To do: add methods for node insertion, deletion, ordering as needed.
}

// Parent class of leaf nodes.
// Is this class necessary, or should we directly create Condition and Action nodes?
// Are those necessary classes??
abstract public class LeafNode : Node
{

}