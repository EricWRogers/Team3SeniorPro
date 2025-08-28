using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{

    public List<GameObject> mobsToSpawn;
    public Vector3 spawnArea = new Vector3(100, 40, 100);

    public int numberOfMobs = 10;



    void Start()
    {
       for (int i = 0; i < numberOfMobs; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                Random.Range(-spawnArea.y / 2, spawnArea.y / 2),
                Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
            );

            Vector3 spawnPosition = transform.position + randomPosition;

            int mobIndex = Random.Range(0, mobsToSpawn.Count);
            Instantiate(mobsToSpawn[mobIndex], spawnPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
