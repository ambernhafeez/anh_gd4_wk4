using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    Vector3 spawnPosition;

    void Start()
    {
        spawnPosition = transform.position;
        InvokeRepeating("SpawnObstacles", 2, 3);
    }

    void SpawnObstacles()
    {
        int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        Instantiate(obstaclePrefabs[obstacleIndex], spawnPosition, transform.rotation);
    } 
}
