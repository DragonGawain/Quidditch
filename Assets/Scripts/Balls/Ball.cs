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
}
