using UnityEngine;

public class Zombie : MonoBehaviour
{
    DatosJugador datosJugador;
    GameObject jugador;
    Vector3 rotacion, posicion;
    GameObject modelo, DatosJuego;
    Animator zombieAnimator;
    public float puntosMiZombie;
    float velocidadZombie;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // variables
        velocidadZombie = 1.3f;
        DatosJuego = GameObject.Find("DatosJuego");
        datosJugador = DatosJuego.GetComponent<DatosJugador>();
        modelo = this.transform.GetChild(0).gameObject;
        zombieAnimator = modelo.GetComponent<Animator>();
        jugador = GameObject.FindGameObjectWithTag("Player");
        rotacion = Vector3.zero;
        posicion = this.transform.position;
        puntosMiZombie = 50;
        // calculo de datos MiZomnbie
        for (int i = 0; i < datosJugador.RondaActual; i++) {
            Debug.Log("Zombie de la ronda "+ i);
            puntosMiZombie += 100;
            if(i > 5)
            {
                velocidadZombie = 1.6f;
            }
            if(i == 10) 
                break;
        }
        if(datosJugador.RondaActual > 9)
        {
            velocidadZombie = 3f;
            puntosMiZombie += + (puntosMiZombie * 0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(rotacion);
        rotacion.y = HerramientasGenericas.CalcularAnguloBidimensional(new Vector2(this.transform.position.x, this.transform.position.z), new Vector2(jugador.transform.position.x, jugador.transform.position.z));

        posicion.x += Mathf.Cos(rotacion.y * Mathf.Deg2Rad) * Time.deltaTime * velocidadZombie;
        posicion.z -= Mathf.Sin(rotacion.y * Mathf.Deg2Rad) * Time.deltaTime * velocidadZombie;
        this.transform.position = posicion;
        // Animaciones
        if(velocidadZombie == 1.3f || velocidadZombie == 1.6f) //caminando, caminando rapido
        {
            zombieAnimator.SetInteger("EstadoZombie", 0);
        }
        else
        {
            zombieAnimator.SetInteger("EstadoZombie", 1);
        }
        // lo que sigue del zombie
        /*
        1. Golpe de brazo + animación 
        2. Recibir balazo
         */
    }
    public void RecibirDisparo(float danoDisparo)
    {
        puntosMiZombie -= danoDisparo;
        zombieAnimator.SetInteger("estado", 2);  
        Debug.Log("Zombie recibió un disparo. Vida restante: " + puntosMiZombie);
    }

}
