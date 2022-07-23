using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    private const string TitleScreenSceneName = "01_TitleScreen";
    private const string MainGameSceneName = "02_MainGame";
    private const string GameOverSceneName = "03_GameOver";
    private const string OptionsSceneName = "04_Options";
    private const string CreditsSceneName = "05_Credits";
    private const string GameWinSceneName = "06_GameWin";
    private bool isGameStarted = false;
    private string currentMenu;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this.gameObject);
        } else
        {
            gameManager = this;
            DontDestroyOnLoad(gameManager);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TODO replace this test call with gameover logic
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameOver();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameWin();
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        SceneManager.LoadScene(MainGameSceneName);
    }

    public void EndGame()
    {
        isGameStarted = false;
        SceneManager.LoadScene(TitleScreenSceneName);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    private void GameOver()
    {
        isGameStarted = false;
        SceneManager.LoadScene(GameOverSceneName);
    }
    private void GameWin()
    {
        isGameStarted = false;
        SceneManager.LoadScene(GameWinSceneName);
    }


    public void OpenOptions()
    {
        currentMenu = OptionsSceneName;
        SceneManager.LoadScene(OptionsSceneName, LoadSceneMode.Additive);
    }

    public void OpenCredits()
    {
        currentMenu = CreditsSceneName;
        SceneManager.LoadScene(CreditsSceneName, LoadSceneMode.Additive);
    }

    public void CloseMenu()
    {
        if (currentMenu == null)
        {
            return;
        }
        SceneManager.UnloadSceneAsync(currentMenu);
    }
}
