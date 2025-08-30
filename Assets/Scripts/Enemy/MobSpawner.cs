using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{

    public List<GameObject> mobsToSpawn;
    public Vector3 spawnArea = new Vector3(100, 40, 100);

    public int numberOfMobs = 10;
    public int currentMobs = 0;
    public float spawnInterval = 5f;
    private float timer = 0f;



    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnMobs();
            timer = 0f;
        }
    }

    public void SpawnMobs()
    {
        if (currentMobs >= numberOfMobs) return; // don't spawn if at max

        Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-spawnArea.x / 2, spawnArea.x / 2), Random.Range(-spawnArea.y / 2, spawnArea.y / 2), Random.Range(-spawnArea.z / 2, spawnArea.z / 2));
        int mobIndex = Random.Range(0, mobsToSpawn.Count);
        Instantiate(mobsToSpawn[mobIndex], spawnPosition, Quaternion.identity);
        currentMobs++;    
    }
}
