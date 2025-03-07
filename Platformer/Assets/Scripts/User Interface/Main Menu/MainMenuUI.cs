using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Reference's")]
    [Space]
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject settingsMenuUI;
    [Space]
    [Header("Start Button")]
    [Space]
    [SerializeField] private int sceneNumber = 1;

    private AsyncOperation gameStartOperation;


    private void Start()
    {
        gameStartOperation = SceneManager.LoadSceneAsync(sceneNumber); //Loads the level async
        gameStartOperation.allowSceneActivation = false;
    }

    private void Awake()
    {
        if (settingsMenuUI == null)
        {
            Debug.Log("Settings Menu needs to be created"); // Change when settings is created.
        }

        if (mainMenuUI == null)
        {
            Debug.Log("Main Menu needs to be assigned");
        }
    }

    public void mm_OnClickStart()
    {
        if (gameStartOperation != null && gameStartOperation.progress >= 0.9f)
        {
            gameStartOperation.allowSceneActivation = true;
        }
        else
        {
            Debug.Log("Game is still loading...");
        }
    }


    public void mm_OnClickSettings()
    {
        mainMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    public void mm_OnClickQuit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void mm_OnClickLoad()
    {
        // Load game from saved state.
    }

    public void mm_OnClickDiscord()
    {
        Application.OpenURL("https://discord.gg/3kdCAkpnmz");
    }

}
