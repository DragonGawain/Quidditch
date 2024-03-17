using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snitch : Ball
{
    // VARIABLES
    // Should speed and the like be defined here, or by the parent classe?
    [SerializeField] Transform target;
    float speed = 1f;



    // METHODS.
    // Start is called before the first frame update
    void Start() {  }

    // Update is called once per frame
    void Update()
    {
        // Get the closest seeker to flee away from them
        target = GetClosestTarget("seeker");

        Vector3 desiredVelocity = myNPCMovement.Flee(target.position, speed);
        //Vector3 desiredVelocity = myNPCMovement.KinematicFlee(target.position, speed);

        transform.position += desiredVelocity * Time.deltaTime;
    }
}
