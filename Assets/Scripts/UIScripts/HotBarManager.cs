using System.Collections.Generic;
using UnityEngine;

public class HotBarManager : MonoBehaviour
{
    public Transform hotBarGameObj;
    public List<HotBarSlot> m_hotBarSlots;
    private int m_currentSlotIndex;

    void Awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_currentSlotIndex = 0;

        for (int x = 0; x < hotBarGameObj.childCount; x++)
        {
            m_hotBarSlots.Add(hotBarGameObj.GetChild(x).GetComponent<HotBarSlot>());
        }
        m_hotBarSlots[m_currentSlotIndex].ToggleSelected(true);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.mouseScrollDelta.y > 0)
        {
            ChangeSlot(1);
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            ChangeSlot(-1);
        }

        //Hotbar input selection
        if (Input.GetKeyDown("1"))
        {
            SetSlot(0);
        }
        if (Input.GetKeyDown("2"))
        {
            SetSlot(1);
        }
        if (Input.GetKeyDown("3"))
        {
            SetSlot(2);
        }
        if (Input.GetKeyDown("4"))
        {
            SetSlot(3);
        }
        if (Input.GetKeyDown("5"))
        {
            SetSlot(4);
        }
        if (Input.GetKeyDown("6"))
        {
            SetSlot(5);
        }
        if (Input.GetKeyDown("7"))
        {
            SetSlot(6);
        }
        if (Input.GetKeyDown("8"))
        {
            SetSlot(7);
        }
        if (Input.GetKeyDown("9"))
        {
            SetSlot(8);
        }
        if (Input.GetKeyDown("0"))
        {
            SetSlot(9);
        }
        
    }

    

    private void ChangeSlot(int _direction)
    {
        m_hotBarSlots[m_currentSlotIndex].ToggleSelected(false);

        m_currentSlotIndex += _direction;
        if (m_currentSlotIndex > 9) m_currentSlotIndex = 0;
        if (m_currentSlotIndex < 0) m_currentSlotIndex = 9;

        m_hotBarSlots[m_currentSlotIndex].ToggleSelected(true);
    }

    private void SetSlot(int newIndex)
    {
        m_hotBarSlots[m_currentSlotIndex].ToggleSelected(false);

        
        if (newIndex < 0) newIndex = 0;
        if (newIndex > 9) newIndex = 9;
        m_currentSlotIndex = newIndex;

        m_hotBarSlots[m_currentSlotIndex].ToggleSelected(true);
    }
}
