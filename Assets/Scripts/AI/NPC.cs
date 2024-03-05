using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// So far, basic movement functions for NPCs.
// Should probably rename or split into multiple classes or files.
public class NPC : MonoBehaviour
{
    // VARIABLES
    float maxVelocity;
    Vector3 currentVelocity; // to do: hook into RigidBody.

    // Have these as parameters in the specific functions instead?
    float stopRadius;
    float slowRadius;

    float wanderInterval;
    float wanderTimer;
    Vector3 lastWanderDirection;
    Vector3 lastDisplacement;
    float wanderDegreesDelta;


    // METHODS

    // Seek.
    Vector3 KinematicSeek(Vector3 targetPosition)
    {
        Vector3 desiredVelocity = targetPosition - gameObject.transform.position;
        return desiredVelocity.normalized * maxVelocity;
    }
    Vector3 Seek(Vector3 targetPosition)
    {
        return KinematicSeek(targetPosition) - currentVelocity;
    }

    // Flee
    Vector3 KinematicFlee(Vector3 targetPosition)
    {
        return -1 * KinematicSeek(targetPosition); // Invertion of the seek behaviour.
    }
    Vector3 Flee(Vector3 targetPosition)
    {
        return KinematicFlee(targetPosition) - currentVelocity;
    }

    // Arrive
    Vector3 KinematicArrive(Vector3 targetPosition)
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
    Vector3 Arrive(Vector3 targetPosition)
    {
        return KinematicArrive(targetPosition) - currentVelocity;
    }

    // Pursue
    Vector3 KinematicPursue(Vector3 targetPosition, Vector3 targetVelocity)
    {
        float distance = Vector3.Distance(targetPosition, gameObject.transform.position);
        float ahead = distance / 10; // should the 10 be a parameter?
        Vector3 futurePosition = targetPosition + targetVelocity * ahead;

        return KinematicSeek(futurePosition);
    }
    Vector3 Pursue(Vector3 targetPosition, Vector3 targetVelocity)
    {
        return KinematicPursue(targetPosition, targetVelocity) - currentVelocity;
    }

    // Evade
    Vector3 KinematicEvade(Vector3 targetPosition, Vector3 targetVelocity)
    {
        float distance = Vector3.Distance(targetPosition, gameObject.transform.position);
        float ahead = distance / 10; // should the 10 be a parameter?
        Vector3 futurePosition = targetPosition + targetVelocity * ahead;

        return KinematicFlee(futurePosition);
    }
    Vector3 Evade(Vector3 targetPosition, Vector3 targetVelocity)
    {
        return KinematicEvade(targetPosition, targetVelocity) - currentVelocity;
    }

    // Wander
    Vector3 KinematicWander()
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
    Vector3 Wander()
    {
        return KinematicWander() - currentVelocity;
    }
}
