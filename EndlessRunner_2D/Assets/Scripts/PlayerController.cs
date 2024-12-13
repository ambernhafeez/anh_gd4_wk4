using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Security.Cryptography;

public class PlayerController : MonoBehaviour
{
    // components
    Animator anim;
    private Rigidbody2D rb;

    // variables
    [SerializeField] bool canJump = true;
    public float jumpForce;
    public bool gameOver = false;
    public int score;
    public int dreamieScore;
    public int ratScore;
    private int jumpCounter = 0;
    public float targetPosX = -241;
    public float speed = 10;
    public bool isInPos = false;
    public bool catchingRat = false;
    public bool caughtRat = false;
    
    // UI text
    public TMP_Text scoreText;
    public TMP_Text titleText;
    public TMP_Text gameOverText;
    public TMP_Text dreamieScoreText;
    public TMP_Text ratScoreText;

    // audio
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip pickupSound;
    private AudioSource playerAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            gameOverText.enabled = true;
        } else gameOverText.enabled = false;
        
        if (transform.position.x < targetPosX)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        } 
        else isInPos = true;
        
        if (isInPos)
        {
            titleText.transform.Translate(Vector2.up * Time.deltaTime * speed);
        }

        if(Input.GetKeyDown(KeyCode.Space) && canJump == true && !gameOver)
        {
            anim.SetTrigger("isJumping");
            anim.SetBool("isTouchingGround", false);

            rb.linearVelocity = Vector3.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCounter += 1;

            playerAudio.PlayOneShot(jumpSound, 1.0f);

        }

        // reload scene if press R or game over is true
        //if(Input.GetKeyDown(KeyCode.R) || gameOver == true)
        if(Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        } 

        // only allow player to jump twice before colliding with ground
        if (jumpCounter >= 2)
        {
            canJump = false;
        }

        if (caughtRat)
        {
            catchingRat = false;
            ratScore += 1;
            ratScoreText.text = ratScore + "";
            caughtRat = false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            // when player collides with ground or platform, set the bool to true
            canJump = true; 
            jumpCounter = 0;
            Debug.Log("ground");
            anim.SetBool("isTouchingGround", true);

        } else if (collision.gameObject.CompareTag("Obstacle") && catchingRat == false)
        {
            // trigger game over on collision with obstacle
            // would be nice if could jump on backs of animals without dying
            // also implement life system
            gameOver = true;
            Debug.Log("Game over!");

            playerAudio.PlayOneShot(crashSound, 1.0f);
            anim.SetTrigger("isDead");
            
        } else if (collision.gameObject.CompareTag("RatObstacle"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            // increase score on collision with items
            score += 1;
            Destroy(collision.gameObject);
            scoreText.text = "Score: " + score;


            playerAudio.PlayOneShot(pickupSound, 1.0f);

        } else if (collision.gameObject.CompareTag("Dreamie"))
        {
            // also add dreamies to score
            score += 1;
            Destroy(collision.gameObject);
            scoreText.text = "Score: " + score;
            playerAudio.PlayOneShot(pickupSound, 1.0f);
            
            // get dreamie score when pick up dreamie. When have 5 dreamies, catch a rat and reset points to 0.
            dreamieScore += 1;
            dreamieScoreText.text = dreamieScore + "/5";

            if (dreamieScore == 5)
            {
                catchingRat = true;
                dreamieScore = 0;
                dreamieScoreText.text = "0/5";
            }

        }
    } 

    // function for reloading the scene 
    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(0); 
        SceneManager.LoadScene(currentScene.buildIndex);
    }

}
