using UnityEngine;

public class ZombieHitBox : MonoBehaviour
{
    GameObject jugador;
    JugadorScriptActual scriptJugador;
    void Start()
    {
        jugador = GameObject.FindWithTag("Player");
        scriptJugador = jugador.GetComponent<JugadorScriptActual>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            scriptJugador.Golpeado();
        }
    }/*
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            scriptJugador.Golpeado();
        }
    }*/
}
