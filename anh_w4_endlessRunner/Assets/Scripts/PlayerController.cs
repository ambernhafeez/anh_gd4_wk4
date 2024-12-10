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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCounter += 1;
        }

        // reload scene if press R or game over is true
        if(Input.GetKeyDown(KeyCode.R) || gameOver == true)
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
        } else if (collision.gameObject.CompareTag("Animal"))
        {
            // trigger game over on collision with animal
            // would be nice if could jump on backs of animals
            // also implement life system
            gameOver = true;
            Debug.Log("Game over!");
        } else if (collision.gameObject.CompareTag("Food"))
        {
            // increase score on collision with food items
            score += 1;
            Destroy(collision.gameObject);
            Debug.Log("Score: " + score);
            scoreText.text = "Score: " + score;
        }
    }

    private void ReloadScene()
    {
        Physics.gravity /= gravityModifier;
        Scene currentScene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(0); 
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
