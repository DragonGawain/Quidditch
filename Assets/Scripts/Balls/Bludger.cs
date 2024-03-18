using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bludger : Ball
{
    // VARIABLES
    // Should speed and the like be defined here, or by the parent classe?
    [SerializeField]
    Transform target;

    [SerializeField, Range(0, 15)]
    float maxSpeed = 4f;

    [SerializeField, Range(0, 10)]
    float acceleration = 1f;

    // Tags corresponding to the possible targets for the snitch
    private string[] tags = { "seeker", "keeper", "beater", "chaser" };


    // METHODS.
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    protected override void OnAwake()
    {
        base.OnAwake();
    }

    void FixedUpdate()
    {
        // Get the closest seeker to flee away from them
        target = GetClosestTarget(tags);

        Vector3 desiredVelocity = Vector3.ClampMagnitude(
            myNPCMovement.Seek(target.position, acceleration) + rb.velocity,
            maxSpeed
        );
        rb.velocity = desiredVelocity;
    }
}
