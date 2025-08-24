using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image m_itemSprite;
    [SerializeField] private GameObject m_slotHighlight;
    [SerializeField] private TextMeshProUGUI m_itemCount;
    [SerializeField] private InventorySlot m_assignedInvSlot;

    private Button m_button;

    public InventorySlot AssignedInvSlot => m_assignedInvSlot;
    public InventoryDisplay parentDisplay { get; private set; }

    void Awake()
    {
        ClearSlot();

        m_itemSprite.preserveAspect = true;

        m_button = GetComponent<Button>();
        m_button?.onClick.AddListener(OnUISlotClick);

        parentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    public void Init(InventorySlot _slot)
    {
        m_assignedInvSlot = _slot;
        UpdateUISlot(_slot);
    }

    public void UpdateUISlot(InventorySlot _slot)
    {
        if (_slot.ItemData != null)
        {
            m_itemSprite.sprite = _slot.ItemData.image;
            m_itemSprite.color = Color.white;
        }
        else
        {
            ClearSlot();
        }
        if (_slot.StackSize > 1)
        {
            m_itemCount.text = _slot.StackSize.ToString();
        }
        else
        {
            m_itemCount.text = "";
        }
    }

    public void ToggleHighlight()
    {
        m_slotHighlight.SetActive(!m_slotHighlight.activeInHierarchy);
    }

    public void UpdateUISlot()
    {
        if (m_assignedInvSlot != null)
        {
            UpdateUISlot(m_assignedInvSlot);
        }
    }

    public void ClearSlot()
    {
        m_assignedInvSlot?.ClearSlot();
        m_itemSprite.sprite = null;
        m_itemSprite.color = Color.clear;
        m_itemCount.text = "";
    }

    public void OnUISlotClick()//Access display class
    {
        parentDisplay?.SlotClicked(this);
    }
}
