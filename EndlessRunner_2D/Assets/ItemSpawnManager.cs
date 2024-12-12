using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    Vector3 spawnPosition;

    private float leftBound = -400;
    private PlayerController playerControllerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnItems", 0, Random.Range(0.5f,2.0f));
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }

    void SpawnItems()
    {
        if (playerControllerScript.gameOver == false)
        {
            spawnPosition = new Vector3(transform.position.x, Random.Range(-146.0f, 25.0f), 0);
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[obstacleIndex], spawnPosition, transform.rotation);
        }
        
    } 
}
