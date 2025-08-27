using UnityEngine;

public class OxygenBubble : MonoBehaviour
{
    public float oxygenFillRate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerManager.instance.usingOxygen = false;
            PlayerManager.instance.OxygenFill(oxygenFillRate);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerManager.instance.usingOxygen = true;
        }
    }
}
