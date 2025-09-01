using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectToSpawn;

    public static ObjectSpawner instance;

    public int poolSize = 20;
    public float spawnInterval = 2f;
    private float nextSpawnTime;
    public Vector3 spawnAreaSize = new Vector3(50f, 20f, 50f);

    private int totalSpawned = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            SpawnObject();
            totalSpawned++;
        }
    }


    void Update()
    {
        if (Time.time >= nextSpawnTime && totalSpawned <= poolSize)
        {
            SpawnObject();
            nextSpawnTime = Time.time + spawnInterval;
            totalSpawned++;
        }

        //transform.Rotate(Vector3.up * Time.deltaTime * 0.5f);
    }



    void SpawnObject()
    {

        int randIndex = Random.Range(0, objectToSpawn.Length);
        Vector3 spawnPosition = new Vector3(
            Random.Range((-spawnAreaSize.x / 2), (spawnAreaSize.x / 2)),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range((-spawnAreaSize.z / 2), (spawnAreaSize.z / 2))
        ) + transform.position;

        GameObject temp = Instantiate(objectToSpawn[randIndex], spawnPosition, Random.rotation);
        temp.transform.parent = transform;

        temp.transform.localScale = Vector3.one * Random.Range(6f, 20f);
    }

    public void DestoryedAstroid()
    {
        totalSpawned--;
    }
    
        void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
