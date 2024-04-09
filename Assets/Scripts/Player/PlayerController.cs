using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerRole
{
    CHASER,
    BEATER,
    KEEPER,
    SEEKER
}

public class PlayerController : MonoBehaviour
{
    Inputs inputs;
    Vector3 rotationInputs = new Vector3(0, 0, 0);
    float movementInput = 0;
    Rigidbody body;

    [SerializeField]
    GameObject camObject;
    Vector2 currentMousePos = new Vector2(0, 0);

    [SerializeField, Range(0.1f, 5f)]
    float sensitivity = 1;
    Vector2 offset = new Vector2(0, 0);

    //TODO:: I'm making the playerRole a SerializeField for now to make debugging easier, but it should be set via a UI option at the start
    [SerializeField] PlayerRole playerRole; 

    // Start is called before the first frame update
    void Awake()
    {
        inputs = new Inputs();
        inputs.Player.Enable();
        body = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentMousePos = inputs.Player.MousePosition.ReadValue<Vector2>();
    }

    // FixedUpdate is called 50 times per second
    void FixedUpdate()
    {
        rotationInputs = inputs.Player.Rotate.ReadValue<Vector3>();
        if (rotationInputs.x > 0)
            transform.GetChild(0).Rotate(1, 0, 0);
        else if (rotationInputs.x < 0)
            transform.GetChild(0).Rotate(-1, 0, 0);

        if (rotationInputs.y > 0)
            transform.Rotate(0, -1, 0);
        else if (rotationInputs.y < 0)
            transform.Rotate(0, 1, 0);

        if (rotationInputs.z > 0)
            transform.Rotate(0, 0, -1);
        else if (rotationInputs.z < 0)
            transform.Rotate(0, 0, 1);

        movementInput = inputs.Player.Move.ReadValue<float>();
        if (movementInput > 0)
        {
            // body.velocity += new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            Vector3 movementVector =
                transform.GetChild(0).GetChild(0).position - transform.GetChild(0).position;
            movementVector.Normalize();
            movementVector /= 2;
            body.velocity += movementVector;
            body.velocity = Vector3.ClampMagnitude(body.velocity, 2);
        }
        else if (movementInput < 0)
        {
            Vector3 deccel = new Vector3(body.velocity.x, body.velocity.y, body.velocity.z);
            deccel.Normalize();
            deccel /= 2;
            body.velocity -= deccel;
        }

        // lastMousePos = currentMousePos;
        currentMousePos = inputs.Player.MousePosition.ReadValue<Vector2>();

        // Camera orientation mapping
        // -X : PITCH UP
        // +X : PITCH DOWN
        // -Y : YAW LEFT
        // +Y : YAW RIGHT
        // -Z : ROLL LEFT
        // +Z : ROLL RIGHT

        // mouse moved left - yaw left - decrease Y axis
        if (currentMousePos.x < 0 && offset.x > -21)
        {
            // camObject.transform.Rotate(new Vector3(0.0f,-1.0f * sensitivity,0.0f));
            Quaternion currentRot = camObject.transform.localRotation;
            currentRot.eulerAngles += new Vector3(0.0f, -1.0f * sensitivity, 0.0f);
            camObject.transform.localRotation = currentRot;
            offset.x -= 1.0f * sensitivity;
        }
        // mouse moved right - yaw right - increase Y axis
        else if (currentMousePos.x > 0 && offset.x < 21)
        {
            // camObject.transform.Rotate(new Vector3(0.0f,1.0f * sensitivity,0.0f));
            Quaternion currentRot = camObject.transform.localRotation;
            currentRot.eulerAngles += new Vector3(0.0f, 1.0f * sensitivity, 0.0f);
            camObject.transform.localRotation = currentRot;
            offset.x += 1.0f * sensitivity;
        }

        // mouse moved up - pitch up - decrease X axis
        if (currentMousePos.y > 0 && offset.y > -21)
        {
            // camObject.transform.Rotate(new Vector3(-1.0f * sensitivity,0.0f,0.0f));
            Quaternion currentRot = camObject.transform.localRotation;
            currentRot.eulerAngles += new Vector3(-1.0f * sensitivity, 0.0f, 0.0f);
            camObject.transform.localRotation = currentRot;
            offset.y -= 1.0f * sensitivity;
        }
        // mouse moved down - pitch down - increase X axis
        else if (currentMousePos.y < 0 && offset.y < 21)
        {
            // camObject.transform.Rotate(new Vector3(1.0f * sensitivity,0.0f,0.0f));
            Quaternion currentRot = camObject.transform.localRotation;
            currentRot.eulerAngles += new Vector3(1.0f * sensitivity, 0.0f, 0.0f);
            camObject.transform.localRotation = currentRot;
            offset.y += 1.0f * sensitivity;
        }
    }

    public PlayerRole GetPlayerRole()
    {
        return playerRole;
    }
}
