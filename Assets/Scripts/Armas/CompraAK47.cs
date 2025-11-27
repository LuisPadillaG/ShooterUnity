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
    public string armaComprarHierarchy; //debe ser todo junto y en minúsculas. Ejemplo: ak47
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controladorUI = GameObject.Find("ManejadorUIGameplay").GetComponent<ControladorUI_Juego>();
        DatosJuego = GameObject.Find("DatosJuego");
        scriptJugador = GameObject.FindWithTag("Player").GetComponent<JugadorScriptActual>();
        datosJugador = DatosJuego.GetComponent<DatosJugador>();
        armaParaEnviar = new List<I_Armas>();
        switch (armaComprarHierarchy)
        {
            case "ak47":
                armaParaEnviar.Add(new AK47()); // Rifle
                break;
            case "b23r":
                armaParaEnviar.Add(new B23R()); // Pistola
                break;
            case "remington":
                armaParaEnviar.Add(new Remington870()); // Rifle
                break;
            case "spas":
                armaParaEnviar.Add(new SPAS12()); // Rifle
                break;
            case "mp5":
                armaParaEnviar.Add(new MP5()); // Pistola
                break;
            case "uzi":
                armaParaEnviar.Add(new Uzi()); // Pistola
                break;
            case "m16":
                armaParaEnviar.Add(new M16()); // Rifle
                break;
            default:
                armaParaEnviar.Add(new M1911());
            break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            controladorUI.ActivarPressEnter(armaParaEnviar[0].CostoPuntos);
            if(datosJugador.puntos >= armaParaEnviar[0].CostoPuntos && Input.GetKeyDown(KeyCode.C))
            {
                datosJugador.puntos -= armaParaEnviar[0].CostoPuntos;
                Debug.Log("Intentando comprar un arma...");
                scriptJugador.CompraNuevaArma(armaParaEnviar[0]);
                controladorUI.DesactivarPressEnter();
                
            }
        } 
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            controladorUI.DesactivarPressEnter();
        }
    }

}
