using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public float minThrust = 0.1f;
    public float maxThrust = 0.5f;
    public float minSpinSpeed = 1f;
    public float maxSpinSpeed = 5f;
    public List<GameObject> resourceNodes;
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

    
}
