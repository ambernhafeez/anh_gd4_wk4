using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    Vector3 spawnPosition;

    private float leftBound = -10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnItems", 1, Random.Range(0.5f,2.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    void SpawnItems()
    {
        spawnPosition = new Vector3(transform.position.x, Random.Range(0.0f, 4.5f), 0);
        int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        Instantiate(obstaclePrefabs[obstacleIndex], spawnPosition, Quaternion.Euler(-100,0,0));
    } 
}
