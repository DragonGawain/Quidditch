using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using groupAI;

public class ScoreManager : MonoBehaviour
{
    private Dictionary<TeamManager, int> teamScores = new Dictionary<TeamManager, int>();

    // Get both teams score text from the HUD
    [SerializeField]
    TextMeshProUGUI team1Score;

    [SerializeField]
    TextMeshProUGUI team2Score;

    private TextMeshProUGUI finalResult;

    private TeamManager team1, team2;

    public static ScoreManager singleton = null;

    private void Start()
    {
        if (singleton == null)
            singleton = this;

        if (singleton != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        // Initialize scores for each team
        var teams = FindObjectsOfType<TeamManager>();

        Debug.Log("Score manager found" + teams.Length + " teams");

        if (teams.Length != 2)
        {
            Debug.Log("Invalid number of teams");
        }
        else
        {
            teamScores.Add(teams[0], 0);
            teamScores.Add(teams[1], 0);
        }
        
    }

    public void IncrementScore(TeamManager team)
    {
        // Increment the score for the specified team
        teamScores[team]++;
        Debug.Log("Goal scored by " + team.ToString());
        Debug.Log(
            "New score: Team 1 : "
                + teamScores[team1]
                + " vs Team 2 : "
                + teamScores[team2]
        );

        
    }

    public void SnitchCaught(TeamManager team)
    {
         finalResult = GameObject.Find("Game Result Text (TMP)").GetComponent<TextMeshProUGUI>();

        if (team == team2)
            finalResult.SetText("Team 2 caught the snitch and WON!");
        else if (team == team1)
            finalResult.SetText("Team 1 caught the snitch and WON!");
    }

    private void Update()
    {       
        // Update the score on the UI
 
        team1Score.text = teamScores[team1].ToString();
        team2Score.text = teamScores[team2].ToString();
    }
}
