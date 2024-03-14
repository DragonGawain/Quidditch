using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// So far, basic movement functions for all NPCs.
// Should probably rename or split into multiple classes or files.
public class NPCMovement : MonoBehaviour
{
    // VARIABLES
    //float maxVelocity;
    [SerializeField] Vector3 currentVelocity; // to do: hook into RigidBody.

    // Have these as parameters in the specific functions instead?
    //float stopRadius;
    //float slowRadius;
    
    float wanderInterval;
    float wanderTimer;
    Vector3 lastWanderDirection;
    Vector3 lastDisplacement;
    float wanderDegreesDelta;
    // To do: put these as parameter in the function.

    // Trackers for Seek, Arrive, etc.
    [SerializeField] bool hasArrived;
    public bool HasArrived { get { return hasArrived; } set { hasArrived = value; } }



    // METHODS

    // Seek.
    public Vector3 KinematicSeek(Vector3 targetPosition, float maxVelocity, bool setsArrived, float stopRadius = 0f)
    {
        Vector3 desiredVelocity = targetPosition - gameObject.transform.position;

        if (setsArrived && desiredVelocity.magnitude <= stopRadius && !hasArrived)
        {
            hasArrived = true;
            // desiredVelocity *= 0;
        }

        return desiredVelocity.normalized * maxVelocity;
    }
    public Vector3 Seek(Vector3 targetPosition, float maxVelocity, bool setsArrived, float stopRadius = 0f)
    {
        return KinematicSeek(targetPosition, maxVelocity, setsArrived, stopRadius) - currentVelocity;
    }

    // Flee
    public Vector3 KinematicFlee(Vector3 targetPosition, float maxVelocity)
    {
        return -1 * KinematicSeek(targetPosition, maxVelocity, false); // Invertion of the seek behaviour.
    }
    public Vector3 Flee(Vector3 targetPosition, float maxVelocity)
    {
        return KinematicFlee(targetPosition, maxVelocity) - currentVelocity;
    }

    // Arrive
    public Vector3 KinematicArrive(Vector3 targetPosition, float maxVelocity, bool setsArrived, float stopRadius, float slowRadius)
    {
        Vector3 desiredVelocity = targetPosition - gameObject.transform.position;
        float distance = desiredVelocity.magnitude;
        desiredVelocity = desiredVelocity.normalized * maxVelocity;

        // To do: add events?
        if (distance <= stopRadius && !hasArrived)
        {
            if (setsArrived)
            {
                hasArrived = true;
            }
            desiredVelocity *= 0;
        }
        else if (distance < slowRadius)
        {
            desiredVelocity *= distance / slowRadius;
        }

        return desiredVelocity;
    }
    public Vector3 Arrive(Vector3 targetPosition, float maxVelocity, bool setsArrived, float stopRadius, float slowRadius)
    {
        return KinematicArrive(targetPosition, maxVelocity, setsArrived, stopRadius, slowRadius) - currentVelocity;
    }

    // Pursue
    public Vector3 KinematicPursue(Vector3 targetPosition, Vector3 targetVelocity, float maxVelocity, bool setsArrived, float stopRadius = 0f)
    {
        float distance = Vector3.Distance(targetPosition, gameObject.transform.position);
        float ahead = distance / 10; // should the 10 be a parameter?
        Vector3 futurePosition = targetPosition + targetVelocity * ahead;

        return KinematicSeek(futurePosition, maxVelocity, setsArrived, stopRadius);
    }
    public Vector3 Pursue(Vector3 targetPosition, Vector3 targetVelocity, float maxVelocity, bool setsArrived, float stopRadius = 0f)
    {
        return KinematicPursue(targetPosition, targetVelocity, maxVelocity, setsArrived, stopRadius) - currentVelocity;
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
    public Vector3 KinematicWander(float maxVelocity)
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

            lastDisplacement = desiredVelocity;
            lastWanderDirection = direction;
            wanderTimer = 0;
        }

        return desiredVelocity;
    }
    public Vector3 Wander(float maxVelocity)
    {
        return KinematicWander(maxVelocity) - currentVelocity;
    }
}
