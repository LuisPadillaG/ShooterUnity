using UnityEngine;

public class ZombieMuriendo : MonoBehaviour
{
    float coolDown; 
    GameObject collider;

    void Start()
    {
        coolDown = 4f;
        collider = this.transform.GetChild(1).gameObject;
    }
    void Update()
    {
        coolDown -= Time.deltaTime;
        if (coolDown < 0) {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    { 
        /*if (other.gameObject.tag == "Jugador") { 
            Destroy(this.gameObject);  
        }*/
    }
}
