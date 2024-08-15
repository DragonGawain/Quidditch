using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Goalpost : MonoBehaviour
{
    // TODO: add colliders for the various scoring zones, handle collision detection.
    // TODO: add waypoints that the keeper and chasers will use.
    // TODO: add the event proper.

    // VARIABLES
    private TeamManager owner = null;
    public TeamManager Owner { get { return owner; } set { owner = value; } }

    private ScoreManager scoreManager;



    // EVENTS
    public event GameManager.GoalScored OnGoalScored;



    // METHODS
    private void Start()
    {
        // Find the ScoreManager object in the scene
        scoreManager = FindObjectOfType<ScoreManager>();
    }


    // Increase the score whenever the quaffle enters the trigger zone of the goalpost
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("quaffle"))
        {
            Quaffle q = other.GetComponent<Quaffle>();
            GroupAI theChaser = q.LastOwner.GetComponent<GroupAI>();

            // Ensure that a goal is registered only when the goalpost is triggered by the enemy team!
            if (owner != theChaser.Team)
            {
                // Update score for the corresponding team using the ScoreManager
                scoreManager.IncrementScore(theChaser.Team);

                // After a goal, reposition the quaffle at the center of the "pitch" and ensure there is no velocity acting on it!
                q.ResetQuaffle();

                OnGoalScored?.Invoke(owner, theChaser);
            }
        }
    }
}
