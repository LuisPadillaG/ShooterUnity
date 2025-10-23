
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
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
    Vector3 velocidadAgachado;
    Vector3 posicionBaseCamara;

    public GameObject prefabBala; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DatosJuego = GameObject.Find("DatosJuego");
        datosJugador = DatosJuego.GetComponent<DatosJugador>();

        agachado = false;
        caminando = false;
        velocidadAgachado = Vector3.zero;
        characterController = this.GetComponent<CharacterController>();
        velocidad = Vector3.zero;
        puntosVelocidad = 4;
        camara = this.transform.GetChild(1).gameObject;
        posicionBaseCamara = camara.transform.position;
        HuesoCabeza = this.transform.GetChild(0).GetChild(12).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).gameObject;
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
        datosJugador.armasJugador.Add(new M1911());
        datosJugador.armasJugador.Add(new AK47());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Echos_FPS");
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
                velocidad.y = 10;
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
            I_Armas arma = datosJugador.armasJugador[datosJugador.armaActual];
            Debug.Log("Usando arma: " + arma.Nombre + " con " + arma.Balas + " balas.");

        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            datosJugador.armaActual = 1;
            I_Armas arma = datosJugador.armasJugador[datosJugador.armaActual];
            Debug.Log("Usando arma: " + arma.Nombre + " con " + arma.Balas + " balas.");

        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            datosJugador.armaActual = 2;
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
        

        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(camara.transform.position, direccionCamara, out hit, 15))
            {
                velocidadCamara.x = -70;
                Debug.Log("le di a " + hit.collider.gameObject.tag);
                Instantiate(prefabBala, hit.point, Quaternion.identity);
                if(hit.collider.gameObject.tag == "Zombie")
                {
                    Zombie zombie = hit.collider.GetComponentInParent<Zombie>(); 
                    if (zombie != null)
                    {
                        Debug.Log("se esta ejecutando esto");
                        zombie.RecibirDisparo(datosJugador.armasJugador[datosJugador.armaActual].DanoPorBala);
                    }
                }
            }
        } 
        // Animaciones
        camara.transform.position = HuesoCabeza.transform.position;
        if(velocidad.x == 0 && velocidad.z == 0)
        {
            estadoObjetivo = 0;
            if (caminando)
            {
                caminando = false;
                //MainCamera.transform.position -= MainCamera.transform.TransformDirection(new Vector3(0, -0.14f, 0.6f));
                //characterController.radius = 0.25f;
            }
        }
        else
        {
            estadoObjetivo = 1;
            if (!caminando)
            {
                caminando = true;
                //MainCamera.transform.position += MainCamera.transform.TransformDirection(new Vector3(0, -0.14f, 0.6f));
                //characterController.radius = 0.6f;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                estadoObjetivo = 2;
            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            estadoObjetivo = 3;
        }
        if(estadoObjetivo != animatorModelo.GetInteger("Estado"))
        {
            animatorModelo.SetInteger("Estado", estadoObjetivo);
            animatorModelo.SetTrigger("CambiarEstado");
        }
        // Fin animaciones
    }
}
