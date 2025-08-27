using Unity.VisualScripting;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public float hungerPercentRestored;
    public float thirstPercentRestored;
    public float healthPercentRestored;
    public float oxygenPercentRestored;

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ConsumeItem()
    {
        PlayerManager player = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        player.Eat(hungerPercentRestored / 100);
        player.Drink(thirstPercentRestored / 100);
        player.OxygenFill(oxygenPercentRestored / 100);
        player.Heal(healthPercentRestored / 100);

    }
}
