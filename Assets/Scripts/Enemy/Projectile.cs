using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*public class Projectile : MonoBehaviour
{
    public int damage = 5;              // How much damage this bullet does
    public float lifetime = 5f;         // Destroy after 5s if it hits nothing
    public string playerTag = "Player"; // Tag your player with this

    private void Start()
    {
        Destroy(gameObject, lifetime); // auto cleanup
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if we hit the player
        if (collision.gameObject.CompareTag(playerTag))
        {
            m_maxHealth playerHealth = collision.gameObject.GetComponent<m_maxHealth>();
            if (playerHealth != null)
            {
                playerHealth.Damage(damage);
            }
        }

        // Always destroy projectile on impact
        Destroy(gameObject);
    }

    //yeah everything is cooked and im cooked this is scrapped dotneven wanna continue it rn
}*/