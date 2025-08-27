using Unity.Mathematics;
using UnityEngine;

public class HotBarDisplay : StaticInventoryDisplay
{
    private int m_maxIndexSize = 9;
    protected int m_currentIndex = 0;
    private int m_currentItemID;

    public GameObject hand;

    private GameObject m_player;

    private void Awake()
    {

    }

    protected override void Start()
    {
        base.Start();
        m_player = GameObject.FindWithTag("Player");
        m_currentIndex = 0;
        m_maxIndexSize = m_slots.Length - 1;
        m_currentItemID = -1;
        m_slots[m_currentIndex].ToggleHighlight();
    }

    private void Update()
    {
        if (m_slots[m_currentIndex].AssignedInvSlot.ItemData != null)
        {
            if (m_currentItemID != m_slots[m_currentIndex].AssignedInvSlot.ItemData.id)
            {
                HoldItem();
                m_currentItemID = m_slots[m_currentIndex].AssignedInvSlot.ItemData.id;
            }
        }

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
        if (m_slots[m_currentIndex].AssignedInvSlot.ItemData == null)
        {
            EmptyHand();
        }
    }

    public void UseItem()
    {
        if (m_currentItemID != -1)
        {
            if (m_slots[m_currentIndex].AssignedInvSlot.ItemData.itemTypes == ItemTypes.ItemUseType.Consumable)
            {
                m_slots[m_currentIndex].AssignedInvSlot.ItemData.itemPrefab.GetComponent<Consumable>().ConsumeItem();
                if (m_slots[m_currentIndex].AssignedInvSlot.StackSize > 1)
            {
                int itemLeft = m_slots[m_currentIndex].AssignedInvSlot.StackSize - 1;
                m_slots[m_currentIndex].AssignedInvSlot.UpdateInventorySlot(m_slots[m_currentIndex].AssignedInvSlot.ItemData, itemLeft);
                PlayerInventoryHolder.OnPlayerInventoryChanged.Invoke();
            }
            else
            {
                m_slots[m_currentIndex].ClearSlot();
                PlayerInventoryHolder.OnPlayerInventoryChanged.Invoke();
            }

            }
            if (m_slots[m_currentIndex].AssignedInvSlot.ItemData.itemTypes == ItemTypes.ItemUseType.Placeable)
            {
                //m_slots[m_currentIndex].AssignedInvSlot.ItemData.itemPrefab.GetComponent<Placeable>().PlaceItem();
                if (m_slots[m_currentIndex].AssignedInvSlot.StackSize > 1)
                {
                    int itemLeft = m_slots[m_currentIndex].AssignedInvSlot.StackSize - 1;
                    m_slots[m_currentIndex].AssignedInvSlot.UpdateInventorySlot(m_slots[m_currentIndex].AssignedInvSlot.ItemData, itemLeft);
                    PlayerInventoryHolder.OnPlayerInventoryChanged.Invoke();
                }
                else
                {
                    m_slots[m_currentIndex].ClearSlot();
                    PlayerInventoryHolder.OnPlayerInventoryChanged.Invoke();
                }
            }
            if (m_slots[m_currentIndex].AssignedInvSlot.ItemData.itemTypes == ItemTypes.ItemUseType.Throwable)
            {
                //m_slots[m_currentIndex].AssignedInvSlot.ItemData.itemPrefab.GetComponent<Throwable>().ThrowItem();
                if (m_slots[m_currentIndex].AssignedInvSlot.StackSize > 1)
                {
                    int itemLeft = m_slots[m_currentIndex].AssignedInvSlot.StackSize - 1;
                    m_slots[m_currentIndex].AssignedInvSlot.UpdateInventorySlot(m_slots[m_currentIndex].AssignedInvSlot.ItemData, itemLeft);
                    PlayerInventoryHolder.OnPlayerInventoryChanged.Invoke();
                }
                else
                {
                    m_slots[m_currentIndex].ClearSlot();
                    PlayerInventoryHolder.OnPlayerInventoryChanged.Invoke();
                }
            }
            
        }
    }

    private void HoldItem()
    {
        GameObject item = Instantiate(m_slots[m_currentIndex].AssignedInvSlot.ItemData.itemPrefab, hand.transform.position, hand.transform.rotation, hand.transform);
        item.GetComponent<Collider>().enabled = false;
        item.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void ChangeIndex(int _direction)
    {
        EmptyHand();
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
        if (m_slots[m_currentIndex].AssignedInvSlot.ItemData != null)
        {
            HoldItem();
        }
    }

    private void SetSlot(int newIndex)
    {
        EmptyHand();
        m_slots[m_currentIndex].ToggleHighlight();

        if (newIndex < 0) newIndex = 0;
        if (newIndex > m_maxIndexSize) newIndex = m_maxIndexSize;
        m_currentIndex = newIndex;

        m_slots[m_currentIndex].ToggleHighlight();
        if (m_slots[m_currentIndex].AssignedInvSlot.ItemData != null)
        {
            HoldItem();
        }
    }

    public void EmptyHand()
    {
        if (hand.transform.childCount > 0)
        {
            for (int x = 0; x < hand.transform.childCount; x++)
            {
                Destroy(hand.transform.GetChild(x).gameObject);
            }
        }
            
    }
}
