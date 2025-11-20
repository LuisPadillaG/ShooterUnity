
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JugadorScriptActual : MonoBehaviour
{
    //public GameObject prefabZombie;


    GameObject DatosJuego;
    DatosJugador datosJugador;
    CharacterController characterController;

    // version profe (Variables esenciales)
    GameObject objetoRotacionCamara;
    GameObject camara;
    GameObject MainCamera;
    GameObject objetoModelo;
    GameObject HuesoCabeza;
    Animator animatorModelo;
    Vector3 rotacionCamara;

    Vector3 velocidad; //CharacterController
    bool agachado, caminando; // cambios de camara segun el movimiento
    int puntosVelocidad;
    int estadoObjetivo;
    Vector3 velocidadCamara; // recoil
    float countdownTiempoPorBala;
    Vector3 velocidadAgachado;
    Vector3 posicionBaseCamara;

    public GameObject prefabBala;

    /* efectos de sonido del jugador */
    AudioSource pasoDerecho, pasoIzquierdo;
    float contador_caminata;
    bool caminataActiva, corridaActiva;
    bool estabaEnSuelo;
    int pieTurno; // 1 pie derecho, 2 pie izquierdo
    AudioSource correrDerecho, correrIzquierda;
    AudioSource saltoMadera, saltoPiso;
    AudioSource sonidoArma_ak47, sonidoArma_M1911;

    // objetos publicos (armas)
    public GameObject AK47;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DatosJuego = GameObject.Find("DatosJuego");
        Debug.Log(DatosJuego.gameObject.name);
        datosJugador = DatosJuego.GetComponent<DatosJugador>();

        agachado = false;
        caminando = false;
        velocidadAgachado = Vector3.zero;
        characterController = this.GetComponent<CharacterController>();
        velocidad = Vector3.zero;
        puntosVelocidad = 4;
        camara = this.transform.GetChild(1).gameObject;
        posicionBaseCamara = camara.transform.position; 
        HuesoCabeza = this.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).gameObject;
        objetoModelo = this.transform.GetChild(0).gameObject;
        animatorModelo = objetoModelo.GetComponent<Animator>();
        estadoObjetivo = 0;
        objetoRotacionCamara = new GameObject("RotacionMovimiento");
        objetoRotacionCamara.transform.SetParent(this.transform);
        MainCamera = GameObject.FindWithTag("MainCamera");

        rotacionCamara = camara.transform.rotation.eulerAngles;
        velocidadCamara = Vector3.zero;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        // armas
        datosJugador.armaActual = 1;
        Debug.Log("hola si estoy funcionado");
        datosJugador.armasJugador = new List<I_Armas>();
        datosJugador.armasJugador.Add(new M1911());
        datosJugador.armasJugador.Add(new AK47());
        countdownTiempoPorBala = datosJugador.armasJugador[datosJugador.armaActual].VelocidadPorBala;
        //================ efectos de sonido ===============
        pasoDerecho = this.transform.GetChild(2).GetChild(0).GetComponent<AudioSource>();
        pasoIzquierdo = this.transform.GetChild(2).GetChild(1).GetComponent<AudioSource>();
        contador_caminata = 0;
        correrDerecho = this.transform.GetChild(2).GetChild(2).GetComponent<AudioSource>();
        correrIzquierda = this.transform.GetChild(2).GetChild(3).GetComponent<AudioSource>();
        caminataActiva = false;
        corridaActiva = false;
        estabaEnSuelo = true;
        pieTurno = 1;
        saltoMadera = this.transform.GetChild(2).GetChild(4).GetComponent<AudioSource>();
        saltoPiso = this.transform.GetChild(2).GetChild(5).GetComponent<AudioSource>();
        // efectos de sonido - armas
        sonidoArma_M1911 = this.transform.GetChild(3).GetChild(0).GetComponent<AudioSource>();
        sonidoArma_ak47 = this.transform.GetChild(3).GetChild(6).GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("PruebasConMapa");
        }
        // Movimiento
        if (Input.GetKey(KeyCode.LeftShift))
        {
            puntosVelocidad = 6;
        }
        else
        {
            puntosVelocidad = 4;
        }
        velocidad.x = Input.GetAxis("Horizontal") * puntosVelocidad;
        velocidad.z = Input.GetAxis("Vertical") * puntosVelocidad;

        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocidad.y = 7.5f;
            }
            //Debug.Log(velocidadAgachado);
        }
        else
        {
            velocidad.y -= 30 * Time.deltaTime;
        }

        // Rotacion version profe
        objetoRotacionCamara.transform.rotation = camara.transform.rotation;
        objetoRotacionCamara.transform.rotation = Quaternion.Euler(0, objetoRotacionCamara.transform.rotation.eulerAngles.y, 0);
        characterController.Move(objetoRotacionCamara.transform.TransformDirection(velocidad) * Time.deltaTime);
        objetoModelo.transform.rotation = Quaternion.Euler(0, objetoRotacionCamara.transform.rotation.eulerAngles.y, 0);

        // Rotación de cámara
        rotacionCamara.y += Input.GetAxis("Mouse X") * 3;
        rotacionCamara.x -= Input.GetAxis("Mouse Y") * 3;
        //Basado en MiVersion Camara)
        rotacionCamara.x = Mathf.Clamp(rotacionCamara.x, -40, 40);


        camara.transform.rotation = Quaternion.Euler(rotacionCamara);
        Debug.DrawRay(camara.transform.position, camara.transform.TransformDirection(new Vector3(0, 0, 1)), Color.green);
        // Cambio de arma
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            datosJugador.armaActual = 0;
            countdownTiempoPorBala = datosJugador.armasJugador[datosJugador.armaActual].VelocidadPorBala;
            I_Armas arma = datosJugador.armasJugador[datosJugador.armaActual];
            Debug.Log("Usando arma: " + arma.Nombre + " con " + arma.Balas + " balas.");

        }
        if(datosJugador.armasJugador.Count > 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                datosJugador.armaActual = 1;
                countdownTiempoPorBala = datosJugador.armasJugador[datosJugador.armaActual].VelocidadPorBala;
                I_Armas arma = datosJugador.armasJugador[datosJugador.armaActual];
                Debug.Log("Usando arma: " + arma.Nombre + " con " + arma.Balas + " balas.");
            }
        } 
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            datosJugador.armaActual = 2;
            countdownTiempoPorBala = datosJugador.armasJugador[datosJugador.armaActual].VelocidadPorBala;
            I_Armas arma = datosJugador.armasJugador[datosJugador.armaActual];
            Debug.Log("Usando arma: " + arma.Nombre + " con " + arma.Balas + " balas.");

        }
        // Rango de disparo
        Vector3 direccionCamara = camara.transform.TransformDirection(Vector3.forward);
        direccionCamara.y += -0.1f + Random.value * 0.1f;
        direccionCamara.x += -0.1f + Random.value * 0.1f;
        // Recoil
        rotacionCamara += velocidadCamara * Time.deltaTime;
        velocidadCamara.x += Time.deltaTime * 500;
        if (velocidadCamara.x > 0)
        {
            velocidadCamara.x = 0;
        }

        //pistola disparo + velocidad
        RaycastHit hit;
        if (Input.GetMouseButton(0))
        {
            countdownTiempoPorBala -= Time.deltaTime;
            if (countdownTiempoPorBala < 0)
            {
                countdownTiempoPorBala = datosJugador.armasJugador[datosJugador.armaActual].VelocidadPorBala;
            }
            if (countdownTiempoPorBala == datosJugador.armasJugador[datosJugador.armaActual].VelocidadPorBala)
            {
                if (Physics.Raycast(camara.transform.position, direccionCamara, out hit, 15))
                {
                    velocidadCamara.x = -70;
                    Debug.Log("le di a " + hit.collider.gameObject.tag);
                    Instantiate(prefabBala, hit.point, Quaternion.identity);
                    if (hit.collider.gameObject.tag == "Zombie")
                    {
                        Zombie zombie = hit.collider.GetComponentInParent<Zombie>();
                        if (zombie != null)
                        {
                            Debug.Log("se esta ejecutando esto");
                            zombie.RecibirDisparo(datosJugador.armasJugador[datosJugador.armaActual].DanoPorBala);
                        }
                    }
                    if (hit.collider.gameObject.tag == "ZombieHead")
                    {
                        Zombie zombie = hit.collider.GetComponentInParent<Zombie>();
                        if (zombie != null)
                        {
                            Debug.Log("se esta ejecutando esto");
                            zombie.RecibirDisparo(datosJugador.armasJugador[datosJugador.armaActual].DanoPorBala * 2);
                        }
                    }
                    switch (datosJugador.armasJugador[datosJugador.armaActual].ID)
                    {
                        case 0: // M1911
                            sonidoArma_M1911.PlayOneShot(sonidoArma_M1911.clip);
                            break;
                        case 1: // B23R
                            break;
                        case 2: // Remington870
                            break;
                        case 3: // SPAS12
                            break;
                        case 4: // MP5
                            break;
                        case 5: // Uzi
                            break;
                        case 6: // AK47
                            sonidoArma_ak47.PlayOneShot(sonidoArma_ak47.clip);
                            break;
                        case 7: // M16
                            break;
                        case 9: // GranadaFragmentacion
                            break;
                        case 10: // Molotov
                            break;
                    }
                }
            }

        }
        // ------------------------------------------------ Animaciones + SFX
        camara.transform.position = HuesoCabeza.transform.position;
        if (velocidad.x == 0 && velocidad.z == 0)
        {
            estadoObjetivo = 0;
            caminataActiva = false;
            corridaActiva = false;
        }
        else
        {
            estadoObjetivo = 1;
            caminataActiva = true;
            if (Input.GetKey(KeyCode.LeftShift) && characterController.isGrounded)
            {
                caminataActiva = false;
                corridaActiva = true;
                estadoObjetivo = 2;
                contador_caminata += Time.deltaTime;
                if (contador_caminata >= 0.25f)
                {
                    switch (pieTurno)
                    {
                        case 1:
                            correrDerecho.Play();
                            pieTurno = 2;
                            break;
                        case 2:
                            correrIzquierda.Play();
                            pieTurno = 1;
                            break;
                    }
                    contador_caminata = 0;
                }
            }
            else
            {
                corridaActiva = false;
            }
            if (caminataActiva && characterController.isGrounded)
            {
                contador_caminata += Time.deltaTime;
                if (contador_caminata >= 0.5f)
                {
                    switch (pieTurno)
                    {
                        case 1:
                            pasoDerecho.Play();
                            pieTurno = 2;
                            break;
                        case 2:
                            pasoIzquierdo.Play();
                            pieTurno = 1;
                            break;
                    }
                    contador_caminata = 0;
                }
            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            estadoObjetivo = 3;
        }
        if (!estabaEnSuelo && characterController.isGrounded)
        {
            // aqui deberia poner una logica donde reconozca donde esta pisando, ya que tenemos dos sonidos (dependiendo del piso).
            // como nota: si vamos a trabajar de la misma manera todo este algoritmo, dependiendo del piso. Podemos almacenar todo esto en dos metodos (un metodo para madera, un metodo para piso normal) y ya simplemente segun el piso las llamamos. Creo que funcionaria, eso espero.
            saltoMadera.Play();
        }
        estabaEnSuelo = characterController.isGrounded;
        if (estadoObjetivo != animatorModelo.GetInteger("Estado"))
        {
            animatorModelo.SetInteger("Estado", estadoObjetivo);
            animatorModelo.SetTrigger("CambiarEstado");
        }
        // ----------------------------------------- Fin animaciones
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Zombie" || collision.gameObject.tag == "ZombieHead")
        {
            Debug.Log("Me electrocutaste pedrito");
        }
    }
    
}
