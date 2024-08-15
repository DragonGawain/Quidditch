using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Snitch : Ball
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
    private string[] tags = { "seeker" };

    //private TextMeshProUGUI finalResult;
    private ScoreManager scoreManager;

    // EVENTS
    public event GameManager.SnitchCaught OnSnitchCaught;

    // METHODS.
    // Start is called before the first frame update
    void Start() {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    protected override void OnAwake()
    {
        base.OnAwake();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     // Get the closest seeker to flee away from them
    //     target = GetClosestTarget("seeker");

    //     Vector3 desiredVelocity = myNPCMovement.Flee(target.position, speed);
    //     //Vector3 desiredVelocity = myNPCMovement.KinematicFlee(target.position, speed);

    //     transform.position += desiredVelocity * Time.deltaTime;
    // }

    void FixedUpdate()
    {
        // Get the closest seeker to flee away from them
        target = GetClosestTarget(tags);

        Vector3 desiredVelocity = Vector3.ClampMagnitude(
            myNPCMovement.Flee(target.position, acceleration) + myRigidbody.velocity,
            maxSpeed
        );
        myRigidbody.velocity = desiredVelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(string.Format("The Snitch collided with {0}.", collision.gameObject));

        GroupAI theSeeker = collision.gameObject.GetComponent<GroupAI>();
        if (collision.gameObject.CompareTag("seeker") && theSeeker != null)
        {            
            
            // TODO: handle player seeker, and make sure everything is fine with the AI one.

            Debug.Log(string.Format("The Snitch was caught by {0}.", theSeeker.gameObject));
        

            // Throw the event.
            EndGame();
            scoreManager.SnitchCaught(theSeeker.Team);
        //    finalResult = GameObject.Find("Game Result Text (TMP)").GetComponent<TextMeshProUGUI>();
                
        //    if (theSeeker.Team == team2)
        //        finalResult.SetText("Team 2 (AI) caught the snitch and WON!");
         //   else if (theSeeker.GetTeam() == Team.PLAYER)
        //        finalResult.SetText("Team 1 (PLAYER) caught the snitch and WON!");
            
            // OnSnitchCaught?.Invoke(theSeeker);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            // verify existence of component
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                if (
                    collision.gameObject.GetComponent<PlayerController>().GetPlayerRole()
                    == PlayerRole.SEEKER
                )
                {
                    Debug.Log(string.Format("The Snitch was caught by the player."));

                    // TODO: handle player seeker, and make sure everything is fine with the AI one.

                    // Throw the event.
                    EndGame();
                    // OnSnitchCaught?.Invoke(theSeeker);
                }
            }
        }
    }

    private void EndGame()
    {
        UIManager uim = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        uim.EndGame();
    }
}
