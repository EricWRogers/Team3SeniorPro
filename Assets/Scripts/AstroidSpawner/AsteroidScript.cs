using UnityEngine;

public class AsteroidScript : MonoBehaviour
{

    public GameObject resourcePrefab;
    public int resourceCount = 3;
    public bool canSpawnResources = true;
    public float health = 10f;

    public float minThrust = 0.5f;
    public float maxThrust = 0.5f;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>()!=null ? GetComponent<Rigidbody>() : gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;


        float thrust = Random.Range(minThrust, maxThrust);

         rb.AddForce(Vector3.forward * thrust, ForceMode.Impulse);
    }


    void Update()
    {

    }

    private void OnDeath()
    {
        if (canSpawnResources)
        {
            for (int i = 0; i < resourceCount; i++)
            {

                Instantiate(resourcePrefab, transform.position, Quaternion.identity);
            }
            ObjectSpawner.instance.DestoryedAstroid();
            Destroy(gameObject);
        }
    }
        public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDeath();
        }
    }
}
