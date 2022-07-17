using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttachButtonToGameManager : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    [SerializeField]
    private bool isStartButton;
    [SerializeField]
    private bool isQuitButton;
    [SerializeField]
    private bool isMainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        button = GetComponent<Button>();
        if (isStartButton)
        {
            button.onClick.AddListener(gameManager.StartGame);
        } else if (isQuitButton)
        {
            button.onClick.AddListener(gameManager.QuitGame);
        } else if (isMainMenuButton)
        {
            button.onClick.AddListener(gameManager.EndGame);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
