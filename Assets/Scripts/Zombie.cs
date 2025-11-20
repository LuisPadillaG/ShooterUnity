
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    DatosJugador datosJugador;
    GameObject jugador;
    public GameObject prefabZombieMuerto;
    Vector3 rotacion;
    //Vector3 posicion;
    GameObject modelo, DatosJuego;
    Animator zombieAnimator;
    float puntosMiZombie;
    float velocidadZombie;
    AudioSource zombieFar_uno;
    NavMeshAgent navMeshAgent;
    bool calculoSobreDatosJugador, variabledos; //puse esto para que tenga un frame extra en reconocer toda la informacion en datos Jugador, ya que antes no me lo dejaba. Por eso lo hacemos una funcion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("se ejecuto el primer start");
        // variables
        velocidadZombie = 1.3f;
        DatosJuego = GameObject.Find("DatosJuego");
        datosJugador = DatosJuego.GetComponent<DatosJugador>();
        modelo = this.transform.GetChild(0).gameObject;
        zombieAnimator = modelo.GetComponent<Animator>();
        jugador = GameObject.FindGameObjectWithTag("Player");
        rotacion = Vector3.zero;
        //posicion = this.transform.position;
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        puntosMiZombie = 50;
        zombieFar_uno = this.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<AudioSource>();
        //calculo sobre ronda respecto al zombie
        CalcularDatosZombie();
        calculoSobreDatosJugador = true;
        variabledos = true;
        navMeshAgent.speed = velocidadZombie;
        Debug.Log("asi mensaje");
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("puntos de salud actual" + puntosMiZombie);
        //Debug.Log(variabledos);
        /*if (variabledos)
        {
            Debug.Log("Se esta ejecutanhdo calculio sobre datosmjugador");
            CalcularDatosZombie();
        }*/

        this.transform.rotation = Quaternion.Euler(rotacion);
        rotacion.y = HerramientasGenericas.CalcularAnguloBidimensional(new Vector2(this.transform.position.x, this.transform.position.z), new Vector2(jugador.transform.position.x, jugador.transform.position.z));

        /*posicion.x += Mathf.Cos(rotacion.y * Mathf.Deg2Rad) * Time.deltaTime * velocidadZombie;
        posicion.z -= Mathf.Sin(rotacion.y * Mathf.Deg2Rad) * Time.deltaTime * velocidadZombie;
        this.transform.position = posicion;*/
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
        navMeshAgent.SetDestination(jugador.transform.position);
    }
    public void CalcularDatosZombie()
    {
        Debug.Log("CALCULO EJECUTADO");
        Debug.Log("RONDA = " + datosJugador.RondaActual);
        for (int i = 1; i <= datosJugador.RondaActual; i++)
        {
            puntosMiZombie += 100;
            if (i > 5 && i <= 9)
            {
                velocidadZombie = 1.6f;
            }
            if (i > 9)
            {
                velocidadZombie = 3f;
                puntosMiZombie += puntosMiZombie * 0.1f; 
            }
        }
        zombieFar_uno.Play();
        //calculoSobreDatosJugador = false;
        Debug.Log("zombie creado con vida " + puntosMiZombie);
    }
    public void RecibirDisparo(float danoDisparo)
    {
        puntosMiZombie -= danoDisparo;
        //zombieAnimator.SetInteger("estado", 2);  
        //Debug.Log("Zombie recibió un disparo. Vida restante: " + puntosMiZombie);
        if(puntosMiZombie <= 0)
        {
            ZombieEliminado();
        }
    }
    public void ZombieEliminado()
    {
        //Debug.Log("zombie eliminadoooooooo");
        Instantiate(prefabZombieMuerto, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
    // tutorial para las particulas de la niebla: https://youtu.be/8pgi1TBGCKM?si=lwtmWtJ4o1i1UxE6
}
