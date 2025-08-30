using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [Header("General Gun Settings")]
    public Transform raycastStart;
    public float raycastRange;


    [Header("Mining Gun Settings")]
    public float minningRange = 3f;
    public float fireRate = 1f;
    public float miningDamage = 1f;
    public bool miningGunActive = true;
    private LineRenderer m_miningBeamVisual;

    [Header("Shooting Gun Settings")]
    public float fireRateShooting = 1f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;

    [Header("Grapple Hook Settings")]
    


    float time;


    void Start()
    {
        m_miningBeamVisual = gameObject.GetComponent<LineRenderer>();
        m_miningBeamVisual.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        bool miningRay = Physics.Raycast(new Ray(raycastStart.position, raycastStart.forward), out RaycastHit miningHit, minningRange);//raycast for mining
        bool ray = Physics.Raycast(new Ray(raycastStart.position, raycastStart.forward), out RaycastHit hit, raycastRange);//raycast for aiming
        
        if (miningRay)
        {
            if (miningGunActive)
            {
                if (time >= 1f / fireRate && Input.GetMouseButton(0))
                {
                    m_miningBeamVisual.enabled = true;
                    m_miningBeamVisual.SetPosition(0, firePoint.position);
                    m_miningBeamVisual.SetPosition(1, miningHit.point);

                    if (miningHit.collider.CompareTag("Mineable"))
                    {
                        miningHit.collider.GetComponent<ResourceNode>().TakeDamage(miningDamage);
                        time = 0f;
                    }
                }
            }
        }
        if (Input.GetMouseButton(0) && miningGunActive)
        {
            m_miningBeamVisual.enabled = true;
            m_miningBeamVisual.SetPosition(0, firePoint.position);
            m_miningBeamVisual.SetPosition(1, raycastStart.position + raycastStart.forward * minningRange);
        }
        else if (!Input.GetMouseButton(0))
        {
            m_miningBeamVisual.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            miningGunActive = !miningGunActive;
            Debug.Log("Mining Gun Active: " + miningGunActive);
        }


        if(ray)
            firePoint.transform.LookAt(hit.point);
        else
            firePoint.transform.LookAt(raycastStart.position + raycastStart.forward * minningRange);
        if (!miningGunActive && time >= 1f / fireRateShooting && Input.GetMouseButton(0))
        {
            Shoot();
            time = 0f;
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(raycastStart.position, raycastStart.forward * minningRange);
    }
}
