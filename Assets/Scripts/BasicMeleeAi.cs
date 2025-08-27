using UnityEngine;

public class BasicMeleeAi : MonoBehaviour
{

    public float speed = 5;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(PlayerManager.instance.transform);
        Vector3 dir = PlayerManager.instance.transform.position - transform.position; // not actually direction, but whatever

        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, dir.magnitude))
        {
            if (hit.collider.gameObject != PlayerManager.instance.gameObject)
            {
                transform.position += transform.right * speed * Time.deltaTime;
            }
        }
        transform.position += dir.normalized * speed * Time.deltaTime;


    }
}
