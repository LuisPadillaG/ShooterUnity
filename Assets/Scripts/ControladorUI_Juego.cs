using UnityEngine;
using TMPro; 
using UnityEngine.UI;

public class ControladorUI_Juego : MonoBehaviour
{
    GameObject DatosJuego;
    DatosJugador datosJugador;
    public TMP_Text textoKills;
    public TMP_Text textoPoints;
    public TMP_Text textoNombreArmaJugador;
    public TMP_Text textoBalasArmaJugador;
    public GameObject PerkHealth;
    public GameObject PerkSpeedCola;
    public GameObject PerkBalas;
    GameObject pressEnter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DatosJuego = GameObject.Find("DatosJuego");
        datosJugador = DatosJuego.GetComponent<DatosJugador>();
        pressEnter = GameObject.FindWithTag("CanvaAction");
        pressEnter.SetActive(false);
        PerkHealth.SetActive(false);
        PerkSpeedCola.SetActive(false);
        PerkBalas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        textoKills.text = "Kills: " + datosJugador.kills;
        textoPoints.text = "Points: " + datosJugador.puntos;
        textoNombreArmaJugador.text = "" + datosJugador.armasJugador[datosJugador.armaActual].Nombre;
        textoBalasArmaJugador.text = "" + datosJugador.armasJugador[datosJugador.armaActual].Balas + "/" + datosJugador.armasJugador[datosJugador.armaActual].MunicionBalas;
    }
    public void ActivarPressEnter()
    {
        pressEnter.SetActive(true);
    }
    public void DesactivarPressEnter()
    {
        pressEnter.SetActive(false);
    }
    public void ActivarPerkHealth()
    {
        PerkHealth.SetActive(true);
    }
    public void DesactivarPerkHealth()
    {
        PerkHealth.SetActive(false);
    }
    public void ActivarPerkSpeedCola()
    {
        PerkSpeedCola.SetActive(true);
    }
    public void DesactivarSpeedCola()
    {
        PerkSpeedCola.SetActive(false);
    }
    public void ActivarPerkBalas()
    {
        PerkBalas.SetActive(true);
    }
    public void DesactivarPerkBalas()
    {
        PerkBalas.SetActive(false);
    }

}
