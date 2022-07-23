using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isGamePaused = false;
    [SerializeField]
    private GameObject pauseMenu;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
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
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        player.enabled = true;
    }
}
