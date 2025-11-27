using System.Collections.Generic;
using UnityEngine;

public class DatosParaEnviarAGameOver : MonoBehaviour
{
    public static int vida;
    public static List<I_Armas> armasJugador;
    public static int armaActual;
    public static int RondaActual;
    public static int kills;
    public static int puntos;
    public static int headshot_acertados;
    public static int zombiesInGame;
    public static int velocidadRecargaPorSegundo;
    public static bool duplicadoBalasActivas;
    public static bool cambiandoLaRonda;
    public static float countDownPerkRecarga;
    public static float countDownPerkBalas;
    public static void GuardarParaGameOver(DatosJugador datos)
    {
        vida = datos.vida;
        armasJugador = new List<I_Armas>(datos.armasJugador);
        armaActual = datos.armaActual;
        RondaActual = datos.RondaActual;
        kills = datos.kills;
        puntos = datos.puntos;
        headshot_acertados = datos.headshot_acertados;
        zombiesInGame = datos.zombiesInGame;
        velocidadRecargaPorSegundo = datos.velocidadRecargaPorSegundo;
        duplicadoBalasActivas = datos.duplicadoBalasActivas;
        cambiandoLaRonda = datos.cambiandoLaRonda;
        countDownPerkRecarga = datos.countDownPerkRecarga;
        countDownPerkBalas = datos.countDownPerkBalas;
    }
}
