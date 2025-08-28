using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PassiveAI : MonoBehaviour
{
    public Vector3 rommingArea = new Vector3(30, 10, 30);

    public List<GameObject> itemsToDrop;
    public float speed = 5;
    private bool targetSet = false;
    private Vector3 target;
    void Start()
    {

    }

    void Update()
    {
        if (!targetSet)
        {
            target = transform.position + new Vector3(Random.Range(-rommingArea.x, rommingArea.x), Random.Range(-rommingArea.y, rommingArea.y), Random.Range(-rommingArea.z, rommingArea.z));
            targetSet = true;
            Invoke("ResetTarget", Random.Range(2, 5));
        }

        if (Physics.Raycast(transform.position, (target - transform.position).normalized, out RaycastHit hit, 2f))
        {
            targetSet = false;
        }
        transform.LookAt(target);
        transform.position += transform.forward * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, target) < 1f)
        {
            targetSet = false;
        }
    }


    public void OnDeath()
    {
        if (itemsToDrop.Count > 0)
        {
            int itemIndex = Random.Range(0, itemsToDrop.Count);
            Instantiate(itemsToDrop[itemIndex], transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
