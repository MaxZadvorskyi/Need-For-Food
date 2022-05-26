using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodControl : MonoBehaviour
{
    public ParticleSystem blowParticle;
    GameManager gameManager;

    [SerializeField] private float foodPresenceTime = 7.0f;
    [SerializeField] private int foodPoints;

    private Vector3 foodRotation = new Vector3(0, 10, 0);
    private float rotationSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Invoke("DestroyFood", foodPresenceTime);
    }

    // Update is called once per frame
    void Update()
    {
        FoodConstantRotation();
        if (!gameManager.isGameActive)
        {
            DestroyFood();
        }
    }

    private void DestroyFood() // destroys food
    {
        Destroy(gameObject);
    }

    private void FoodConstantRotation() // gives food a rotation (design purpose only)
    {
        transform.Rotate(foodRotation * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) // creates explosion while player takes food and updates score
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(blowParticle, gameObject.transform.position, gameObject.transform.rotation);
            gameManager.UpdateScore(foodPoints);
        }
    }
}
