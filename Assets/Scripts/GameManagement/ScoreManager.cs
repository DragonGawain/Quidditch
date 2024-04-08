using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private Dictionary<Team, int> teamScores = new Dictionary<Team, int>();

    private void Start()
    {
        // Initialize scores for each team
        teamScores[Team.PLAYER] = 0;
        teamScores[Team.AI] = 0;
    }

    public void IncrementScore(Team team)
    {
        // Increment the score for the specified team
        teamScores[team]++;
        Debug.Log("Goal scored by " + team.ToString());
        Debug.Log("New score: Team 1 (Player): " + teamScores[Team.PLAYER] + " vs Team 2 (AI): " + teamScores[Team.AI]);
    }
}
