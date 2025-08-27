using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    [Header("Mining Gun Settings")]
    public float minningRange = 3f;
    public float fireRate = 1f;
    public float miningDamage = 1f;
    public bool miningGunActive = true;

    [Header("Shooting Gun Settings")]
    public float fireRateShooting = 1f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    

    float time;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        time += Time.deltaTime;
        if (miningGunActive){
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
        if(!miningGunActive && time >= 1f / fireRateShooting && Input.GetMouseButton(0))
        {
            Shoot();
            time = 0f;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            miningGunActive = !miningGunActive;
            Debug.Log("Mining Gun Active: " + miningGunActive);
        }



    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
            rb.useGravity = false; 
        }
        Destroy(bullet, 2f); // Destroy the bullet after 2 seconds to prevent memory leaks
    }
}
