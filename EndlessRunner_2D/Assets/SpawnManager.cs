using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    Vector3 spawnPosition;

    private PlayerController playerControllerScript;

    void Start()
    {
        spawnPosition = transform.position;
        InvokeRepeating("SpawnObstacles", 1, Random.Range(3,6));
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnObstacles()
    {
        if (playerControllerScript.gameOver == false)
        {
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[obstacleIndex], spawnPosition, transform.rotation);
        }
        
    } 
}
