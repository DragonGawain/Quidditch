using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // VARIABLES
    // Should speed and the like be defined here, or by the child classes?
    // Do we need a variable for being held/handled? Should it be exclusive to the quaffle?

    // References
    [SerializeField] protected NPCMovement myNPCMovement;
    public NPCMovement MyNPCMovement { get { return myNPCMovement; } }



    // METHODS.
    private void Awake()
    {
        myNPCMovement = GetComponent<NPCMovement>();
    }

    // Get the closest target
    protected Transform GetClosestTarget(string tag)
    {
        GameObject[] targets;                   // potential targets
        Transform target = null;                // the closest target
        float minDistance = Mathf.Infinity;     // min distance between the current ball and closest target

        // Get the potential targets for the current ball
        targets = GameObject.FindGameObjectsWithTag(tag);

        // Get the closest target out of the potential ones
        foreach (GameObject t in targets)
        {
            float distance = Vector3.Distance(t.transform.position, transform.position);
            if (distance < minDistance)
            {
                target = t.transform;
                minDistance = distance;
            }
        }

        return target;
    }
}
