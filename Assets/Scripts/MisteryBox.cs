using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class MisteryBox : MonoBehaviour
{
    ControladorUI_Juego controladorUI;
    GameObject DatosJuego;
    DatosJugador datosJugador;
    List<I_Armas> armaParaEnviar;
    List<I_Armas> TodasLasArmasEnElJuegoProgramadas;
    JugadorScriptActual scriptJugador;
    AudioSource audio_PuedesComprar, audio_Compraste;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        armaParaEnviar = new List<I_Armas>();
        TodasLasArmasEnElJuegoProgramadas = new List<I_Armas>();
        TodasLasArmasEnElJuegoProgramadas.Add(new M1911()); // Pistola
        TodasLasArmasEnElJuegoProgramadas.Add(new B23R()); // Pistola
        TodasLasArmasEnElJuegoProgramadas.Add(new Remington870()); // Rifle
        TodasLasArmasEnElJuegoProgramadas.Add(new SPAS12()); // Rifle
        TodasLasArmasEnElJuegoProgramadas.Add(new MP5()); // Pistola
        TodasLasArmasEnElJuegoProgramadas.Add(new Uzi()); // Pistola
        TodasLasArmasEnElJuegoProgramadas.Add(new M16()); // Rifle
        TodasLasArmasEnElJuegoProgramadas.Add(new AK47()); // Rifle 
        DatosJuego = GameObject.Find("DatosJuego");
        scriptJugador = GameObject.FindWithTag("Player").GetComponent<JugadorScriptActual>();
        datosJugador = DatosJuego.GetComponent<DatosJugador>();
        controladorUI = GameObject.Find("ManejadorUIGameplay").GetComponent<ControladorUI_Juego>();
        audio_PuedesComprar = this.transform.GetChild(2).GetComponent<AudioSource>();
        audio_Compraste = this.transform.GetChild(3).GetComponent<AudioSource>();
    }

    // Update is called once per frame 
    public void SeleccionarArmaAEnviar()
    {
        /*Debug.Log(datosJugador.armasJugador[datosJugador.armaActual]);
        Debug.Log(datosJugador.armasJugador.Count);*/
        int rnd; 
        do
        {
            rnd = Random.Range(0, TodasLasArmasEnElJuegoProgramadas.Count);
            bool repetida = false;
            foreach (var arma in datosJugador.armasJugador)
            {
                if (arma.ID == rnd)
                {
                    repetida = true;
                    break;
                }
            } 
            if (!repetida)
                break; 
        } while (true); 
        I_Armas armaEncontrada = TodasLasArmasEnElJuegoProgramadas.Find(a => a.ID == rnd); 
        //omg, si pude hacer un .find y un do while en el videojuego, que emoción.
        scriptJugador.CompraNuevaArma(armaEncontrada);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            audio_PuedesComprar.Play();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            controladorUI.ActivarPressEnter(950);
            if (Input.GetKeyDown(KeyCode.C) && datosJugador.puntos >= 950)
            {
                SeleccionarArmaAEnviar();
                audio_Compraste.Play();
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