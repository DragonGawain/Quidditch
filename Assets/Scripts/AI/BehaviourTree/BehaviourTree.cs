using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree : MonoBehaviour
{
    // VARIABLES
    [SerializeField] private Node myRootNode = null;

    // Other references.
    [SerializeField] protected NPCMovement myNPCMovement;
    public NPCMovement MyNPCMovement { get { return myNPCMovement; } }

    [SerializeField] protected GroupAI myGroupAI;
    public GroupAI MyGroupAI { get { return myGroupAI; } }



    // METHODS
    protected abstract Node PlantTheTree(BehaviourTree self);

    protected void Awake()
    {
        // Collect other references.
        myNPCMovement = gameObject.GetComponent<NPCMovement>();
        myGroupAI = gameObject.GetComponent<GroupAI>(); // To do: find the better way to fetch the groupAI.
    }
    protected void Start()
    {
        BehaviourTree currentBehaviourTree = this;
        myRootNode = PlantTheTree(this);
    }

    private void Update()
    {
        if (myRootNode != null)
        {
            myRootNode.Execute();
        }
    }
}
