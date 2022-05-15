using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagement : MonoBehaviour
{
    public GameObject[] enemiesPrefabs;
    GameManager gameManager;

    float SpawnPosUp = 12;
    float SpawnPosBottom = -7;
    float spawnPosSides = 18;
    float spawnHeightY = 0;

    public float enemiesSpawnIntervale = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 RandomPositionEnemiesSpawnFromTop() // randomize swapn position from top
    {
        float spawnPosX = Random.Range(-spawnPosSides, spawnPosSides);
        Vector3 spawnPos = new Vector3(spawnPosX, spawnHeightY, SpawnPosUp);
        return spawnPos;
    }

    Vector3 RandomPositionEnemiesSpawnFromBottom() // randomize swapn position from bottom
    {
        float spawnPosX = Random.Range(-spawnPosSides, spawnPosSides);
        Vector3 spawnPos = new Vector3(spawnPosX, spawnHeightY, SpawnPosBottom);
        return spawnPos;
    }

    Vector3 RandomPositionEnemiesSpawnFromRight() // randomize swapn position from right side of a screen
    {
        float spawnPosZ = Random.Range(SpawnPosBottom, SpawnPosUp);
        Vector3 spawnPos = new Vector3(-spawnPosSides, spawnHeightY, spawnPosZ);
        return spawnPos;
    }

    Vector3 RandomPositionEnemiesSpawnFromLeft() // randomize swapn position from left side of a screen
    {
        float spawnPosZ = Random.Range(SpawnPosBottom, SpawnPosUp);
        Vector3 spawnPos = new Vector3(spawnPosSides, spawnHeightY, spawnPosZ);
        return spawnPos;
    }


    void EnemiesSpawnSelection() // spawn an enemy from random position
    {
        int enemiesList = Random.Range(0, enemiesPrefabs.Length);
        int enemiesSpawnPosition = Random.Range(0, 4);
        switch (enemiesSpawnPosition)
        {
            case 0:
                Instantiate(enemiesPrefabs[enemiesList], RandomPositionEnemiesSpawnFromTop(), enemiesPrefabs[enemiesList].transform.rotation * Quaternion.Euler(0f, 180f, 0f));
                break;
            case 1:
                Instantiate(enemiesPrefabs[enemiesList], RandomPositionEnemiesSpawnFromBottom(), enemiesPrefabs[enemiesList].transform.rotation * Quaternion.Euler(0f, 0f, 0f));
                break;
            case 2:
                Instantiate(enemiesPrefabs[enemiesList], RandomPositionEnemiesSpawnFromRight(), enemiesPrefabs[enemiesList].transform.rotation * Quaternion.Euler(0f, 90f, 0f));
                break;
            case 3:
                Instantiate(enemiesPrefabs[enemiesList], RandomPositionEnemiesSpawnFromLeft(), enemiesPrefabs[enemiesList].transform.rotation * Quaternion.Euler(0f, -90f, 0f));
                break;
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (gameManager.isGameActive)
        {
            yield return new WaitForSeconds(enemiesSpawnIntervale);
            EnemiesSpawnSelection();
        }
    }

    public IEnumerator IncreaseSpawnRate()
    {
        yield return new WaitForSeconds(30);
        enemiesSpawnIntervale = 1.25f;
        yield return new WaitForSeconds(30);
        enemiesSpawnIntervale = 1;
        yield return new WaitForSeconds(30);
        enemiesSpawnIntervale = 0.75f;
        yield return new WaitForSeconds(30);
        enemiesSpawnIntervale = 0.5f;
    }

    public void GameStart()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        StartCoroutine("SpawnEnemy");
    }
}
