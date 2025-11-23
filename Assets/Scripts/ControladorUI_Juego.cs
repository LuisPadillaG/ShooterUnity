using UnityEngine;
using TMPro; 
using UnityEngine.UI;

public class ControladorUI_Juego : MonoBehaviour
{
    GameObject DatosJuego;
    DatosJugador datosJugador;
    public TMP_Text textoKills; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DatosJuego = GameObject.Find("DatosJuego");
        datosJugador = DatosJuego.GetComponent<DatosJugador>();
    }

    // Update is called once per frame
    void Update()
    {
        textoKills.text = "Kills: " + datosJugador.kills;
    }
}
