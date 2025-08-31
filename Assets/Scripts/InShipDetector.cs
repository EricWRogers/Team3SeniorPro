using UnityEngine;

public class InShipDetector : MonoBehaviour
{

    public PlayerMovement pm;

    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        pm.insideShip = true;
    }

    void OnTriggerExit(Collider other)
    {
        pm.insideShip = false;
    }
}
