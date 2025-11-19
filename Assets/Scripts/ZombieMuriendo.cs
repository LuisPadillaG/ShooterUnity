using UnityEngine;

public class ZombieMuriendo : MonoBehaviour
{
    float coolDown;
    Material mat;

    void Start()
    {
        coolDown = 2f; // dura dos segundos el fade
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        coolDown -= Time.deltaTime;

    }
}
