using UnityEngine;

namespace Beehaw.Managers
{
    public class PauseMenu : MonoBehaviour
    {
        private bool isGamePaused = false;
        [SerializeField]
        private GameObject pauseMenu;
        // TODO creat PlayerController class to collect all player input and pass to movement and ability classes so pausing can disable gameplay.
        //private PlayerController player;

        private void Start()
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            //player = FindObjectOfType<PlayerController>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                HandlePause();
            }
        }

        private void HandlePause()
        {
            if (!isGamePaused)
            {
                PauseGame();
            }
            else if (isGamePaused)
            {
                ResumeGame();
            }
        }

        private void PauseGame()
        {
            isGamePaused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            //player.enabled = false;
        }

        public void ResumeGame()
        {
            isGamePaused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            //player.enabled = true;
        }
    }
}