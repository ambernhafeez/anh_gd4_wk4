using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    private Vector3 startPosition;
    [SerializeField] float scrollSpeed = 3.5f;
    // all backgrounds are 640 pixels wide so this can be hardcoded
    float backgroundWidth = 640;
    private PlayerController playerControllerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        //backgroundWidth = GetComponent<SpriteRenderer>().size.x;
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false && playerControllerScript.isInPos == true) 
        {
            transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        }

        if (transform.position.x < startPosition.x - backgroundWidth)
        {
            transform.position = startPosition; 
        }

        if (playerControllerScript.catchingRat)
        {
            transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed * 2);
        }
    }
}
