
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
    ZombieAnimations animacion_ataque;
    Animator zombieAnimator;
    float puntosMiZombie;
    float velocidadZombie;
    float velocidadZombie_original;
    AudioSource zombieFar_uno;
    NavMeshAgent navMeshAgent;
    bool isHitting;
    float contadorIsTaSiendoGolpeado; //jaja, esta bien chido el nombre de esta variable ayno que risa.
    bool golpesonido; //true es uno, falso es otro
    AudioSource golpeUno, golpeDos, muelto;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // Debug.Log("se ejecuto el primer start");
        // variables
        velocidadZombie = 1.3f;  
        velocidadZombie_original = 1.3f;
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
        velocidadZombie_original = velocidadZombie;
        navMeshAgent.speed = velocidadZombie;
        //Debug.Log("asi mensaje");
        isHitting = false; 
        contadorIsTaSiendoGolpeado = 0;
        datosJugador.zombiesInGame += 1;
        animacion_ataque =  this.transform.GetChild(0).GetComponent<ZombieAnimations>();
        golpesonido = true;
        golpeUno = this.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<AudioSource>();
        golpeDos = this.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<AudioSource>();
        muelto = this.transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {  
        contadorIsTaSiendoGolpeado -= Time.deltaTime;
        this.transform.rotation = Quaternion.Euler(rotacion);
        rotacion.y = HerramientasGenericas.CalcularAnguloBidimensional(new Vector2(this.transform.position.x, this.transform.position.z), new Vector2(jugador.transform.position.x, jugador.transform.position.z));
        navMeshAgent.SetDestination(jugador.transform.position);
        // Pegar
        isHitting = false;
        float distancia = navMeshAgent.remainingDistance;
        if (!navMeshAgent.pathPending && distancia <= 2.2f)
        {
            //Debug.Log("El zombie está cerca del jugador!");
            if(!isHitting) {
                if (golpesonido)
                {
                    golpeUno.Play();
                    golpesonido = false;
                }
                else
                {
                    golpeDos.Play();
                    golpesonido = true;
                }
            }
            isHitting = true;
        } 
        /*posicion.x += Mathf.Cos(rotacion.y * Mathf.Deg2Rad) * Time.deltaTime * velocidadZombie;
        posicion.z -= Mathf.Sin(rotacion.y * Mathf.Deg2Rad) * Time.deltaTime * velocidadZombie;
        this.transform.position = posicion;*/
        // Animaciones
        if(contadorIsTaSiendoGolpeado < 0)
        {
            if (!isHitting)
            {
                if (velocidadZombie == 1.3f || velocidadZombie == 1.6f) //caminando, caminando rapido
                {
                    zombieAnimator.SetInteger("EstadoZombie", 0);
                }
                else
                {
                    zombieAnimator.SetInteger("EstadoZombie", 1);
                }
            }
            else
            {
                if (isHitting)
                {
                    zombieAnimator.SetInteger("EstadoZombie", 3);
                }
            }
        }
        else
        {
            if (contadorIsTaSiendoGolpeado > 0)
            {
                zombieAnimator.SetInteger("EstadoZombie", 4);
                animacion_ataque.AtaqueDesctivado();
            }
        }
        navMeshAgent.speed = velocidadZombie;
        // lo que sigue del zombie
        /*
        1. Golpe de brazo + animación 
        2. Recibir balazo
         */

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ColliderJugador"){
            Debug.Log("Estamos chocanco con el collider");
        }
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
        contadorIsTaSiendoGolpeado = 1f; 
        puntosMiZombie -= danoDisparo;
        zombieAnimator.SetInteger("estado", 2);  
        //Debug.Log("Zombie recibió un disparo. Vida restante: " + puntosMiZombie);
        if(puntosMiZombie <= 0)
        {
            datosJugador.puntos += 100;
            ZombieEliminado();
        }
    }
    public void RecibirDisparoCabeza(float danoDisparo)
    {
        contadorIsTaSiendoGolpeado = 1f;
        datosJugador.headshot_acertados++;
        danoDisparo= danoDisparo * 2;
        puntosMiZombie -= danoDisparo;
        zombieAnimator.SetInteger("estado", 2);
        //Debug.Log("Zombie recibió un disparo. Vida restante: " + puntosMiZombie);
        if (puntosMiZombie <= 0)
        {
            datosJugador.puntos += 200;
            ZombieEliminado();
        }
    }
    public void ZombieEliminado()
    {
        //Debug.Log("zombie eliminadoooooooo");
        Instantiate(prefabZombieMuerto, this.transform.position, this.transform.rotation);
        datosJugador.kills++;
        datosJugador.zombiesInGame -= 1;
        Destroy(this.gameObject);
        muelto.Play();
    }
    // tutorial para las particulas de la niebla: https://youtu.be/8pgi1TBGCKM?si=lwtmWtJ4o1i1UxE6
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            velocidadZombie = 10;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            velocidadZombie = velocidadZombie_original;
        }
    }
}
