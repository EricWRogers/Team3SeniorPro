using UnityEngine;

public class HotBarDisplay : StaticInventoryDisplay
{
    private int m_maxIndexSize = 9;
    private int m_currentIndex = 0;

    private void Awake()
    {

    }

    protected override void Start()
    {
        base.Start();

        m_currentIndex = 0;
        m_maxIndexSize = m_slots.Length - 1;
        m_slots[m_currentIndex].ToggleHighlight();
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            ChangeIndex(1);
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            ChangeIndex(-1);
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

    private void UseItem()
    {
        if (m_slots[m_currentIndex].AssignedInvSlot.ItemData != null)
        {
            m_slots[m_currentIndex].AssignedInvSlot.ItemData.UseItem();
        }
    }

    private void ChangeIndex(int _direction)
    {
        m_slots[m_currentIndex].ToggleHighlight();
        m_currentIndex += _direction;
        if (m_currentIndex > m_maxIndexSize)
        {
            m_currentIndex = 0;
        }
        if (m_currentIndex < 0)
        {
            m_currentIndex = m_maxIndexSize;
        }
        m_slots[m_currentIndex].ToggleHighlight();
    }

    private void SetSlot(int newIndex)
    {
        m_slots[m_currentIndex].ToggleHighlight();

        if (newIndex < 0) newIndex = 0;
        if (newIndex > m_maxIndexSize) newIndex = m_maxIndexSize;
        m_currentIndex = newIndex;

        m_slots[m_currentIndex].ToggleHighlight();
    }
}
