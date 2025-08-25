using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    public Slider healthBar;
    public Slider hungerBar;
    public Slider thirstBar;
    public Slider oxygenBar;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = PlayerManager.instance.currentHealthPercentage;
        hungerBar.value = PlayerManager.instance.currentHungerPercentage;
        thirstBar.value = PlayerManager.instance.currentThirstPercentage;
        //oxygenBar.value = PlayerManager.instance.currentOxygenPercentage;
    }
}
