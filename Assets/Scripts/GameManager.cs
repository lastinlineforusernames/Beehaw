using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    private const string MainGameSceneName = "02_MainGame";
    private const string GameOverSceneName = "03_GameOver";
    private const string TitleScreenSceneName = "01_TitleScreen";
    
    private bool isGameStarted = false;
    
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
}
