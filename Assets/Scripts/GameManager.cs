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
    public Text livesText;
    public Text scoreText;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public GameObject playerMale;
    public GameObject playerFemale;

    public AudioSource foodHasEaten;
    public AudioSource getHit;

    public List<GameObject> foodObjects;

    FoodSpawnManagement foodSpawnManagement;
    SpawnManagement spawnManagement;
    PlayerMovement playerMovement;
    MainManager mainManager;

    public int lives;
    public int score;
    public bool isGameActive;
    public bool isGamePaused;

    Vector3 playerSpawnPosition = new Vector3(0, 0, 0);
    Quaternion playerSpawnRotation = new Quaternion(0, 0, 0, 0);

    // Start is called before the first frame update
    public void Start()
    {
            foodSpawnManagement = GameObject.Find("SpawnerManager").GetComponent<FoodSpawnManagement>();
            spawnManagement = GameObject.Find("SpawnerManager").GetComponent<SpawnManagement>();
            mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        
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
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        GameOnPause();
        playerMovement.IncreaseDifficulty();
        spawnManagement.StartCoroutine("IncreaseSpawnRate");
    }

    public void UpdateLives(int LiveCount)
    {
        if (isGameActive)
        {
            lives -= LiveCount;
            livesText.text = "x " + lives;
        }
    }

    public void UpdateScore(int foodPoints)
    {
        if (isGameActive)
        {
            score += foodPoints;
            scoreText.text = "Score: " + score;
        }
    }

    public void GameStart()
    {
        lives = 3;
        livesText.text = "x " + lives;
        score = 0;
        scoreText.text = "Score: " + score;
        isGameActive = true;
        foodSpawnManagement.GameStart();
        spawnManagement.GameStart();
    }


    public void GameOver()
    {
        if(lives == 0)
        {
            isGameActive = false;
            gameOverScreen.gameObject.SetActive(true);
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

    public void GameOnPause()
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
}
