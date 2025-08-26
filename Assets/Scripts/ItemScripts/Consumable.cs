using Unity.VisualScripting;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public float hungerRestored;
    public float thirstRestored;
    public float healthRestored;
    public float oxygenRestored;

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ConsumeItem()
    {
        PlayerManager player = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        player.Eat(hungerRestored);
        player.Drink(thirstRestored);
        player.OxygenFill(oxygenRestored);
        player.Heal(healthRestored);

    }
}
