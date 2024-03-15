using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Timer : MonoBehaviour
{
    public float timeLeft;
    public bool timerOn = false;

    private TextMeshProUGUI timerTxt;
    private TextMeshProUGUI finalResult;

    private UIManager uiManager;


    void Awake()
    {
        // Get the timer text from the HUD
        timerTxt = GameObject.Find("Time Left Text (TMP)").GetComponent<TextMeshProUGUI>();

        // Get the UIManager from the UI obj.
        uiManager = GameObject.Find("UI").GetComponent<UIManager>();
    }

    void Start()
    {
        timerOn = true;
    }

    void Update()
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                updateTimer(timeLeft);
            }
            else
            {
                timeLeft = 0;
                timerOn = false;

                // Enable the End Game Panel and output the game result
                uiManager.EndGame();
                OutputGameResult();
            }
        }
    }

    // Update the timer text by showing the minutes and seconds left
    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    // Output the game result based on each team's score
    void OutputGameResult()
    {
        finalResult = GameObject.Find("Game Result Text (TMP)").GetComponent<TextMeshProUGUI>();

        float score1 = float.Parse(GameObject.Find("Team 1 Score Text (TMP)").GetComponent<TextMeshProUGUI>().text);
        float score2 = float.Parse(GameObject.Find("Team 2 Score Text (TMP)").GetComponent<TextMeshProUGUI>().text);

        Debug.Log(score1);
        Debug.Log(score2);

        if (score1 > score2)
            finalResult.SetText("Team 1 WON!");
        else if (score1 < score2)
            finalResult.SetText("Team 2 WON!");
        else
            finalResult.SetText("DRAW");
    }
}
