
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

//Reference : https://www.youtube.com/watch?v=2XEiHf1N_EY
// https://www.youtube.com/watch?v=Coch-PkHY54
public class MouseLookRotate : MonoBehaviour
{
    [Tooltip("Maximum speed in degrees/second. To flip an axis, multiply its value by -1")]
    [SerializeField] private Vector2 sensitivity = new Vector2(200,-200);//y is negative to not have inverted control
    [Tooltip("Rotation acceleration in degrees/second")]
    [SerializeField] private Vector2 acceleration = new Vector2(200, 200);
    [Tooltip("The maximum angle the player can rotate to look upwards")]
    [SerializeField] private float maxY = 60;
    [Tooltip("The maximum angle the player can rotate to look downwards")]
    [SerializeField] private float minY = -60;
    public float MAX_Y = 60;//maximum Y value (cant go directly up)

    //current rotation in degrees
    private Vector2 rotation;
    //current velocity in degrees
    private Vector2 velocity;


   private float clampRotation(float angle, float min, float max)
    {
        return Mathf.Clamp(angle, min, max);
    }

    private Vector2 getInput() {
        //useing the mouse position x and y 
        //Vector3 mouseInput = Mouse.current.position.ReadValue();
        //return new Vector2(mouseInput.x, mouseInput.y);
        return new Vector2( Mouse.current.delta.x.ReadValue(),
                            Mouse.current.delta.y.ReadValue());
    }

    private void Update()
    {

        Vector2 newVelocity = getInput() * sensitivity;
        velocity = new Vector2(
            Mathf.MoveTowards(velocity.x, newVelocity.x, acceleration.x * Time.deltaTime),
            Mathf.MoveTowards(velocity.y, newVelocity.y, acceleration.y * Time.deltaTime)
            );
        rotation += velocity * Time.deltaTime;
        rotation.y = clampRotation(rotation.y, minY,maxY);
        
        //convert to euler angles 
        // vertical rotation (pitch) is around the x axis, and yaw is around y axis
        transform.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);
    }

    //private void getViewportCenter()
    //{
    //    //TODO: This can be optimised to be on move or on resize instead of in update
    //    //just here for testing
    //    //return the center pixel of the viewport in screenspace
    //    viewportCenter = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
    //}

    //void getMousePosition()
    //{
    //    //get pixel coordinates on screen
    //    mouseScreenPos = Mouse.current.position.ReadValue();
    //    //need z value for it to register for the camera
    //    mouseScreenPos.z = Camera.main.nearClipPlane + 1;
    //    //translate to position in scene
    //    Vector3 newMousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
    //    deltaMouse = mousePos - newMousePos;
    //    mousePos = newMousePos;
    //    mouseViewportSpace = Camera.main.ScreenToViewportPoint(mouseScreenPos);

    //}

    //void lookAtMouse()
    //{
    //    Quaternion rotation = Quaternion.LookRotation(mouseViewportSpace - transform.position);

    //    float time = 0;
    //    while (time < deltaTime)
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, time);
    //        time += Time.deltaTime * speed;
    //        //yield return null;
    //        yield return wait;
    //    }
    //}

}
