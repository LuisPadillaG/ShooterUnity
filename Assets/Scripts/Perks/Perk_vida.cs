using UnityEngine;

public class Perk_vida : MonoBehaviour
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
        if(datosJugador.vida < 100)
        {
            controladorUI.DesactivarPerkHealth();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            controladorUI.ActivarPressEnter();
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (datosJugador.puntos >= 2500 && datosJugador.vida != 200)
                {
                    particulas.Play();
                    datosJugador.puntos -= 2500;
                    buyPerk.Play();
                    datosJugador.vida = 200;
                    musica.Play();
                    controladorUI.ActivarPerkHealth();
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
