using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;
    public UnityEvent PlayerDeath;

    [Header("Health")]
    [SerializeField] private float m_maxHealth = 100;
    private float m_currentHealth;
    public float currentHealthPercentage => m_currentHealth / m_maxHealth;


    [Header("Hunger")]
    [SerializeField] private float m_maxHunger = 100;
    [SerializeField] private float m_hungerDecayRate = 0.3f;
    private float m_currentHunger;
    public float currentHungerPercentage => m_currentHunger / m_maxHunger;


    [Header("Thirst")]
    [SerializeField] private float m_maxThirst = 100;
    [SerializeField] private float m_thristDecayRate = 0.3f;
    private float m_currentThirst;
    public float currentThirstPercentage => m_currentThirst / m_maxThirst;


    [Header("Oxygen")]
    [SerializeField] private float maxOxygen = 100;
    [SerializeField] private float oxygenDecayRate = 0.5f;
    public bool usingOxygen = true;
    private float currentOxygen;
    public float currentOxygenPercentage => currentOxygen / maxOxygen;

    [Header("General")]
    public GameObject hand;
    public ItemData heldItem;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        PlayerDeath.AddListener(Death);

        m_currentHealth = m_maxHealth;
        m_currentHunger = m_maxHunger;
        m_currentThirst = m_maxThirst;
        currentOxygen = maxOxygen;

    }

    void Update()
    {
        
        if (hand.transform.childCount > 0)
            heldItem = hand.transform.GetChild(0).GetComponent<ItemScript>().itemData;
        if (usingOxygen)
        {
            currentOxygen -= oxygenDecayRate * Time.deltaTime;
            if (currentOxygen <= 0)
            {
                TakeDamage(1f);
                currentOxygen = 0;
            }
        }

        m_currentHunger -= m_hungerDecayRate * Time.deltaTime;
        m_currentThirst -= m_thristDecayRate * Time.deltaTime;

        if (m_currentHunger <= 0 || m_currentThirst <= 0)
        {
            TakeDamage(1f);
            m_currentHunger = 0;
            m_currentThirst = 0;
        }
        

    }


    public void TakeDamage(float damage)
    {
        m_currentHealth -= (int)damage;
        if (m_currentHealth <= 0)
        {
            Death();
        }
    }

    public void Heal(float amount)
    {
        m_currentHealth += amount;
        if (m_currentHealth > m_maxHealth)
        {
            m_currentHealth = m_maxHealth;
        }
    }

    public void Eat(float amount)
    {
        m_currentHunger += amount;
        if (m_currentHunger > m_maxHunger)
        {
            m_currentHunger = m_maxHunger;
        }
    }

    public void Drink(float amount)
    {
        m_currentThirst += amount;
        if (m_currentThirst > m_maxThirst)
        {
            m_currentThirst = m_maxThirst;
        }
    }

    public void OxygenFill(float amount)
    {
        currentOxygen += amount;
        if (currentOxygen > maxOxygen)
        {
            currentOxygen = maxOxygen;
        }
    }

    public void Death()
    {
        Debug.Log("Player has died.");
    }

}



