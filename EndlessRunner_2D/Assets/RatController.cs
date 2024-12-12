using UnityEngine;

public class RatController : MonoBehaviour
{
    private PlayerController playerControllerScript;
    [SerializeField] public float speed = 70;
    private Rigidbody2D rb;
    private int jumpForce = 250;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == true)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
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
