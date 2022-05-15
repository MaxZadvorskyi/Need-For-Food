using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    [SerializeField] GameObject instructionsText;
    [SerializeField] GameObject volumeBar;
    public GameObject mainMenu;

    private bool isInstructionsShown;

    private void Awake()
    {  
        if(instance != null)
        {
            Destroy(gameObject);
            Destroy(volumeBar);
            Destroy(mainMenu);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(volumeBar);
        DontDestroyOnLoad(mainMenu);
    }

    public void GameStart() //starts a game with start button
    {
        mainMenu.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void ShowInstructions() //shows player the rules
    {
        if (!isInstructionsShown)
        {
            instructionsText.SetActive(true);
            isInstructionsShown = true;
        }
        else
        {
            instructionsText.SetActive(false);
            isInstructionsShown = false;
        }  
    }
}

