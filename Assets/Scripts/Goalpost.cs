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
    [SerializeField] private Team owningTeam = Team.NONE;
    public Team OwningTeam { get { return owningTeam; } }

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
            GroupAI theChaser = q.LastHolder.GetComponent<GroupAI>();

            // Ensure that a goal is registered only when the goalpost is triggered by the enemy team!
            if (owningTeam != theChaser.GetTeam())
            {
                // Update score for the corresponding team using the ScoreManager
                scoreManager.IncrementScore(theChaser.GetTeam());

                OnGoalScored?.Invoke(owningTeam, theChaser);
            }
        }
    }
}
