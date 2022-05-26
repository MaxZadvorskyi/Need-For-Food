using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagement : MonoBehaviour
{
    public GameObject[] enemiesPrefabs;
    GameManager gameManager;

    private float SpawnPosUpBottom = 6.0f;
    private float spawnPosSides = 11.0f;
    private float spawnHeightY = 0;

    private float maxSpawnIntervale = 0.5f;

    [SerializeField] private float timeStep;
    [SerializeField] private float increasingStep;
    [SerializeField] private float enemiesSpawnIntervale = 1.75f;

    Vector3 RandomPositionEnemiesSpawnFromTop() // randomizing spawn position from the top
    {
        float spawnPosX = Random.Range(-spawnPosSides, spawnPosSides);
        Vector3 spawnPos = new Vector3(spawnPosX, spawnHeightY, SpawnPosUpBottom);
        return spawnPos;
    }

    Vector3 RandomPositionEnemiesSpawnFromBottom() // randomizing spawn position from the bottom
    {
        float spawnPosX = Random.Range(-spawnPosSides, spawnPosSides);
        Vector3 spawnPos = new Vector3(spawnPosX, spawnHeightY, -SpawnPosUpBottom);
        return spawnPos;
    }

    Vector3 RandomPositionEnemiesSpawnFromRight() // randomizing spawn position from the right side of a screen
    {
        float spawnPosZ = Random.Range(-SpawnPosUpBottom, SpawnPosUpBottom);
        Vector3 spawnPos = new Vector3(-spawnPosSides, spawnHeightY, spawnPosZ);
        return spawnPos;
    }

    Vector3 RandomPositionEnemiesSpawnFromLeft() // randomizing spawn position from the left side of a screen
    {
        float spawnPosZ = Random.Range(-SpawnPosUpBottom, SpawnPosUpBottom);
        Vector3 spawnPos = new Vector3(spawnPosSides, spawnHeightY, spawnPosZ);
        return spawnPos;
    }


    private void EnemiesSpawnSelection() // spawns an enemy from a random position
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

    IEnumerator SpawnEnemy() // spawns an enemy based on time intervals
    {
        while (gameManager.isGameActive)
        {
            yield return new WaitForSeconds(enemiesSpawnIntervale);
            EnemiesSpawnSelection();
        }
    }

    public IEnumerator IncreaseSpawnRate() // increases enemies spawn rate based on time in game
    {
        WaitForSeconds increaseRate = new WaitForSeconds(timeStep);
        while (gameManager.isGameActive && enemiesSpawnIntervale > maxSpawnIntervale)
        {
            enemiesSpawnIntervale -= increasingStep;
            yield return increaseRate;
        }    
    }

    public void GameStart()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        StartCoroutine("SpawnEnemy");
    }
}
