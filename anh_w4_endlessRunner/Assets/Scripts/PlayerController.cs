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
    private Rigidbody rb;
    public int score;
    private int jumpCounter = 0;
    Animator anim;
    public ParticleSystem dirtParticle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canJump == true && !gameOver)
        {
            dirtParticle.Stop();
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCounter += 1;

            anim.SetTrigger("Jump_trig");
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // when player collides with ground, set the bool to true
            canJump = true; 
            jumpCounter = 0;
            dirtParticle.Play();
            
            // can also do if (collision.transform.tag == "Animal"); 
        } else if (collision.gameObject.CompareTag("Animal"))
        {
            // trigger game over on collision with animal
            // would be nice if could jump on backs of animals without dying
            // also implement life system
            gameOver = true;
            Debug.Log("Game over!");
            anim.SetBool("Death_b", true);
            anim.SetInteger("DeathType_int", 1);
            dirtParticle.Stop();

        } else if (collision.gameObject.CompareTag("Food"))
        {
            // increase score on collision with food items
            score += 1;
            Destroy(collision.gameObject);
            Debug.Log("Score: " + score);
            scoreText.text = "Score: " + score;
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
