using Beehaw.Character;
using UnityEngine;

namespace Beehaw.Managers
{
    public class PauseMenu : MonoBehaviour
    {
        private bool isGamePaused = false;
        [SerializeField]
        private GameObject pauseMenu;
        private PlayerController player;

        private void Start()
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
            player.enabled = false;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Pause");
        }

        public void ResumeGame()
        {
            isGamePaused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            player.enabled = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Unpause");
        }
    }
}