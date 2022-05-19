using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    [SerializeField] GameObject instructionsText;
    [SerializeField] GameObject volumeBar;
    [SerializeField] GameObject playerChoose;
    [SerializeField] Text highScoreBar;
    public GameObject mainMenu;

    public int playerType;
    public int HScore;
    public string HScoreName;

    private bool isInstructionsShown;
    private bool isPlayerChooseShown;

    private void Awake()
    {
        LoadHighScore();
        highScoreBar.text = HScoreName + " " + HScore;
        
        if (instance != null)
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

    public void PlayClick() //starts a game with start button
    {
        if (!isPlayerChooseShown && !isInstructionsShown)
        {
            playerChoose.SetActive(true);
            isPlayerChooseShown = true;
        }
        else if (isPlayerChooseShown)
        {
            playerChoose.SetActive(false);
            isPlayerChooseShown = false;
        }
        else if (!isPlayerChooseShown && isInstructionsShown)
        {
            instructionsText.SetActive(false);
            isInstructionsShown = false;
            playerChoose.SetActive(true);
            isPlayerChooseShown = true;
        }
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
        if (!isInstructionsShown && !isPlayerChooseShown)
        {
            instructionsText.SetActive(true);
            isInstructionsShown = true;
        }
        else if (!isInstructionsShown && isPlayerChooseShown)
        {
            playerChoose.SetActive(false);
            isPlayerChooseShown = false;
            instructionsText.SetActive(true);
            isInstructionsShown = true;
        }
        else if (isInstructionsShown)
        {
            instructionsText.SetActive(false);
            isInstructionsShown = false;
        }
    }

    public void PickMalePlayer()
    {
        playerType = 1;
        playerChoose.SetActive(false);
        mainMenu.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void PickFemalePlayer()
    {
        playerType = 2;
        playerChoose.SetActive(false);
        mainMenu.SetActive(false);
        SceneManager.LoadScene(1);
    }

    [System.Serializable]
    public class SaveData
    {
        public int highScore;
        public string highScoreName;
    }
    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore = HScore;
        data.highScoreName = HScoreName;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveHighscore.json", json);
    }

    public void LoadHighScore()
    {
        string filePath = Application.persistentDataPath + "/saveHighscore.json", json;
        if (File.Exists(filePath))
        {
            string jsonTwo = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(jsonTwo);
            HScore = data.highScore;
            HScoreName = data.highScoreName;
        }
    }
}

