using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree : MonoBehaviour
{
    // VARIABLES
    [SerializeField] private Node myRootNode = null;

    // References.
    [SerializeField] protected NPCMovement myNPCMovement;
    public NPCMovement MyNPCMovement { get { return myNPCMovement; } }

    [SerializeField] protected GroupAI myGroupAI;
    public GroupAI MyGroupAI { get { return myGroupAI; } }

    // Speeds and the like.
    [SerializeField] protected float myMaxSpeed = 1f;
    public float MyMaxSpeed { get { return myMaxSpeed; } }



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



    // UTILITY METHODS
    public (GameObject, float) LocateClosestBludger(GameObject closestBludger)
    {
        float distanceToClosestBludger = (closestBludger != null) ? Vector3.Distance(this.gameObject.transform.position, closestBludger.transform.position) : float.MaxValue;
        foreach (Bludger bludger in this.MyGroupAI.TheBludgers)
        {
            // Assuming that we have only two bludgers.
            if (bludger.gameObject != closestBludger && Vector3.Distance(this.gameObject.transform.position, bludger.gameObject.transform.position) < distanceToClosestBludger)
            {
                closestBludger = bludger.gameObject;
                distanceToClosestBludger = Vector3.Distance(this.gameObject.transform.position, bludger.gameObject.transform.position);
                break;
            }
        }

        return (closestBludger, distanceToClosestBludger);
    }
}
