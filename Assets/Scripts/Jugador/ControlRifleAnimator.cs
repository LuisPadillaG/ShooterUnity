using UnityEngine;

public class ControlRifleAnimator : MonoBehaviour
{
    GameObject DatosJuego;
    DatosJugador datosJugador;
    GameObject Player;
    Animator animator;
    Vector3 posicionPlayerAnterior;
    GameObject objeto_particulas;
    ParticleSystem particulas;
    JugadorScriptActual scriptJugador;
    float contadorInicial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DatosJuego = GameObject.Find("DatosJuego");
        datosJugador = DatosJuego.GetComponent<DatosJugador>();
        animator = this.transform.GetChild(0).GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player");
        posicionPlayerAnterior = Player.transform.position;
        objeto_particulas = this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject;
        particulas = objeto_particulas.GetComponent<ParticleSystem>();
        scriptJugador = Player.GetComponent<JugadorScriptActual>();
        animator.SetInteger("RifleCaminata", 2);
        contadorInicial = 0;

    }

    // Update is called once per frame
    void Update()
    {
        contadorInicial += Time.deltaTime;
        if(contadorInicial >= 1)
        {
            if (scriptJugador.contadorRecarga <= 0)
            {
                if (Input.GetMouseButton(0))
                {
                    if (scriptJugador.disparoActivo && datosJugador.armasJugador[datosJugador.armaActual].Balas > 0)
                    {
                        particulas.Play();
                    }
                }
                if (Player.transform.position.x == posicionPlayerAnterior.x || Player.transform.position.z == posicionPlayerAnterior.z)
                {
                    animator.SetInteger("RifleCaminata", 0);
                }
                else
                {
                    animator.SetInteger("RifleCaminata", 1);
                }
            }
            else
            {
                animator.SetInteger("RifleCaminata", 3);
            }
                
        }
        posicionPlayerAnterior = Player.transform.position;

    }
}
