using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerControllerKelly : MonoBehaviour
{
    //a broom is somewhat like a helicopter meets a plane
    public PlayerInputActions playerControls;
    private InputAction forward,yaw,pitch,roll;
    public float moveSpeed = 10f, pivotSpeed = 2f, verticalSpeed = 5f ;
    public float throttleIncrement = 0.1f, maxThrust = 200f, maxBackwardsThrust = 10f, upwardsOffset = 1.50f, upwardsForce = Mathf.Abs(Physics.gravity.y);//TODO: UPWARDS FORCE STILL APPEARS NEGATIVE??
    public float deltaZ = 75.0f;
    [Tooltip("")]
    public float responsiveness = 10f;
    [Tooltip("Control speed at which the player rotates left or right ")]
    public float yawResponsiveness=0.5f;
    [Tooltip("Control speed at which the player rotates up or down ")]
    public float pitchResponsiveness = 0.5f;

    private Rigidbody rb;
    private float _throttle,_forward, _roll, _yaw, _pitch; 
    private float MAX_OFF = 10.5f, MIN_OFF = 9.0f;
    private float liftTimer = 0;
    public Vector3 mouseScreenPos = Vector3.zero;
    public Vector3 mouseViewportSpace = Vector3.zero;
    public Vector3 mousePos= Vector3.zero;
    public Vector3 deltaMouse = Vector3.zero;
    public Vector3 viewportCenter = Vector3.zero;


    //TESTING

    public float eulerX, eulerY, eulerZ;

    Vector3 Velocity = Vector3.zero;

    
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
        pitch = playerControls.Player.Pitch;
        roll = playerControls.Player.Roll; 
        forward.Enable();
        yaw.Enable();
        pitch.Enable();
        roll.Enable();
        
    }
    void Update()
    {

        _forward = forward.ReadValue<float>();
        //_yaw = yaw.ReadValue<float>();
      
        _pitch =  pitch.ReadValue<float>();
        _roll = roll.ReadValue<float>();
          
    }
    void CalculateState()
    {
        Velocity = rb.velocity;

    }

    private void OnDisable()
    {//Required by Input system, can have issues if not included
        forward.Disable();
        yaw.Disable();
        pitch.Disable();
        roll.Disable();

        //lookAtMouse.Deactivate();
    }

    private void throttler()
    {//throttle speed up or down
        //forward speed
        float force =0;
        if (_forward< 0)// Like cats, brooms dont like to move backwards
        {
            _throttle +=  throttleIncrement/2;
            force = _forward * maxBackwardsThrust * _throttle;
            rb.AddForce(transform.forward *force);
            // rb.AddForce(transform.forward * forwardMove * maxBackwardsThrust * _throttle);//needs to be * forwardMove to remain in a backwards direction
        }
        else if (_forward > 0)
        {
            _throttle += throttleIncrement;
            force = _forward * maxThrust * _throttle;
            rb.AddForce(transform.forward * force);
            //rb.AddForce(transform.forward * forwardMove * maxThrust * _throttle);

        }

        //equalize(force/4);
    }

    private void equalize(float _force)
    {//this does not work very well
        float eZ = eulerZ-180;
        if (eulerZ < deltaZ || eulerZ > 360-deltaZ)
        {

        }
        else
        {
            Debug.Log("euler");
            Roll(Mathf.Sign(eZ),_force);
        }
        //rb.AddRelativeTorque(0, 0, eZ);
        //if (eulerZ > 180) eulerZ -= 360;
        //if (Mathf.Abs(eulerZ) > 60)
        //{
        //    eulerZ = 0;
        //}
        //if (eulerZ > 30 || eulerZ< -30)
        //{
        //    Ailerons(Mathf.Sign(eulerZ), _force);
        //}
        //rb.AddRelativeTorque(0,0,eulerZ);
        //var stableVector = new Vector3(-Mathf.Clamp(0.0f, -stabilizer, stabilizer), 0, -Mathf.Clamp(0, -stabilizer, stabilizer));
        //var stable = transform.TransformDirection(stableVector);
        ////print(Array(Vector3(xRot, 0, zRot), stableVector, rigidbody.angularVelocity));
        //rb.angularVelocity += stableVector;
        //rb.angularVelocity = rb.angularVelocity / 1.1f;
    }
    private void Yaw()
    {//controls the nose movement //like a rudder

        float torque = _yaw * responseModifier(pivotSpeed);
        rb.AddRelativeTorque(transform.up * torque);
    }

    private void Roll(float _r)
    {//The ailerons(flaps) of a plane contol the Roll
        //_roll = roll * responseModifier(rollSpeed);
        float torque = _r * responseModifier(pivotSpeed / 2);
        rb.AddTorque(transform.forward * torque);
        //rb.AddTorque(transform.forward * _pitch);//this worked for roll
    }
    private void Roll(float _r, float force)
    {//The ailerons(flaps) of a plane contol the Roll
        //_roll = roll * responseModifier(rollSpeed);
        float torque = _r * responseModifier(force / 2);
        rb.AddTorque(transform.forward * torque);
        //rb.AddTorque(transform.forward * _pitch);//this worked for roll
    }
    private void Roll()
    {//The ailerons(flaps) of a plane contol the Roll
        //_roll = roll * responseModifier(rollSpeed);
        float torque = _roll * responseModifier(pivotSpeed / 2);
        rb.AddTorque(transform.forward * torque);
        //rb.AddTorque(transform.forward * _pitch);//this worked for roll
    }
    private void Pitch()
    {//Controls the pitch // Elevator
        float torque = _pitch * responseModifier(pivotSpeed);
        rb.AddTorque(transform.right * torque);
    }

    private void Lift()
    {//like a helicopter, the brooms have upward lift(while steering more like a plane)
        //make it bob a little
        if (liftTimer == 0)
        {
            liftTimer = Random.Range(0.5f, 1.0f);
            upwardsOffset = Random.Range(MIN_OFF, MAX_OFF); //gravity seems to win if you use an even range
            //upwardsOffset = Random.Range(upwardsForce - UP_OFF, upwardsForce + UP_OFF);
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
        Yaw();
        Lift();
        Pitch();
        Roll();
        //testing
        eulerX = transform.eulerAngles.x;
        eulerY = transform.eulerAngles.y;
        eulerZ = transform.eulerAngles.z;
    }

    
    private float responseModifier(float responsiveness)
    {
        //factor in mass to rotation (pitch,yaw,roll)
           return (rb.mass / 10f) * responsiveness;

    }
     
 }
