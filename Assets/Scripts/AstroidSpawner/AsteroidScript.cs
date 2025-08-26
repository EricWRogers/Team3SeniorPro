using UnityEngine;

public class AsteroidScript : MonoBehaviour
{

    public GameObject resourcePrefab;
    public int resourceCount = 3;
    public bool canSpawnResources = true;
    public float health = 10f;

    public float minThrust = 0.1f;
    public float maxThrust = 0.5f;
    public float minSpinSpeed = 1f;
    public float maxSpinSpeed = 5f;
    private float spinSpeed;
    private Rigidbody rb;


    void Start()
    {

        rb = GetComponent<Rigidbody>()!=null ? GetComponent<Rigidbody>() : gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;


        spinSpeed = Random.Range(minSpinSpeed, maxSpinSpeed);
        float thrust = Random.Range(minThrust, maxThrust);

         rb.AddForce(Vector3.left * thrust , ForceMode.Impulse);
    }


    void Update()
    {
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
        if (transform.position.magnitude - ObjectSpawner.instance.transform.position.magnitude > 200f)
        {
            transform.localScale = transform.localScale * 0.999f;
            if (transform.localScale.x < 0.5f)
            {
                Destroy(gameObject);
                ObjectSpawner.instance.DestoryedAstroid();
            }
        }
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
