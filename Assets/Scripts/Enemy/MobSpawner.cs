using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [Header("Spawn Area")]
    public Vector3 spawnArea = new Vector3(100, 40, 100);

    [Header("Passive Mobs")]
    public List<GameObject> passiveMobsToSpawn; 
    public int numberOfPassiveMobs = 10;
    public float passiveSpawnInterval = 5f;
    private int currentPassiveMobs = 0;
    private float passiveTimer = 0f;

    [Header("Enemy Mobs (passive + waves)")]
    public List<GameObject> enemyMobsToSpawn;
    public int numberOfEnemyMobs = 1;       // max concurrent passive enemies
    public float enemySpawnInterval = 20f;
    private int currentEnemyMobs = 0;
    private float enemyTimer = 0f;
    public int maxEnemyCount = 5;           // global hard cap

    // Controlled by WaveManager
    private bool allowPassiveEnemies = false;

    // Called by WaveManager when first wave ends
    public void EnablePassiveEnemySpawning()
    {
        allowPassiveEnemies = true;
    }

    void Update()
    {
        // Passive mob spawning 
        passiveTimer += Time.deltaTime;
        if (passiveTimer >= passiveSpawnInterval)
        {
            SpawnPassiveMob();
            passiveTimer = 0f;
        }

        // Passive enemy spawning (disabled until wave 1 is complete) ---
        if (allowPassiveEnemies)
        {
            enemyTimer += Time.deltaTime;
            if (enemyTimer >= enemySpawnInterval)
            {
                if (currentEnemyMobs < maxEnemyCount)
                    SpawnEnemyMob();
                enemyTimer = 0f;
            }
        }
    }

    void SpawnPassiveMob()
    {
        if (currentPassiveMobs >= numberOfPassiveMobs || passiveMobsToSpawn.Count == 0) return;

        Vector3 spawnPosition = GetRandomSpawnPosition();
        int mobIndex = Random.Range(0, passiveMobsToSpawn.Count);
        Instantiate(passiveMobsToSpawn[mobIndex], spawnPosition, Quaternion.identity);
        currentPassiveMobs++;
    }

    public void SpawnEnemyMob()
    {
        if (currentEnemyMobs >= numberOfEnemyMobs || enemyMobsToSpawn.Count == 0) return;

        Vector3 spawnPosition = GetRandomSpawnPosition();
        int mobIndex = Random.Range(0, enemyMobsToSpawn.Count);
        Instantiate(enemyMobsToSpawn[mobIndex], spawnPosition, Quaternion.identity);
        currentEnemyMobs++;
    }

    // Used only for waves
    public GameObject SpawnEnemyMobForWave()
    {
        if (enemyMobsToSpawn.Count == 0) return null;

        Vector3 spawnPosition = GetRandomSpawnPosition();
        int mobIndex = Random.Range(0, enemyMobsToSpawn.Count);

        GameObject enemy = Instantiate(enemyMobsToSpawn[mobIndex], spawnPosition, Quaternion.identity);
        return enemy;
    }
    Vector3 GetRandomSpawnPosition()
    {
        return transform.position + new Vector3(
            Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
            Random.Range(-spawnArea.y / 2, spawnArea.y / 2),
            Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
        );
    }

    // Enemy counters 
    public int GetCurrentEnemyCount()
    {
        return currentEnemyMobs;
    }

    public void OnEnemyDeath()
    {
        currentEnemyMobs = Mathf.Max(0, currentEnemyMobs - 1);
    }
}
