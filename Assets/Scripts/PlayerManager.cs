using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;
    public UnityEvent PlayerDeath;

    [Header("Health")]
    [SerializeField] private float maxHealth = 100;
    private float currentHealth;
    public float currentHealthPercentage => currentHealth / maxHealth;


    [Header("Hunger")]
    [SerializeField] private float maxHunger = 100;
    [SerializeField] private float hungerDecayRate = 0.3f;
    private float currentHunger;
    public float currentHungerPercentage => currentHunger / maxHunger;


    [Header("Thirst")]
    [SerializeField] private float maxThirst = 100;
    [SerializeField] private float thirstDecayRate = 0.3f;
    private float currentThirst;
    public float currentThirstPercentage => currentThirst / maxThirst;


    [Header("Oxygen")]
    [SerializeField] private float maxOxygen = 100;
    [SerializeField] private float oxygenDecayRate = 0.5f;
    public bool usingOxygen = false;
    private float currentOxygen;
    public float currentOxygenPercentage => currentOxygen / maxOxygen;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        PlayerDeath.AddListener(Death);


    }

    void Update()
    {

        if(usingOxygen)
        {
            currentOxygen -= oxygenDecayRate * Time.deltaTime;
            if (currentOxygen <= 0)
            {
                TakeDamage(1f);
                currentOxygen = 0;
            }
        }

        currentHunger -= hungerDecayRate * Time.deltaTime;
        currentThirst -= thirstDecayRate * Time.deltaTime;

        if (currentHunger <= 0 || currentThirst <= 0)
        {
            TakeDamage(1f);
            currentHunger = 0;
            currentThirst = 0;
        }
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= (int)damage;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Eat(float amount)
    {
        currentHunger += amount;
        if (currentHunger > maxHunger)
        {
            currentHunger = maxHunger;
        }
    }

    public void Drink(float amount)
    {
        currentThirst += amount;
        if (currentThirst > maxThirst)
        {
            currentThirst = maxThirst;
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



