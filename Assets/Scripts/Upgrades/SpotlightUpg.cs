using UnityEngine;
using System.Collections.Generic;
using SuperPupSystems.Helper; // Import your Health namespace

public class SpotlightUpg : MonoBehaviour
{
    [Header("Targets")]
    public Transform playerTarget;
    public float detectionRadius = 15f;

    [Header("Spotlight Settings")]
    public Light spotLight;
    public Color playerColor = Color.white;
    public Color enemyColor = Color.red;
    public float rotationSpeed = 2f;
    public float searchAngle = 45f;
    public float searchSpeed = 2f;

    [Header("Enemy Damage")]
    public int burnDamagePerSecond = 5; // must be int because Health.Damage(int)

    private enum SpotState { Searching, PlayerViewing, EnemyViewing }
    private SpotState currentState = SpotState.Searching;

    private float searchTimer = 0f;
    private Transform currentEnemy;

    private Health currentEnemyHealth;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip searchClip;
    public AudioClip playerClip;
    public AudioClip enemyClip;

    //may add where if theres an object in between you and the spotlight, cut viewing capabilities and return to search mode


    void Start()
    {
        if (spotLight == null)
            spotLight = GetComponentInChildren<Light>();
    }

    void Update()
    {
        // Priority 1: Enemy
        currentEnemy = FindClosestEnemyInRange();
        if (currentEnemy != null)
        {
            currentState = SpotState.EnemyViewing;
            EnemyFocus(currentEnemy);
            return;
        }

        // Priority 2: Player
        if (playerTarget != null && Vector3.Distance(transform.position, playerTarget.position) <= detectionRadius)
        {
            currentState = SpotState.PlayerViewing;
            PlayerFocus();
            return;
        }

        // Priority 3: Searching
        currentState = SpotState.Searching;
        SearchMode();
    }

    private void PlayerFocus()
    {
        spotLight.color = playerColor;
        RotateTowards(playerTarget.position);

        // play player clip
        PlayClip(playerClip);
    }
    

    private float damageBuffer = 0f; // keeps track of fractional damage

    private void EnemyFocus(Transform enemy)
    {
        spotLight.color = enemyColor;
        RotateTowards(enemy.position);

        // play enemy clip
        PlayClip(enemyClip);

        Health hp = enemy.GetComponent<Health>();
        if (hp != null)
        {
            damageBuffer += burnDamagePerSecond * Time.deltaTime;
            if (damageBuffer >= 1f)
            {
                int damageToApply = Mathf.FloorToInt(damageBuffer);
                hp.Damage(damageToApply);
                damageBuffer -= damageToApply;
            }
        }
    }
    

    private void SearchMode()
    {
        spotLight.color = playerColor;

        // play search clip
        PlayClip(searchClip);

        searchTimer += Time.deltaTime * searchSpeed;
        float angle = Mathf.Sin(searchTimer) * searchAngle;
        Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    private void RotateTowards(Vector3 targetPos)
    {
        Vector3 dir = (targetPos - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * rotationSpeed);
    }

    private Transform FindClosestEnemyInRange()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject e in enemies)
        {
            float dist = Vector3.Distance(transform.position, e.transform.position);
            if (dist < detectionRadius && dist < minDist)
            {
                minDist = dist;
                closest = e.transform;
            }
        }
        return closest;
    }

    private void PlayClip(AudioClip clip/*clip it plays*/, bool loop = true)
    {
        if (audioSource.clip == clip && audioSource.isPlaying)
            return; // already playing this clip

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.loop = loop;
        if (clip != null)
            audioSource.Play();
    }

    private void OnEnemyDeath()
    {
        // enemy died → clear tracking
        currentEnemy = null;
        currentEnemyHealth = null;
        currentState = SpotState.Searching;
    }

    /*
    // --- UPGRADE FEATURE (Enable Spotlight) ---
    // Uncomment this when you want the upgrade system working:
    public void UnlockSpotlight()
    {
        spotLight.enabled = true;
    }
    */
}

