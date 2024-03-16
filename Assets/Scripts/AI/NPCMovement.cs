using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// So far, basic movement functions for all NPCs.
// Should probably rename or split into multiple classes or files.
public class NPCMovement : MonoBehaviour
{
    // VARIABLES
    // References
    protected Rigidbody myRigidbody;
    public Rigidbody MyRigidbody { get { return myRigidbody; } }

    // To do: hook currentVelocity into the rigidBody stuff.
    private Vector3 currentVelocity;



    // METHODS
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }


    // Seek.
    public Vector3 KinematicSeek(Vector3 targetPosition, float maxVelocity)
    {
        Vector3 desiredVelocity = targetPosition - gameObject.transform.position;
        return desiredVelocity.normalized * maxVelocity;
    }
    public Vector3 Seek(Vector3 targetPosition, float maxVelocity)
    {
        return KinematicSeek(targetPosition, maxVelocity) - currentVelocity;
    }

    // Flee
    public Vector3 KinematicFlee(Vector3 targetPosition, float maxVelocity)
    {
        return -1 * KinematicSeek(targetPosition, maxVelocity); // Invertion of the seek behaviour.
    }
    public Vector3 Flee(Vector3 targetPosition, float maxVelocity)
    {
        return KinematicFlee(targetPosition, maxVelocity) - currentVelocity;
    }

    // Arrive
    public Vector3 KinematicArrive(Vector3 targetPosition, float maxVelocity, float stopRadius, float slowRadius)
    {
        Vector3 desiredVelocity = targetPosition - gameObject.transform.position;
        float distance = desiredVelocity.magnitude;
        desiredVelocity = desiredVelocity.normalized * maxVelocity;

        // To do: add events?
        if (distance <= stopRadius)
        {
            desiredVelocity *= 0;
        }
        else if (distance < slowRadius)
        {
            desiredVelocity *= distance / slowRadius;
        }

        return desiredVelocity;
    }
    public Vector3 Arrive(Vector3 targetPosition, float maxVelocity, float stopRadius, float slowRadius)
    {
        return KinematicArrive(targetPosition, maxVelocity, stopRadius, slowRadius) - currentVelocity;
    }

    // Pursue
    public Vector3 KinematicPursue(Vector3 targetPosition, Vector3 targetVelocity, float maxVelocity)
    {
        float distance = Vector3.Distance(targetPosition, gameObject.transform.position);
        float ahead = distance / 10; // should the 10 be a parameter?
        Vector3 futurePosition = targetPosition + targetVelocity * ahead;

        return KinematicSeek(futurePosition, maxVelocity);
    }
    public Vector3 Pursue(Vector3 targetPosition, Vector3 targetVelocity, float maxVelocity)
    {
        return KinematicPursue(targetPosition, targetVelocity, maxVelocity) - currentVelocity;
    }

    // Evade
    public Vector3 KinematicEvade(Vector3 targetPosition, Vector3 targetVelocity, float maxVelocity)
    {
        float distance = Vector3.Distance(targetPosition, gameObject.transform.position);
        float ahead = distance / 10; // should the 10 be a parameter?
        Vector3 futurePosition = targetPosition + targetVelocity * ahead;

        return KinematicFlee(futurePosition, maxVelocity);
    }
    public Vector3 Evade(Vector3 targetPosition, Vector3 targetVelocity, float maxVelocity)
    {
        return KinematicEvade(targetPosition, targetVelocity, maxVelocity) - currentVelocity;
    }

    // Wander
    public Vector3 KinematicWander(float maxVelocity, float wanderInterval, ref float wanderTimer, ref Vector3 lastWanderDirection, ref Vector3 lastDisplacement, float wanderDegreesDelta)
    {
        wanderTimer += Time.deltaTime;

        if (lastWanderDirection == Vector3.zero)
        {
            lastWanderDirection = gameObject.transform.forward.normalized * maxVelocity;
        }

        if (lastDisplacement == Vector3.zero)
        {
            lastDisplacement = gameObject.transform.forward;
        }

        Vector3 desiredVelocity = lastDisplacement;
        if (wanderTimer > wanderInterval)
        {
            float angle = (Random.value - Random.value) * wanderDegreesDelta;
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * lastWanderDirection.normalized;
            Vector3 circleCenter = gameObject.transform.position + lastDisplacement;
            Vector3 destination = circleCenter + direction.normalized;
            desiredVelocity = destination - gameObject.transform.position;
            desiredVelocity = desiredVelocity.normalized * maxVelocity;

            // To do: hook these back in?
            lastDisplacement = desiredVelocity;
            lastWanderDirection = direction;
            wanderTimer = 0;
        }

        return desiredVelocity;
    }
    public Vector3 Wander(float maxVelocity, float wanderInterval, ref float wanderTimer, ref Vector3 lastWanderDirection, ref Vector3 lastDisplacement, float wanderDegreesDelta)
    {
        return KinematicWander(maxVelocity, wanderInterval, ref wanderTimer, ref lastWanderDirection, ref lastDisplacement, wanderDegreesDelta) - currentVelocity;
    }


    // 
}
