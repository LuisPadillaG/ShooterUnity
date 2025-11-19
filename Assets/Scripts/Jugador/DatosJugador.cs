using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DatosJugador : MonoBehaviour
{
    public int vida = 100;
    public List<I_Armas> armasJugador;
    public int armaActual;
    public int RondaActual = 1;
    public int kills;
    public void Start()
    {
        
        Debug.Log("nose un mensaje");
    }
}
