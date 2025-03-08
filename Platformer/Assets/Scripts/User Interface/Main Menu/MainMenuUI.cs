using System.Collections;
using System.Xml.Serialization;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Main Menu Reference's")]
    [Space]
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject settingsMenuUI;
    [SerializeField] private GameObject helpMenuUI;
    [Space]
    [Header("Start Button")]
    [Space]
    [SerializeField] private int sceneNumber = 1;

    [Header("Help Section Reference's")]
    [Space]
    [SerializeField] private GameObject nextArrowUI;
    [SerializeField] private GameObject backArrowUI;
    [SerializeField] private GameObject batteryHelpText;
    [SerializeField] private GameObject inventoryHelpText;

    private AsyncOperation gameStartOperation;


    private void Start()
    {
        gameStartOperation = SceneManager.LoadSceneAsync(sceneNumber); //Loads the level async
        gameStartOperation.allowSceneActivation = false;
    }

    private void Awake()
    {
        DebugChecker();
        helpMenuUI.SetActive(false);
        backArrowUI.SetActive(false);
        inventoryHelpText.SetActive(false);
    }

    private void DebugChecker()
    {
        if (settingsMenuUI == null)
        {
            Debug.LogError("Settings Menu needs to be created"); // Change when settings is created.
        }

        if (mainMenuUI == null)
        {
            Debug.LogError("Main Menu needs to be assigned");
        }

        if (helpMenuUI == null)
        {
            Debug.LogError("Help menu needs to be assigned;");
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
        EditorApplication.ExitPlaymode();
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

    public void mm_OnClickHelp()
    {
        helpMenuUI.SetActive(true);
    }

    public void mm_OnClickExitHelp()
    {
        helpMenuUI.SetActive(false);
    }

    public void hm_OnClickNextArrow()
    {
        batteryHelpText.SetActive(false);
        inventoryHelpText.SetActive(true);
        backArrowUI.SetActive(true);
        nextArrowUI.SetActive(false);
    }

    public void hm_OnClickBackArrow()
    {
        batteryHelpText.SetActive(true);
        inventoryHelpText.SetActive(false);
        backArrowUI.SetActive(false);
        nextArrowUI.SetActive(true);
    }

}
