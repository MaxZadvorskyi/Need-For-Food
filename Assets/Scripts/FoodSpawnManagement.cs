using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawnManagement : MonoBehaviour
{
    public GameObject[] foodPrefabs;
    GameManager gameManager;

    public float spawnAreaZ = 5;
    public float spawnAreaX = 14;

    float delayFoodSpawn = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 FoodSpawnPosition() // randomize spawn position of food 
    {
        float SpawnPositionZ = Random.Range(-spawnAreaZ, spawnAreaZ);
        float SpawnPositionX = Random.Range(-spawnAreaX, spawnAreaX);
        Vector3 FoodSpawn = new Vector3(SpawnPositionX, 0.5f, SpawnPositionZ);
        return FoodSpawn;
    }

    void FoodSpawnSelection() // spawn food
    {
        int foodList = Random.Range(0, foodPrefabs.Length);
        Instantiate(foodPrefabs[foodList], FoodSpawnPosition(), foodPrefabs[foodList].transform.rotation);
    }

    IEnumerator SpawnFood()
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
