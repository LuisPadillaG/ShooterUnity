using System.Collections.Generic;
using UnityEngine;

public class DatosJugador : MonoBehaviour
{
    public int vida = 100;
    public List<I_Armas> armasJugador = new List<I_Armas>();
    public int armaActual;
    public int RondaActual = 1; 
}
