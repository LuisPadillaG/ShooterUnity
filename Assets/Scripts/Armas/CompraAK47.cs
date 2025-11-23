using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CompraAK47 : MonoBehaviour
{
    ControladorUI_Juego controladorUI;
    GameObject DatosJuego;
    DatosJugador datosJugador;
    JugadorScriptActual scriptJugador;
    List<I_Armas> armaParaEnviar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controladorUI = GameObject.Find("ManejadorUIGameplay").GetComponent<ControladorUI_Juego>();
        DatosJuego = GameObject.Find("DatosJuego");
        scriptJugador = GameObject.FindWithTag("Player").GetComponent<JugadorScriptActual>();
        datosJugador = DatosJuego.GetComponent<DatosJugador>();
        armaParaEnviar = new List<I_Armas>();
        armaParaEnviar.Add(new AK47());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            controladorUI.ActivarPressEnter();
            if(datosJugador.puntos >= 500 && Input.GetKeyDown(KeyCode.Return))
            {
                //scriptJugador.CompraNuevaArma(armaParaEnviar[0]);
                controladorUI.DesactivarPressEnter();
            }
        }
        else
        {
            controladorUI.DesactivarPressEnter();
        }
    }

}
