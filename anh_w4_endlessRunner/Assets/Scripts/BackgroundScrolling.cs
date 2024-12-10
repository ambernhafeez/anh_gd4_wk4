using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    private Vector3 startPosition;
    [SerializeField] float scrollSpeed = 3.5f;
    float backgroundWidth;
    private PlayerController playerControllerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        backgroundWidth = GetComponent<BoxCollider>().size.x;
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false) 
        {
            transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        }

        if (transform.position.x < startPosition.x - (backgroundWidth/2))
        {
            transform.position = startPosition; 
        }
    }
}
