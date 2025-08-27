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
        PlayerManager.instance.Eat(hungerPercentRestored);
        PlayerManager.instance.Drink(thirstPercentRestored );
        PlayerManager.instance.OxygenFill(oxygenPercentRestored);
        PlayerManager.instance.Heal(healthPercentRestored);

    }
}
