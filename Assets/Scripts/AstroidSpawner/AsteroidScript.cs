using UnityEngine;

public class AsteroidScript : MonoBehaviour
{

    public GameObject resourcePrefab;
    public int resourceCount = 3;
    public float health = 10f;
    public void TakeDamage(float damage){
        health -= damage;
        if(health <= 0){
            OnDeath();
        }
    }
    void Start()
    {
        if(!gameObject.GetComponent<Rigidbody>()){
            gameObject.AddComponent<Rigidbody>();
        }
        gameObject.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * Random.Range(100f, 500f), ForceMode.Impulse);
    }

   
    void Update()
    {
        
    }

    private void OnDeath() {
        for(int i = 0; i < resourceCount; i++){
            
            Instantiate(resourcePrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }    
}
