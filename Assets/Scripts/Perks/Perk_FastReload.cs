using UnityEngine;

public class Perk_FastReload : MonoBehaviour
{
    ControladorUI_Juego controladorUI;
    GameObject DatosJuego;
    DatosJugador datosJugador;
    JugadorScriptActual scriptJugador;
    AudioSource buyPerk, nonbuyPerk, musica;
    ParticleSystem particulas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controladorUI = GameObject.Find("ManejadorUIGameplay").GetComponent<ControladorUI_Juego>();
        DatosJuego = GameObject.Find("DatosJuego");
        scriptJugador = GameObject.FindWithTag("Player").GetComponent<JugadorScriptActual>();
        datosJugador = DatosJuego.GetComponent<DatosJugador>();
        buyPerk = this.transform.GetChild(0).GetComponent<AudioSource>();
        nonbuyPerk = this.transform.GetChild(1).GetComponent<AudioSource>();
        musica = this.transform.GetChild(2).GetComponent<AudioSource>();
        particulas = this.transform.GetChild(3).GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(datosJugador.velocidadRecargaPorSegundo == 1)
        {
            datosJugador.countDownPerkRecarga -= Time.deltaTime;
            // este codigo esta dlv, pero equis.
            if(datosJugador.countDownPerkRecarga <= 10)
            {
                controladorUI.DesactivarSpeedCola();
            }
            if (datosJugador.countDownPerkRecarga <= 9)
            {
                controladorUI.ActivarPerkSpeedCola();
            }
            if (datosJugador.countDownPerkRecarga <= 8)
            {
                controladorUI.DesactivarSpeedCola();
            }
            if (datosJugador.countDownPerkRecarga <= 7)
            {
                controladorUI.ActivarPerkSpeedCola();
            }
            if (datosJugador.countDownPerkRecarga <= 6)
            {
                controladorUI.DesactivarSpeedCola();
            }
            if (datosJugador.countDownPerkRecarga <= 5)
            {
                controladorUI.ActivarPerkSpeedCola();
            }
            if (datosJugador.countDownPerkRecarga <= 3.5f)
            {
                controladorUI.DesactivarSpeedCola();
            }
            if (datosJugador.countDownPerkRecarga <= 1)
            {
                controladorUI.ActivarPerkSpeedCola();
            }
            if (datosJugador.countDownPerkRecarga <= 0)
            {
                datosJugador.countDownPerkRecarga = 0;
                datosJugador.velocidadRecargaPorSegundo = 2;
                controladorUI.DesactivarSpeedCola();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            controladorUI.ActivarPressEnter(2000);
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (datosJugador.puntos >= 2000 && datosJugador.velocidadRecargaPorSegundo != 1)
                {
                    particulas.Play();
                    datosJugador.puntos -= 2000;
                    buyPerk.Play();
                    datosJugador.countDownPerkRecarga = 30;
                    datosJugador.velocidadRecargaPorSegundo = 1;
                    musica.Play();
                    controladorUI.ActivarPerkSpeedCola();
                }
                else
                {
                    nonbuyPerk.Play();
                }
            }
            //Debug.Log("El perk vida esta listo para ser comprado");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("El perk vida dejo de estar activo para ser comprado");
            controladorUI.DesactivarPressEnter();
        }
    }
}
