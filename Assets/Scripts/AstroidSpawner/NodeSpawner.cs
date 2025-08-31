using System.Collections.Generic;
using UnityEngine;

public class NodeSpawner : MonoBehaviour
{
    public bool canSpawnResourceNode = true;
    private List<GameObject> m_resourceNode;
    private int m_randomInt;
    public float waitTime = 3f;
    private float m_currentWait;
    private Vector3 spawnDir;
    private Quaternion surfaceAlignment;

    void Start()
    {
        m_resourceNode = transform.GetComponentInParent<AsteroidScript>().resourceNodes;
        m_currentWait = waitTime;

        spawnDir = transform.position - transform.parent.gameObject.transform.position; 

    }

    void Update()
    {
        if (m_currentWait > 0)
        {
            m_currentWait -= Time.deltaTime;
        }
        else if (canSpawnResourceNode)
        {
            m_randomInt = Random.Range(0, m_resourceNode.Count);

            Instantiate(m_resourceNode[m_randomInt], this.transform);
            canSpawnResourceNode = false;
        }
    }
    public void ResetTimer()
    {
        m_currentWait = waitTime;
        canSpawnResourceNode = true;
    }
}
