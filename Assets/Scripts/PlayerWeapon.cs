using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    public float minningRange = 3f;
    public float fireRate = 1f;
    public float miningDamage = 1f;
    public bool miningGunActive = true;
    float time;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!miningGunActive) return;
        time += Time.deltaTime;
        if (time >= 1f / fireRate && Input.GetMouseButton(0))
        {
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hit, minningRange))
            {
                if (hit.collider.CompareTag("Mineable") && Input.GetMouseButton(0))
                {
                    hit.collider.GetComponent<AsteroidScript>().TakeDamage(miningDamage);
                     time = 0f;
                }
            }
           
        }

    }
}
