using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool canJump = true;
    [SerializeField] float gravityModifier = 2;
    public float jumpForce;
    public bool gameOver = false;
    public TMP_Text scoreText;
    private Rigidbody2D rb;
    public int score;
    private int jumpCounter = 0;
    Animator anim;

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

        rb.gravityScale *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canJump == true && !gameOver)
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCounter += 1;

            playerAudio.PlayOneShot(jumpSound, 1.0f);

            //anim.SetTrigger("Jump_trig");
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
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            // when player collides with ground, set the bool to true
            canJump = true; 
            jumpCounter = 0;

        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // trigger game over on collision with obstacle
            // would be nice if could jump on backs of animals without dying
            // also implement life system
            gameOver = true;
            Debug.Log("Game over!");

            playerAudio.PlayOneShot(crashSound, 1.0f);

        } else if (collision.gameObject.CompareTag("Item"))
        {
            // increase score on collision with items
            score += 1;
            Destroy(collision.gameObject);
            Debug.Log("Score: " + score);
            scoreText.text = "Score: " + score;

            playerAudio.PlayOneShot(pickupSound, 1.0f);
        }
        if (collision.gameObject.CompareTag("RatObstacle"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    // function for reloading the scene 
    private void ReloadScene()
    {
        Physics.gravity /= gravityModifier;
        Scene currentScene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(0); 
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
