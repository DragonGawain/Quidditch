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

        SceneManager.LoadScene("MainGame");
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
        SceneManager.LoadScene("MainGame");
        mainMenu.SetActive(false);
        HUD.SetActive(false);
        pauseMenu.SetActive(false);
        endGame.SetActive(true);
    }
}
