using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputActions playerControls;
    private InputAction forward,yaw;
    public float moveSpeed = 10f, pivotSpeed = 2f, verticalSpeed = 5f ;
    public float throttleIncrement = 0.1f, maxThrust = 200f, maxBackwardsThrust = 10f, upwardsOffset = 8.00f, upwardsForce = Mathf.Abs(Physics.gravity.y);
    [Tooltip("")]
    public float responsiveness = 10f;

    private Rigidbody rb;
    private float forwardMove = 0, pivot =0;
    private float _throttle, _roll, _yaw, _pitch,_upwardsForce; //a broom is somewhat like a helicopter meets a plane
    private float MAX_OFF = 11.0f, MIN_OFF = 8.7f;
    private float liftTimer = 0;
    
    //Inputs inputs;
    //Vector3 rotationInputs = new Vector3(0, 0, 0);
    //float movementInput = 0;
    //Rigidbody body;



    void Awake()
    {
        playerControls = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();
    //    inputs = new Inputs();
    //    inputs.Player.Enable();
    //    body = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {//Required by Input system, can have issues if not included
        Debug.Log("PrintOnEnable: script was enabled");

        forward = playerControls.Player.Forward;
        yaw = playerControls.Player.Yaw;
        forward.Enable();
        yaw.Enable();
    }
    void Update()
    {
        forwardMove = forward.ReadValue<float>();
        pivot = yaw.ReadValue<float>();
        
    }


    private void OnDisable()
    {//Required by Input system, can have issues if not included
        forward.Disable();
        yaw.Disable();
    }

    private void throttler()
    {//throttle speed up or down
        //forward speed

        if (forwardMove< 0)// Like cats, brooms dont like to move backwards
        {
            _throttle +=  throttleIncrement/2;
            rb.AddForce(transform.forward * forwardMove * maxBackwardsThrust * _throttle);//needs to be * forwardMove to remain in a backwards direction
        }
        else if (forwardMove > 0)
        {
            _throttle += forwardMove * throttleIncrement;
            rb.AddForce(transform.forward * forwardMove * maxThrust * _throttle);
 
        }       
    }
    private void rudder()
    {//controls the nose movement (yaw)
        _yaw = pivot * responseModifier(pivotSpeed);
        rb.AddTorque(transform.up * _yaw);
    }

    private void yoke()
    {
        //_pitch 
        rb.AddTorque(transform.forward * _yaw);
    }

    private void lift()
    {//like a helicopter, the brooms have upward lift(while steering more like a plane)
        //make it bob a little
        if (liftTimer == 0)
        {
            liftTimer = Random.Range(1.0f, 2.0f);
            upwardsOffset = Random.Range(MIN_OFF, MAX_OFF);
        }
        else { 
            liftTimer -= Time.deltaTime;
            if(liftTimer < 0)
                liftTimer = 0;
        }
        rb.AddForce(transform.up * (upwardsForce+upwardsOffset));

    }

    private void FixedUpdate()
    {//Physics need to be done in FixedUpdate()
     //Vector3 Velocity = new Vector3(forwardMove * moveSpeed, 0,pivot* pivotSpeed);
     //rb.velocity = Velocity;
        throttler();
        rudder();
        lift();
    }

    //factor in mass to rotation (pitch,yaw,roll)
    private float responseModifier(float responsiveness)
    {
           return (rb.mass / 10f) * responsiveness;

    }
        //// FixedUpdate is called 50 times per second
        //void FixedUpdate()
        //{
        //    rotationInputs = inputs.Player.Rotate.ReadValue<Vector3>();
        //    if (rotationInputs.x > 0)
        //        transform.Rotate(1, 0, 0);
        //    else if (rotationInputs.x < 0)
        //        transform.Rotate(-1, 0, 0);

        //    if (rotationInputs.y > 0)
        //        transform.Rotate(0, 1, 0);
        //    else if (rotationInputs.y < 0)
        //        transform.Rotate(0, -1, 0);

        //    if (rotationInputs.z > 0)
        //        transform.Rotate(0, 0, 1);
        //    else if (rotationInputs.z < 0)
        //        transform.Rotate(0, 0, -1);

        //    movementInput = inputs.Player.Move.ReadValue<float>();
        //    if (movementInput > 0)
        //    {
        //        // body.velocity += new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        //        Vector3 movementVector = transform.GetChild(0).position - transform.position;
        //        movementVector.Normalize();
        //        movementVector = movementVector / 2;
        //        body.velocity += movementVector;
        //        body.velocity = Vector3.ClampMagnitude(body.velocity, 2);
        //    }
        //    else if (movementInput < 0)
        //    {
        //        Vector3 deccel = new Vector3(body.velocity.x, body.velocity.y, body.velocity.z);
        //        deccel.Normalize();
        //        deccel = deccel / 2;
        //        body.velocity -= deccel;
        //    }
        //}
    }
