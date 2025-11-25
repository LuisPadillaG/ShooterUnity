using System.Collections.Generic;
using UnityEngine;

public class DatosJugador : MonoBehaviour
{
    public int vida = 100;
    public List<I_Armas> armasJugador;
    public int armaActual;
    public int RondaActual = 1;
    public int kills;
    public int puntos = 500;
    public int headshot_acertados = 0;
    public int velocidadRecargaPorSegundo = 2;
    public bool duplicadoBalasActivas;
    /* perks */
    public float countDownPerkRecarga = 0;
    public float countDownPerkBalas = 0;

    public void Start()
    {
        Debug.Log(" ======== Se han creado los datos del jugador. Disfruta tu partida =========");
    }
}
