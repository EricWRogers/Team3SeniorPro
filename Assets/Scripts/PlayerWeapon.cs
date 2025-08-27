using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    [Header("Mining Gun Settings")]
    public float minningRange = 3f;
    public float fireRate = 1f;
    public float miningDamage = 1f;
    public bool miningGunActive = true;
    public GameObject miningBeam;
    public LayerMask layerMask;
    private MiningBeamVisual m_miningBeamVisual;

    [Header("Shooting Gun Settings")]
    public float fireRateShooting = 1f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    

    float time;


    void Start()
    {
        m_miningBeamVisual = miningBeam.GetComponent<MiningBeamVisual>();
    }

    // Update is called once per frame
    void Update()
    {
        
        time += Time.deltaTime;
        if (miningGunActive)
        {
            if (time >= 1f / fireRate && Input.GetMouseButton(0))
            {
                miningBeam.SetActive(true);
                //m_miningBeamVisual.ChangeScale(new Vector3(miningBeam.transform.localScale.x, minningRange, miningBeam.transform.localScale.z), new Vector3(miningBeam.transform.localPosition.x, minningRange + .5f, miningBeam.transform.localPosition.z));
                if (Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hit, minningRange))
                {
                    if (hit.collider.CompareTag("Mineable") && Input.GetMouseButton(0))
                    {
                        hit.collider.GetComponent<ResourceNode>().TakeDamage(miningDamage);
                        time = 0f;
                    }
                }

            }
            else if (!Input.GetMouseButton(0)) miningBeam.SetActive(false);
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
