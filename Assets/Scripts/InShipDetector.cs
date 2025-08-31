using UnityEngine;

public class InShipDetector : MonoBehaviour
{

    public PlayerMovement pm;

    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void OnTriggerStay(Collider other)
    {
        pm.insideShip = true;
    }

    void Oit(Collider other)
    {
        pm.insideShip = false;
    }
}
