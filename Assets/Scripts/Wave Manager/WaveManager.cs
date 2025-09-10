using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SuperPupSystems.Helper;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    public float timeBetweenWaves = 180f; // 3 minutes wave increments
    public int baseEnemyCount = 5;        // starting enemies in wave 1
    public int enemiesPerWaveIncrease = 2;
    private int currentWave = 0;

    [Header("References")]
    public TextMeshProUGUI timerText; // TMP text
    public Timer waveTimer;           // reference to Timer
    public MobSpawner mobSpawner;     // reference to MobSpawner

    private bool waveActive = false;
    private List<GameObject> activeWaveEnemies = new List<GameObject>();

    void Start()
    {
        // Hook timer timeout event
        waveTimer.timeout.AddListener(StartWave);

        // Start first countdown
        waveTimer.StartTimer(timeBetweenWaves);
    }

    void Update()
    {
        if (!waveActive)
        {
            UpdateTimerUI(waveTimer.timeLeft);
        }
        else
        {
            // During wave, keep timer at 00:00
            UpdateTimerUI(0);

            //Check only wave enemies, not passives
            activeWaveEnemies.RemoveAll(e => e == null); // clean up destroyed enemies

            if (activeWaveEnemies.Count == 0)
            {
                EndWave();
            }
        }
    }

    void StartWave()
    {
        waveActive = true;
        currentWave++;

        int enemyCount = baseEnemyCount + (currentWave - 1) * enemiesPerWaveIncrease;

        activeWaveEnemies.Clear(); // reset list

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = mobSpawner.SpawnEnemyMobForWave(); // spawn wave enemy
            if (enemy != null) activeWaveEnemies.Add(enemy);
        }
    }

    void EndWave()
    {
        waveActive = false;

        // Allow passive enemy spawning only after wave 1 ends
        if (currentWave == 1)
        {
            mobSpawner.EnablePassiveEnemySpawning();
        }

        // Restart timer for next wave
        waveTimer.StartTimer(timeBetweenWaves);
    }

    void UpdateTimerUI(float timeLeft)
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
