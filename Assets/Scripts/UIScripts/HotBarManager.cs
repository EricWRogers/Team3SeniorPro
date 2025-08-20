using System.Collections.Generic;
using UnityEngine;

public class HotBarManager : MonoBehaviour
{
    public Transform hotBarGameObj;
    public List<Transform> m_hotBarSlots;

    void awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int x = 0; x < hotBarGameObj.childCount; x++)
        {
            Debug.Log(x);
            m_hotBarSlots.Add(hotBarGameObj.GetChild(x));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
