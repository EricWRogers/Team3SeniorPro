using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectToSpawn;

    private GameObject[] pooledObjects;
    public int poolSize = 10;
    public float spawnInterval = 2f;
    private float nextSpawnTime;
    public Vector3 spawnAreaSize = new Vector3(50f, 10f, 50f);
  


    void Update()
    {
            if (Time.time >= nextSpawnTime)
            {
                SpawnObject();
                nextSpawnTime = Time.time + spawnInterval;
            }
    }

    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }

    void SpawnObject(){

        int randIndex = Random.Range(0, objectToSpawn.Length);
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        ) + transform.position;

        Instantiate(objectToSpawn[randIndex], spawnPosition, Quaternion.identity);
    }
}
