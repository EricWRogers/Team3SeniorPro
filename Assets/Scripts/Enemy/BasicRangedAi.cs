using System.Collections.Generic;
using UnityEngine;

public class BasicRangedAi : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float preferredDistance = 8f;   // The distance this enemy tries to maintain
    public float minDistance = 6f;         // If closer than this, enemy backs away
    public float maxDistance = 10f;        // If farther than this, enemy moves closer

    [Header("Combat")]
    public GameObject projectilePrefab;
    public Transform firePoint;            // Empty GameObject at the NPC's weapon/muzzle
    public float fireRate = 1f;            // Shots per second
    private float fireCooldown = 0f;

    [Header("Drops")]
    public List<GameObject> itemsToDrop;
    public List<string> tags;

    void Update()
    {
        Transform player = PlayerManager.instance.transform;

        // Always face the player
        transform.LookAt(player);

        // Distance to player
        Vector3 dir = player.position - transform.position;
        float distance = dir.magnitude;

        // Adjust position to keep preferred distance
        if (distance < minDistance)
        {
            // Too close → back up
            transform.position -= dir.normalized * speed * Time.deltaTime;
        }
        else if (distance > maxDistance)
        {
            // Too far → move closer
            transform.position += dir.normalized * speed * Time.deltaTime;
        }
        else
        {
            // Within good range → stop moving
        }

        // Shooting logic
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            ShootAtPlayer(player.position);
            fireCooldown = 1f / fireRate;
        }
    }

    void ShootAtPlayer(Vector3 target)
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Vector3 direction = (target - firePoint.position).normalized;
            Rigidbody rb = proj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = direction * 15f; // Adjust projectile speed here
            }
        }
    }

    public void OnDeath()
    {
        if (itemsToDrop.Count > 0)
        {
            int itemIndex = Random.Range(0, itemsToDrop.Count);
            Instantiate(itemsToDrop[itemIndex], transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}