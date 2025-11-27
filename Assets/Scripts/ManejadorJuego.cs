using UnityEngine;

public class ManejadorJuego : MonoBehaviour
{
    DatosJugador datosJugador;
    int killsDeRonda;
    int numerodeKillAnteriorJugador;
    int rondaAnterior;
    int zombiesMaximsDeLaRonda;
    ControladorUI_Juego controlador;
    float contadorParaAsegurarPrimerFrame;
    AudioSource audioRoundChange;
    public GameObject prefabFrankReferencia;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        datosJugador = GameObject.Find("DatosJuego").GetComponent<DatosJugador>();
        numerodeKillAnteriorJugador = 0;
        rondaAnterior = datosJugador.RondaActual;
        CalcularZombiesMaximosPorRonda();
        killsDeRonda = datosJugador.kills;
        numerodeKillAnteriorJugador = datosJugador.kills;
        datosJugador.cambiandoLaRonda = false;
        controlador = this.transform.GetComponent<ControladorUI_Juego>();
        contadorParaAsegurarPrimerFrame = 0;
        audioRoundChange = this.transform.GetChild(0).GetChild(0).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (!datosJugador.cambiandoLaRonda)
        {
            if (numerodeKillAnteriorJugador < datosJugador.kills)
            {
                killsDeRonda++;
            }
            numerodeKillAnteriorJugador = datosJugador.kills;
            if (killsDeRonda >= zombiesMaximsDeLaRonda)
            {
                datosJugador.RondaActual++; //aqui en teoria que no lo probe, ya deberia funcionar directo, pero no hay una trancion
                                            // lo logico seria añadir segunderos para la transición entre rondas. Otra variable en datosJugador
            }
            if (rondaAnterior != datosJugador.RondaActual)
            {
                audioRoundChange.Play();
                datosJugador.cambiandoLaRonda = true;
                contadorParaAsegurarPrimerFrame = 0;
                rondaAnterior = datosJugador.RondaActual;
                killsDeRonda = 0;
                CalcularZombiesMaximosPorRonda();
            }
        }
        else
        {
            controlador.CambioRonda(datosJugador.RondaActual);
            if(contadorParaAsegurarPrimerFrame == 0)
            {
                CalcularZombiesMaximosPorRonda();
                datosJugador.puntos += 2000;
                contadorParaAsegurarPrimerFrame+=1;
                if(datosJugador.RondaActual == 10)
                {
                    Instantiate(prefabFrankReferencia, Vector3.zero, Quaternion.identity);
                }
            }
            else
            {

            }
        }
    }
    public void CalcularZombiesMaximosPorRonda()
    {
        switch (datosJugador.RondaActual)
        {
            case > 100:
                zombiesMaximsDeLaRonda = 885;
                break;
            case > 50:
                zombiesMaximsDeLaRonda = 243;
                break;
            case > 30:
                zombiesMaximsDeLaRonda = 105;
                break;
            case > 20:
                zombiesMaximsDeLaRonda = 60;
                break;
            case > 10:
                zombiesMaximsDeLaRonda = 33;
                break;
            case <= 10:
                zombiesMaximsDeLaRonda = 20;
                break;
        }
    }
}
