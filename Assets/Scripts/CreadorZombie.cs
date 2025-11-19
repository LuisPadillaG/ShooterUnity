using UnityEngine;

public class CreadorZombie : MonoBehaviour
{
    float contador;
    public GameObject prefabZombie;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        contador = 0;
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if (contador > 3)
        {
            Instantiate(prefabZombie, this.transform.position, Quaternion.identity);
            contador = 0;
        }
    }
}
