using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public int resourceCount = 2;
    public float health = 10f;
    public ItemData ItemToSpawn;

    private void OnBreak()
    {
        for (int i = 0; i < resourceCount; i++)
        {
            Instantiate(ItemToSpawn.itemPrefab, transform.position, Quaternion.identity);
        }
        transform.GetComponentInParent<NodeSpawner>().canSpawnResourceNode = false;
        transform.GetComponentInParent<NodeSpawner>().ResetTimer();
        Destroy(gameObject);
    }
        public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnBreak();
        }
    }
}
