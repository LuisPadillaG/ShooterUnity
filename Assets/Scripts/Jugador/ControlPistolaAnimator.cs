using UnityEngine;

public class ControlPistolaAnimator : MonoBehaviour
{
    GameObject Player;
    Animator animator;
    Vector3 posicionPlayerAnterior;
    GameObject objeto_particulas;
    ParticleSystem particulas;
    JugadorScriptActual scriptJugador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.transform.GetChild(0).GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player");
        posicionPlayerAnterior = Player.transform.position;
        objeto_particulas = this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;
        particulas = objeto_particulas.GetComponent<ParticleSystem>();
        scriptJugador = Player.GetComponent<JugadorScriptActual>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)){
            if (scriptJugador.disparoActivo) {
                particulas.Play();
            }
        }
        if (Player.transform.position.x == posicionPlayerAnterior.x || Player.transform.position.z == posicionPlayerAnterior.z)
        {
            animator.SetInteger("EstaCaminando", 0);
        }
        else
        {
            animator.SetInteger("EstaCaminando", 1);
        }
        posicionPlayerAnterior = Player.transform.position;
    }
}
