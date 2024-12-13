using TMPro;
using UnityEngine;

public class RatController : MonoBehaviour
{
    private PlayerController playerControllerScript;
    [SerializeField] public float speed = 70;
    private Rigidbody2D rb;
    private int jumpForce = 275;
    private Animator anim;
    private float startPosX;
    private float startPosY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == true)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }

        // if player isInPos == true, start run animation. Set default animation to idle.
        if (playerControllerScript.isInPos)
        {
            anim.SetBool("isGameStart", true);
        }

        // if player catchingRat is true, slown down rat by moving it towards the player
        if (playerControllerScript.catchingRat)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed * 3);

        } 
        if (transform.position.x <= (playerControllerScript.targetPosX + 20))
        {
            transform.position = new Vector2(startPosX, startPosY);
            playerControllerScript.caughtRat = true;
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
	    if (collision.gameObject.tag == "Item") 
        {
	        Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>()); 
        }
        if (collision.gameObject.CompareTag("RatObstacle"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
