using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodControl : MonoBehaviour
{
    public ParticleSystem blowParticle;
    GameManager gameManager;

    public float foodPresenceTime = 7;
    public int foodPoints;

    Vector3 foodRotation = new Vector3(0, 10, 0);

    float rotationSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Invoke("DestroyFood", foodPresenceTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(foodRotation * rotationSpeed * Time.deltaTime);
        if (!gameManager.isGameActive)
        {
            DestroyFood();
        }
    }

    void DestroyFood()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) // makes explosion while player takes food
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(blowParticle, gameObject.transform.position, gameObject.transform.rotation);
            gameManager.UpdateScore(foodPoints);
        }
    }
}
