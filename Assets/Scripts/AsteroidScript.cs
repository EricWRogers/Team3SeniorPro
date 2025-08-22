using UnityEngine;

public class AsteroidScript : MonoBehaviour
{

    public GameObject resourcePrefab;
    public int resourceCount = 3;
    public float health = 10f;
    public void TakeDamage(float damage){
        health -= damage;
        if(health <= 0){
            OnDestroy();
        }
    }
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    private void OnDestroy() {
        for(int i = 0; i < resourceCount; i++){
            
            Instantiate(resourcePrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }    
}
