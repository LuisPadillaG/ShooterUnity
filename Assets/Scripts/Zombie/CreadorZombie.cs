using UnityEngine;

public class CreadorZombie : MonoBehaviour
{
    float contador;
    public GameObject prefabZombie;
    DatosJugador datosJugador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        contador = 0;
        datosJugador = GameObject.Find("DatosJuego").GetComponent<DatosJugador>();
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if (contador > 6 && datosJugador.zombiesInGame < 25 && !datosJugador.cambiandoLaRonda)
        {
            Instantiate(prefabZombie, this.transform.position, Quaternion.identity);
            contador = 0;
        }
    }
}
