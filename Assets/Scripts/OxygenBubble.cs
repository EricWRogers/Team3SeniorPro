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
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager.instance.usingOxygen = false;
            PlayerManager.instance.OxygenFill(oxygenFillRate);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager.instance.usingOxygen = true;
        }
    }
}
