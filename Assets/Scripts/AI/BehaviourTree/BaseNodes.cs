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

    protected BehaviourTree myParentTree;
    public BehaviourTree MyParentTree { get { return myParentTree; } }

    protected Node myParentNode;
    public Node MyParentNode { get { return myParentNode; } set { myParentNode = value; } }

    protected Dictionary<string, object> blackboard = new Dictionary<string, object>();



    // CONSTRUCTORS
    public Node(BehaviourTree parentTree)
    {
        myParentTree = parentTree;
    }



    // METHODS

    // Nodes of all types can return success, failure, or still running. 
    // The meaning of those depends on their type: is a condition true? Was the action successful? Was the sequence fully executed? Etc.
    virtual public NodeState Execute() => NodeState.FAILURE; // Should this be public?


    // Blackboard / Data Context.
    public void WriteToBlackboard(string key, object value)
    {
        blackboard[key] = value;
    }
    public object ReadFromBlackboard(string key)
    {
        object value = null;

        if (blackboard.TryGetValue(key, out value))
        {
            return value;
        }

        Node currentNode = myParentNode;
        while (currentNode != null)
        {
            value = currentNode.ReadFromBlackboard(key);
            if (value != null)
            {
                return value;
            }
            currentNode = currentNode.MyParentNode;
        }

        return null;
    }

    public bool ClearFromBlackboard(string key)
    {
        if (blackboard.ContainsKey(key))
        {
            blackboard.Remove(key);
            return true;
        }

        Node currentNode = myParentNode;
        while (currentNode != null)
        {
            bool cleared = currentNode.ClearFromBlackboard(key);
            if (cleared)
            {
                return true;
            }
            currentNode = currentNode.myParentNode;
        }
        return false;
    }


    // Randomize the node's result (success or failure).
    // Threshold: a value between 0 and 1 above which we'll consider a success.
    protected NodeState RandomResult(float threshold = 0.5f)
    {
        if (Random.Range(0f, 1f) >= threshold)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }



    // Subtypes:
    // - Condition (leaf)
    // - Action (leaf)
    // - Composite: (branch, have children)
    // -> Selector
    // -> Sequence
}


// Parent class of composite nodes.
abstract public class NodeBranch : Node
{
    // VARIABLES
    protected List<Node> myChildNodes;



    // CONSTRUCTOR
    public NodeBranch(BehaviourTree parentTree, List<Node> childNodes = null) : base(parentTree)
    {
        if (childNodes != null)
        {
            myChildNodes = new List<Node>();
            foreach (Node child in childNodes)
            {
                AttachChild(child);
            }
        }
    }



    // METHODS
    public void AttachChild(Node child)
    {
        myChildNodes.Add(child);
        child.MyParentNode = this;
    }
    // To do: add methods for node insertion, deletion, ordering if needed.


    // Randomize the order of child nodes.
    public void ShuffleChildren()
    {
        // Reference: https://stackoverflow.com/questions/273313/randomize-a-listt

        int noOfChildNodes = myChildNodes.Count;
        while (noOfChildNodes > 1)
        {
            noOfChildNodes--;

            int newId = Random.Range(0, noOfChildNodes + 1);
            Node currentNode = myChildNodes[newId];
            myChildNodes[newId] = myChildNodes[noOfChildNodes];
            myChildNodes[noOfChildNodes] = currentNode;
        }
    }
}