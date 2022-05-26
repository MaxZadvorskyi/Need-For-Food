using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawnManagement : MonoBehaviour
{
    public GameObject[] foodPrefabs;
    GameManager gameManager;

    private float spawnAreaZ = 5.0f;
    private float spawnAreaX = 10.0f;
    private float delayFoodSpawn = 2.0f;

    Vector3 FoodSpawnPosition() // randomizes spawning position for food  
    {
        float SpawnPositionZ = Random.Range(-spawnAreaZ, spawnAreaZ);
        float SpawnPositionX = Random.Range(-spawnAreaX, spawnAreaX);
        Vector3 FoodSpawn = new Vector3(SpawnPositionX, 0.5f, SpawnPositionZ);
        return FoodSpawn;
    }

    private void FoodSpawnSelection() // chooses random food type to spawn
    {
        int foodList = Random.Range(0, foodPrefabs.Length);
        Instantiate(foodPrefabs[foodList], FoodSpawnPosition(), foodPrefabs[foodList].transform.rotation);
    }

    private IEnumerator SpawnFood() // spawns food with time delays while game is active and running
    {
        while (gameManager.isGameActive)
        {
            yield return new WaitForSeconds(delayFoodSpawn);
            FoodSpawnSelection();
        }
    }

    public void GameStart()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        StartCoroutine("SpawnFood");
    }
}
