using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;



public class PlayerController : MonoBehaviour
{
    PlayerMovementController moveController;


    private float maxSpeed = 10f;
    public float MaxSpeed { get {return maxSpeed;} set { maxSpeed = value; } }

    private TeamManager team;//which team the player is on- who to notify if the player switches positions
    public TeamManager Team { set { team = value;} }
    // Start is called before the first frame update
    void Start()
    {
        moveController = gameObject.AddComponent<PlayerMovementController>();
        
        /*
        public void StartGame()
        {
            // Temporary for timer testing
            //SceneManager.LoadScene("CristianScene");

            switch (playerRole)
            {
                case PlayerRole.CHASER:
                    //PlayerController.playerMaxSpeed = 3f;
                    SceneManager.LoadScene("MainGameChaser");
                    break;
                case PlayerRole.BEATER:
                    //PlayerController.playerMaxSpeed = 5f;
                    SceneManager.LoadScene("MainGameBeater");
                    break;
                case PlayerRole.SEEKER:
                    //PlayerController.playerMaxSpeed = 2.25f;
                    SceneManager.LoadScene("MainGameSeeker");
                    break;
                case PlayerRole.KEEPER:
                    //PlayerController.playerMaxSpeed = 5f;
                    SceneManager.LoadScene("MainGameKeeper");
                    break;
                default:
                    //PlayerController.playerMaxSpeed = 3f;
                    SceneManager.LoadScene("MainGameChaser");
                    break;
            }
            mainMenu.SetActive(false);
            HUD.SetActive(true);
            Debug.Log("start game");
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO why is this here
    public PlayerRole GetPlayerRole()
    {
        //return playerRole;
        return 0;
    }
}
