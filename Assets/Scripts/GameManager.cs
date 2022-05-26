using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR 
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text livesText;
    [SerializeField] private Text scoreText;
   
    public Text highScorerName;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject tryBetterText;
    [SerializeField] private GameObject highScoreOwner;
    [SerializeField] private GameObject playerMale;
    [SerializeField] private GameObject playerFemale;

    FoodSpawnManagement foodSpawnManagement;
    SpawnManagement spawnManagement;
    PlayerMovement playerMovement;
    MainManager mainManager;

    private int lives;
    public int Lives // ENCAPSULATION
    {
        get { return lives; }
        set { lives = value; }
    } 

    public int score;

    private bool isGamePaused;

    public bool isGameActive;

    private Vector3 playerSpawnPosition = new Vector3(0, 0, 0);
    private Quaternion playerSpawnRotation = new Quaternion(0, 0, 0, 0);

    // Start is called before the first frame update
    public void Start()
    {
        foodSpawnManagement = GameObject.Find("SpawnerManager").GetComponent<FoodSpawnManagement>();
        spawnManagement = GameObject.Find("SpawnerManager").GetComponent<SpawnManagement>();
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        GameStartWithCharacter();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        spawnManagement.StartCoroutine("IncreaseSpawnRate");
    }

    // Update is called once per frame
    void Update()
    {
        GameOnPause();
        playerMovement.IncreaseDifficulty();
    }

    private void ShowLives()
    {
       livesText.text = "x " + Lives;
    }

    private void ShowScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int LiveCount) // updates lives counter when player takes a hit
    {
        if (isGameActive)
        {
            Lives -= LiveCount;
            ShowLives();
        }
    }

    public void UpdateScore(int foodPoints) // updates score counter when player picks up food
    {
        if (isGameActive)
        {
            score += foodPoints;
            ShowScore();
        }
    }

    public void GameStartWithCharacter() // defines a character to spawn depending on players choose from main menu
    {
        if (mainManager.playerType == 1)
        {
            Instantiate(playerMale, playerSpawnPosition, playerSpawnRotation);
            GameStart();
        }
        else if (mainManager.playerType == 2)
        {
            Instantiate(playerFemale, playerSpawnPosition, playerSpawnRotation);
            GameStart();
        }
        else
        {
            return;
        }
    }

    public void GameStart() // starts a game and sets a default values of lives and score
    {
        Lives = 3;
        score = 0;
        ShowLives();
        ShowScore();
        isGameActive = true;
        foodSpawnManagement.GameStart();
        spawnManagement.GameStart();
    }


    public void GameOver() // defines when game is over and is new highscore set or not
    {
        if(Lives == 0 && score < mainManager.HScore || Lives == 0 && score == 0)
        {
            isGameActive = false;
            gameOverScreen.SetActive(true);
            tryBetterText.SetActive(true);
        }
        else if (Lives == 0 && score > mainManager.HScore)
        {
            isGameActive = false;
            gameOverScreen.SetActive(true);
            highScoreOwner.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        mainManager.mainMenu.SetActive(true);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void GameOnPause() // pauses and unpauses the game
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameActive && !isGamePaused) 
        {
            Time.timeScale = 0;
            isGamePaused = true;
            pauseScreen.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isGameActive && isGamePaused)
        {
            Time.timeScale = 1;
            isGamePaused = false;
            pauseScreen.gameObject.SetActive(false);
        }
    }

    public void HighScoreSet() // updates a highscore
    {
        mainManager.HScore = score;
        mainManager.HScoreName = highScorerName.text;
        mainManager.SaveHighScore();
        highScoreOwner.SetActive(false);
    }
}
