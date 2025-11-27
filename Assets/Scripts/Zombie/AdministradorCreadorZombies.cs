using UnityEngine;

public class AdministradorCreadorZombies : MonoBehaviour
{
    GameObject Granero_1erPiso, Granero_2doPiso, Cobertizo, Casa1erPiso, Casa2doPiso, Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Granero_1erPiso = GameObject.Find("Granero_1erPiso");
        Granero_2doPiso = GameObject.Find("Granero_2doPiso");
        Cobertizo = GameObject.Find("Cobertizo");
        Casa1erPiso = GameObject.Find("Casa1erPiso");
        Casa2doPiso = GameObject.Find("Casa2doPiso");
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
