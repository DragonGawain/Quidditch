using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager singleton = null;

    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject HUD;

    [SerializeField]
    GameObject endGame;

    public static PlayerRole playerRole;

    // Start is called before the first frame update
    void Awake()
    {
        if (singleton == null)
            singleton = this;

        if (singleton != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    public void StartGame()
    {
        // Temporary for timer testing
        //SceneManager.LoadScene("CristianScene");
        Time.timeScale = 1;
        switch (playerRole)
        {
            case PlayerRole.CHASER:
                PlayerController.playerMaxSpeed = 3f;
                SceneManager.LoadScene("MainGameChaser");
                break;
            case PlayerRole.BEATER:
                PlayerController.playerMaxSpeed = 5f;
                SceneManager.LoadScene("MainGameBeater");
                break;
            case PlayerRole.SEEKER:
                PlayerController.playerMaxSpeed = 2.25f;
                SceneManager.LoadScene("MainGameSeeker");
                break;
            case PlayerRole.KEEPER:
                PlayerController.playerMaxSpeed = 5f;
                SceneManager.LoadScene("MainGameKeeper");
                break;
            default:
                PlayerController.playerMaxSpeed = 3f;
                SceneManager.LoadScene("MainGameChaser");
                break;
        }
        mainMenu.SetActive(false);
        HUD.SetActive(true);
        Debug.Log("start game");
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        HUD.SetActive(false);
        Time.timeScale = 0;
        Debug.Log("pause game");
    }

    public void UnpauseGame()
    {
        pauseMenu.SetActive(false);
        HUD.SetActive(true);
        Time.timeScale = 1;
        Debug.Log("unpause game");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        mainMenu.SetActive(true);
        HUD.SetActive(false);
        pauseMenu.SetActive(false);
        endGame.SetActive(false);
        Debug.Log("return to main menu");
    }

    // Enable the End Game Panel
    public void EndGame()
    {
        /*        SceneManager.LoadScene("EndGame");
                mainMenu.SetActive(false);
                HUD.SetActive(false);
                pauseMenu.SetActive(false);*/
        endGame.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true
        Time.timeScale = 0;;
    }

    public void ChoosePlayerRole(TMP_Dropdown playerRoleInput)
    {
        int pr = playerRoleInput.value;
        switch (pr)
        {
            case 0:
                playerRole = PlayerRole.CHASER;
                break;
            case 1:
                playerRole = PlayerRole.BEATER;
                break;
            case 2:
                playerRole = PlayerRole.SEEKER;
                break;
            case 3:
                playerRole = PlayerRole.KEEPER;
                break;
            default:
                break;
        }
    }
}
