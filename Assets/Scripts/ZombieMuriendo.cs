using UnityEngine;

public class ZombieMuriendo : MonoBehaviour
{
    float coolDown; 

    void Start()
    {
        coolDown = 4f; 
    }
    void Update()
    {
        coolDown -= Time.deltaTime;
        if (coolDown < 0) {
            Destroy(this.gameObject);
        }
    } 
}
