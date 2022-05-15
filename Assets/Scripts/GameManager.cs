using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text livesText;
    public Text scoreText;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public GameObject menuScreen;
    public GameObject rulesText;

    public AudioSource foodHasEaten;
    public AudioSource getHit;

    public List<GameObject> foodObjects;

    FoodSpawnManagement foodSpawnManagement;
    SpawnManagement spawnManagement;
    PlayerMovement playerMovement;

    public int lives;
    public int score;
    public bool isGameActive;
    public bool isGamePaused;
    public bool isRulesShown = false;
    // Start is called before the first frame update
    public void Start()
    {
        foodSpawnManagement = GameObject.Find("SpawnerManager").GetComponent<FoodSpawnManagement>();
        spawnManagement = GameObject.Find("SpawnerManager").GetComponent<SpawnManagement>();
        playerMovement = GameObject.Find("Player Male").GetComponent<PlayerMovement>();
        //foodHasEaten = GetComponent<AudioSource>();
        //getHit = GetComponent<AudioSource>();
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
        menuScreen.gameObject.SetActive(false);
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

    public void ShowRules()
    {
        if (!isRulesShown)
        {
            rulesText.gameObject.SetActive(true);
            isRulesShown = true;
        }
        else if (isRulesShown)
        {
            rulesText.gameObject.SetActive(false);
            isRulesShown = false;
        }
    }
}
