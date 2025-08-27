using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    public StatCircleDisplay healthBar;
    public StatCircleDisplay hungerBar;
    public StatCircleDisplay thirstBar;
    public StatCircleDisplay oxygenBar;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.curStat = PlayerManager.instance.currentHealthPercentage;
        hungerBar.curStat = PlayerManager.instance.currentHungerPercentage;
        thirstBar.curStat = PlayerManager.instance.currentThirstPercentage;
        oxygenBar.curStat = PlayerManager.instance.currentOxygenPercentage;
    }
}
