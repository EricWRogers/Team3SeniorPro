using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image m_itemSprite;
    [SerializeField] private TextMeshProUGUI m_itemCount;
    [SerializeField] private InventorySlot m_assignedInvSlot;

    private Button m_button;

    public InventorySlot assignedInvSlot => m_assignedInvSlot;
    public InventoryDisplay parentDisplay { get; private set; }

    void Awake()
    {
        ClearSlot();

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
        if (_slot.itemData != null)
        {
            m_itemSprite.sprite = _slot.itemData.Image;
            m_itemSprite.color = Color.white;
        }
        else
        {
            ClearSlot();
        }
        if (_slot.stackSize > 1)
        {
            m_itemCount.text = _slot.stackSize.ToString();
        }
        else
        {
            m_itemCount.text = "";
        }
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
