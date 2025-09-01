using System.Collections.Generic;
using UnityEngine;

public class BasicMeleeAi : MonoBehaviour
{

    public float speed = 5;
    public List<GameObject> itemsToDrop;
    public List<string> tags;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(PlayerManager.instance.transform);
        Vector3 dir = PlayerManager.instance.transform.position - transform.position; // not actually direction, but whatever
        Physics.Raycast(transform.position, dir, out RaycastHit hit, dir.magnitude);
        if (!tags.Contains(hit.transform.tag))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else
        {
            if (dir.magnitude > 2f)
            {
                transform.position += dir.normalized * speed * Time.deltaTime;
            }
            else
            {
                PlayerManager.instance.TakeDamage(2f * Time.deltaTime);

            }
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
