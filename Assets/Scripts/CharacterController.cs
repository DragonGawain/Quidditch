using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Inputs inputs;
    Vector3 rotationInputs = new Vector3(0,0,0);
    float movementInput = 0;
    Rigidbody body;


    // Start is called before the first frame update
    void Awake()
    {
        inputs = new Inputs();
        inputs.Player.Enable();
        body = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called 50 times per second
    void FixedUpdate()
    {
        rotationInputs = inputs.Player.Rotate.ReadValue<Vector3>();
        if (rotationInputs.x > 0)
            transform.Rotate(1,0,0);
        else if (rotationInputs.x < 0)
            transform.Rotate(-1,0,0);
        
        if (rotationInputs.y > 0)
            transform.Rotate(0,1,0);
        else if (rotationInputs.y < 0)
            transform.Rotate(0,-1,0);

        if (rotationInputs.z > 0)
            transform.Rotate(0,0,1);
        else if (rotationInputs.z < 0)
            transform.Rotate(0,0,-1);

        movementInput = inputs.Player.Move.ReadValue<float>();
        if (movementInput > 0)
        {
            body.velocity += new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            body.velocity = Vector3.ClampMagnitude(body.velocity, 10);
        }
        else if(movementInput < 0)
        {
            Vector3 deccel = new Vector3(body.velocity.x, body.velocity.y, body.velocity.z);
            deccel.Normalize();
            deccel = deccel/2;
            body.velocity -= deccel;
        }
    }
}
