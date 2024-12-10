using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    private PlayerController playerControllerScript;
    private float leftBound = -10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false) 
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter (Collision collision) 
    {
	    if (collision.gameObject.tag == "Food") 
        {
	        Physics.IgnoreCollision(collision.collider, GetComponent<Collider>()); 
        }
    }
}
