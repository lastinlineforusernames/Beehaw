using UnityEngine;
using UnityEngine.SceneManagement;

namespace Beehaw.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager gameManager;
        public const int PlayerLayerMask = 8;
        public const int EnemyLayerMask = 6;
        private const string TitleScreenSceneName = "01_TitleScreen";
        private const string MainGameSceneName = "02_MainGame";
        private const string GameOverSceneName = "03_GameOver";
        private const string OptionsSceneName = "04_Options";
        private const string CreditsSceneName = "05_Credits";
        private const string GameWinSceneName = "06_GameWin";
        private bool isGameStarted = false;
        private string currentMenu;
        private AudioManager audioManager;
        

        private void Awake()
        {
            if (gameManager != null && gameManager != this)
            {
                Destroy(gameObject);
            }
            else
            {
                gameManager = this;
                DontDestroyOnLoad(gameManager);
            }
        }

        private void Start()
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            audioManager.playTitleBGM();
        }

        public void StartGame()
        {
            isGameStarted = true;
            SceneManager.LoadScene(MainGameSceneName);
            Physics2D.IgnoreLayerCollision(PlayerLayerMask, EnemyLayerMask, false);
            audioManager.playGameBGM();
        }

        public void EndGame()
        {
            isGameStarted = false;
            SceneManager.LoadScene(TitleScreenSceneName);
            audioManager.playTitleBGM();
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        public void GameOver()
        {
            isGameStarted = false;
            SceneManager.LoadScene(GameOverSceneName);
            audioManager.playGameOverBGM();
        }
        public void GameWin()
        {
            isGameStarted = false;
            SceneManager.LoadScene(GameWinSceneName);
            audioManager.playVictoryBGM();
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
}