using UnityEngine;

public class Perk_TapRoot : MonoBehaviour
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
        if (datosJugador.duplicadoBalasActivas)
        {
            datosJugador.countDownPerkBalas -= Time.deltaTime;
            // este codigo esta dlv, pero equis.
            if (datosJugador.countDownPerkBalas <= 10)
            {
                controladorUI.DesactivarPerkBalas();
            }
            if (datosJugador.countDownPerkBalas <= 9)
            {
                controladorUI.ActivarPerkBalas();
            }
            if (datosJugador.countDownPerkBalas <= 8)
            {
                controladorUI.DesactivarPerkBalas();
            }
            if (datosJugador.countDownPerkBalas <= 7)
            {
                controladorUI.ActivarPerkBalas();
            }
            if (datosJugador.countDownPerkBalas <= 6)
            {
                controladorUI.DesactivarPerkBalas();
            }
            if (datosJugador.countDownPerkBalas <= 5)
            {
                controladorUI.ActivarPerkBalas();
            }
            if (datosJugador.countDownPerkBalas <= 3f)
            {
                controladorUI.DesactivarPerkBalas();
            }
            if (datosJugador.countDownPerkBalas <= 1.5f)
            {
                controladorUI.ActivarPerkBalas();
            }
            if (datosJugador.countDownPerkBalas <= 0)
            {
                datosJugador.countDownPerkBalas = 0;
                datosJugador.duplicadoBalasActivas = false;
                controladorUI.DesactivarPerkBalas();
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
                if (datosJugador.puntos >= 2000 && !datosJugador.duplicadoBalasActivas)
                {
                    particulas.Play();
                    datosJugador.puntos -= 2000;
                    buyPerk.Play();
                    datosJugador.duplicadoBalasActivas = true;
                    datosJugador.countDownPerkBalas = 30;
                    musica.Play();
                    controladorUI.ActivarPerkBalas();
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
