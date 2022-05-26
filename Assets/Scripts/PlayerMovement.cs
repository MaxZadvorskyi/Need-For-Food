using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody playerRB;
    Animator playerAnimator;
    GameManager gameManager;
    MainManager mainManager;
    HitParticle hitParticle;

    public AudioSource foodHasEaten;
    public AudioSource getHit;

    public ParticleSystem dirt;

    [SerializeField] private float playerSpeed;
    public int playerType;

    private float xBounds = 10.0f;
    private float upperLowerBound = 5.0f;
    
    // Start is called before the first frame update
    public void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        hitParticle = GameObject.Find("Hit Particle").GetComponent<HitParticle>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoves();
        MovementRestrictions();
        SoundsVolumeControl();
    }
    
    void PlayerMoves() // controls the movement of the player (with animations control) 
    {   gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); 

        if (gameManager.isGameActive)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            float turnSpeed = 0.15f;

            // makes player oriented in the way he moves
            Vector3 playerMoves = new Vector3(horizontalInput, 0, verticalInput);
            if (playerMoves != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerMoves), turnSpeed);
            }

            // set up a player animation and movement
            playerRB.AddForce(playerMoves * playerSpeed * Time.deltaTime);

            if (verticalInput != 0 || horizontalInput != 0 && gameManager.isGameActive)
            {
                playerAnimator.SetInteger("move", 2);
                dirt.gameObject.SetActive(true);
                dirt.Play();
            }
            else
            {
                playerAnimator.SetInteger("move", 0);
                dirt.gameObject.SetActive(false);
                dirt.Pause();
            }
        }
        else
        {
            playerAnimator.SetInteger("move", 0);
        }
    }
       
    void MovementRestrictions()  // creates restrictions for player movement so he cant go outside camera view
    {
        if (transform.position.x > xBounds)
        {
            transform.position = new Vector3(xBounds, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -xBounds)
        {
            transform.position = new Vector3(-xBounds, transform.position.y, transform.position.z);
        }
        else if (transform.position.z > upperLowerBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, upperLowerBound);
        }
        else if (transform.position.z < -upperLowerBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -upperLowerBound);
        }
    }

    private void OnTriggerEnter(Collider other) // defining behevior of the player when he colides with other objects (food and enemies)
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        if (other.gameObject.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            foodHasEaten.Play();
            Debug.Log("You picked up food");
        }
        else if (other.gameObject.CompareTag("Enemy") && gameManager.isGameActive)
        {
            getHit.Play();
            gameManager.UpdateLives(1);
            hitParticle.PlayHitParticle();
            Debug.Log("Auch, you get hit by enemy");
            
            if(gameManager.Lives == 0)
            {
                playerAnimator.SetBool("backward_fall", true);
                dirt.gameObject.SetActive(false);
                gameManager.GameOver();
            }
        }
    }

    public void IncreaseDifficulty()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        if (gameManager.score > 75 && gameManager.score < 150)
        {
            playerAnimator.SetFloat("speed", 0.85f);
        }
        else if (gameManager.score > 150 && gameManager.score < 200)
        {
            playerAnimator.SetFloat("speed", 0.7f);
        }
        else if (gameManager.score > 200 && gameManager.score < 300)
        {
            playerAnimator.SetFloat("speed", 0.55f);
        }
        else if (gameManager.score > 300)
        {
            playerAnimator.SetFloat("speed", 0.4f);
        }
    }// makes player move slower based on how big his score is

    public void SoundsVolumeControl()
    {
        foodHasEaten.volume = mainManager.gameObject.GetComponent<AudioSource>().volume;
        getHit.volume = mainManager.gameObject.GetComponent<AudioSource>().volume;
    }// makes that sound effects are chained to a master volume bar
}
