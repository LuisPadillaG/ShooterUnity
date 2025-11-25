
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class JugadorScriptActual : MonoBehaviour
{
    //public GameObject prefabZombie;
    public GameObject prefabM1619;
    public GameObject prefabB23R;
    public GameObject prefabMP5;
    public GameObject prefabUzi;
    //ahora rifles
    public GameObject prefabAK_47;
    public GameObject prefabRemington;
    public GameObject prefabM16;
    public GameObject prefabS12;
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
    public bool disparoActivo; // solo para mis prefabs
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
    AudioSource sonidoArma_ak47, sonidoArma_M1911, sonidoArma_B23R, sonidoArma_Remington, sonidoArma_Spas, sonidoArma_MP5, sonidoArma_Uzi, sonidoArma_M16;

    // objetos publicos (armas)
    public GameObject AK47;
    float contadorAnimacionCambioDeArma;
    public float contadorRecarga;
    GameObject modeloArma;
    // golpe de zombie
    public Volume globalVolume;  
    Vignette vignette;
    float coolDownRecibirDano;
    bool golpeado_recientemente;
    //
    Vector3 retrocesoActual;
    float tiempoRetroceso;
    float duracionRetroceso; // tiempo que dura el retroceso
    float fuerzaRetroceso; // distancia total del retroceso

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
        datosJugador.armaActual = 0;
        contadorRecarga = 0;
        Debug.Log("hola si estoy funcionado");
        datosJugador.armasJugador = new List<I_Armas>();
        datosJugador.armasJugador.Add(new M1911());
        modeloArma = Instantiate(prefabM1619, new Vector3(0,0,0), Quaternion.identity);
        modeloArma.transform.SetParent(camara.transform, false);
        //datosJugador.armasJugador.Add(new AK47());
        countdownTiempoPorBala = datosJugador.armasJugador[datosJugador.armaActual].VelocidadPorBala;
        disparoActivo = false;
        contadorAnimacionCambioDeArma = 2;
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
        sonidoArma_B23R = this.transform.GetChild(3).GetChild(1).GetComponent<AudioSource>();
        sonidoArma_Remington = this.transform.GetChild(3).GetChild(2).GetComponent<AudioSource>();
        sonidoArma_Spas = this.transform.GetChild(3).GetChild(3).GetComponent<AudioSource>();
        sonidoArma_MP5 = this.transform.GetChild(3).GetChild(4).GetComponent<AudioSource>();
        sonidoArma_Uzi = this.transform.GetChild(3).GetChild(5).GetComponent<AudioSource>();
        sonidoArma_ak47 = this.transform.GetChild(3).GetChild(6).GetComponent<AudioSource>();
        sonidoArma_M16 = this.transform.GetChild(3).GetChild(7).GetComponent<AudioSource>();

        //recibir daño
        globalVolume.profile.TryGet(out vignette);
        coolDownRecibirDano = 0.4f;
        golpeado_recientemente = false;
        retrocesoActual = Vector3.zero;
        tiempoRetroceso = 0f;
        duracionRetroceso = 0.2f; 
        fuerzaRetroceso = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        contadorRecarga -= Time.deltaTime;
        vignette.intensity.value -= Time.deltaTime * 0.6f;
        if (golpeado_recientemente)
        {
            coolDownRecibirDano -= Time.deltaTime;
            if(coolDownRecibirDano < 0)
            {
                golpeado_recientemente = false;
                coolDownRecibirDano = 0.4f;
                Debug.Log("ya le pueden pegar nuevamente");
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {/*
            if (datosJugador.armasJugador[datosJugador.armaActual].Balas > 0)
            {
                if (datosJugador.armasJugador[datosJugador.armaActual].MaximoBalas <= datosJugador.armasJugador[datosJugador.armaActual].MunicionBalas)
                {
                    int balasA_Entregar = datosJugador.armasJugador[datosJugador.armaActual].MaximoBalas - datosJugador.armasJugador[datosJugador.armaActual].Balas;
                    datosJugador.armasJugador[datosJugador.armaActual].Balas += balasA_Entregar;
                    datosJugador.armasJugador[datosJugador.armaActual].MunicionBalas -= balasA_Entregar;
                    contadorRecarga = datosJugador.velocidadRecargaPorSegundo;
                }else if (datosJugador.armasJugador[datosJugador.armaActual].MaximoBalas > datosJugador.armasJugador[datosJugador.armaActual].MunicionBalas)
                {
                    int balasA_QuitardeMaximoBalas = datosJugador.armasJugador[datosJugador.armaActual].MaximoBalas - datosJugador.armasJugador[datosJugador.armaActual].MunicionBalas;
                    datosJugador.armasJugador[datosJugador.armaActual].Balas += balasA_QuitardeMaximoBalas;
                    datosJugador.armasJugador[datosJugador.armaActual].MunicionBalas = 0;
                    contadorRecarga = datosJugador.velocidadRecargaPorSegundo;
                }
            }
        */
            RecargarArma();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene("PruebasConMapa");
        }
        // Movimiento
        if (contadorAnimacionCambioDeArma < 2)
        {
            contadorAnimacionCambioDeArma += Time.deltaTime;
        }
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
        if (tiempoRetroceso > 0f)
        {
            Vector3 mover = retrocesoActual * (Time.deltaTime / duracionRetroceso); // aplica poco a poco
            characterController.Move(mover);
            tiempoRetroceso -= Time.deltaTime;
            if (tiempoRetroceso <= 0f)
            {
                retrocesoActual = Vector3.zero;
            }
        }
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
            if(datosJugador.armaActual != 0)
            {
                CambioA_Pistola();
            }
            datosJugador.armaActual = 0;
            
        }
        if(datosJugador.armasJugador.Count > 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (datosJugador.armaActual != 1)
                {
                    CambioA_Rifle();
                }
                datosJugador.armaActual = 1;
            }
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
        if (Input.GetMouseButton(0) && datosJugador.armasJugador[datosJugador.armaActual].Balas > 0 && contadorRecarga <= 0)
        {
            countdownTiempoPorBala -= Time.deltaTime;
            disparoActivo = false;
            if (countdownTiempoPorBala < 0)
            {
                if(datosJugador.duplicadoBalasActivas){
                    countdownTiempoPorBala = datosJugador.armasJugador[datosJugador.armaActual].VelocidadPorBala / 2;
                }
                else
                {
                    countdownTiempoPorBala = datosJugador.armasJugador[datosJugador.armaActual].VelocidadPorBala;
                }
            }
            if (datosJugador.duplicadoBalasActivas) // en caso de ser activado el perk
            {
                if (countdownTiempoPorBala == datosJugador.armasJugador[datosJugador.armaActual].VelocidadPorBala / 2)
                {
                    disparoActivo = true;
                    if (Physics.Raycast(camara.transform.position, direccionCamara, out hit, 15))
                    {
                        //velocidadCamara.x = -70; // esta linea era el recoil generico. Ahora no
                        velocidadCamara.x = datosJugador.armasJugador[datosJugador.armaActual].Recoil;
                        datosJugador.armasJugador[datosJugador.armaActual].Balas--;
                        //Sistema de recarga automatica
                        if (datosJugador.armasJugador[datosJugador.armaActual].Balas <= 0)
                        {
                            RecargarArma();
                        }
                        Debug.Log("le di a " + hit.collider.gameObject.tag);
                        Instantiate(prefabBala, hit.point, Quaternion.identity);
                        if (hit.collider.gameObject.tag == "Zombie")
                        {
                            Zombie zombie = hit.collider.GetComponentInParent<Zombie>();
                            if (zombie != null)
                            {
                                Debug.Log("se esta ejecutando esto");
                                datosJugador.puntos += 10;
                                zombie.RecibirDisparo(datosJugador.armasJugador[datosJugador.armaActual].DanoPorBala);
                            }
                        }
                        if (hit.collider.gameObject.tag == "ZombieHead")
                        {
                            Zombie zombie = hit.collider.GetComponentInParent<Zombie>();
                            if (zombie != null)
                            {
                                Debug.Log("se esta ejecutando esto");
                                datosJugador.puntos += 20;
                                zombie.RecibirDisparoCabeza(datosJugador.armasJugador[datosJugador.armaActual].DanoPorBala);
                            }
                        }
                        switch (datosJugador.armasJugador[datosJugador.armaActual].ID)
                        {
                            case 0: // M1911
                                sonidoArma_M1911.PlayOneShot(sonidoArma_M1911.clip);
                                break;
                            case 1: // B23R
                                sonidoArma_B23R.PlayOneShot(sonidoArma_B23R.clip);
                                break;
                            case 2: // Remington870
                                sonidoArma_Remington.PlayOneShot(sonidoArma_Remington.clip);
                                break;
                            case 3: // SPAS12
                                sonidoArma_Spas.PlayOneShot(sonidoArma_Spas.clip);
                                break;
                            case 4: // MP5
                                sonidoArma_MP5.PlayOneShot(sonidoArma_MP5.clip);
                                break;
                            case 5: // Uzi
                                sonidoArma_Uzi.PlayOneShot(sonidoArma_Uzi.clip);
                                break;
                            case 6: // AK47
                                sonidoArma_ak47.PlayOneShot(sonidoArma_ak47.clip);
                                break;
                            case 7: // M16
                                sonidoArma_M16.PlayOneShot(sonidoArma_M16.clip);
                                break;
                            case 9: // GranadaFragmentacion
                                break;
                            case 10: // Molotov
                                break;
                        }
                    }
                }
            }// sino no, ya estuvo suave
            else if (countdownTiempoPorBala == datosJugador.armasJugador[datosJugador.armaActual].VelocidadPorBala)
            {
                disparoActivo = true;
                if (Physics.Raycast(camara.transform.position, direccionCamara, out hit, 15))
                {
                    //velocidadCamara.x = -70; // esta linea era el recoil generico. Ahora no
                    velocidadCamara.x = datosJugador.armasJugador[datosJugador.armaActual].Recoil;
                    datosJugador.armasJugador[datosJugador.armaActual].Balas--;
                    //Sistema de recarga automatica
                    if (datosJugador.armasJugador[datosJugador.armaActual].Balas <= 0)
                    {
                        RecargarArma();
                    }
                    Debug.Log("le di a " + hit.collider.gameObject.tag);
                    Instantiate(prefabBala, hit.point, Quaternion.identity);
                    if (hit.collider.gameObject.tag == "Zombie")
                    {
                        Zombie zombie = hit.collider.GetComponentInParent<Zombie>();
                        if (zombie != null)
                        {
                            Debug.Log("se esta ejecutando esto");
                            datosJugador.puntos += 10;
                            zombie.RecibirDisparo(datosJugador.armasJugador[datosJugador.armaActual].DanoPorBala);
                        }
                    }
                    if (hit.collider.gameObject.tag == "ZombieHead")
                    {
                        Zombie zombie = hit.collider.GetComponentInParent<Zombie>();
                        if (zombie != null)
                        {
                            Debug.Log("se esta ejecutando esto");
                            datosJugador.puntos += 20;
                            zombie.RecibirDisparoCabeza(datosJugador.armasJugador[datosJugador.armaActual].DanoPorBala);
                        }
                    }
                    switch (datosJugador.armasJugador[datosJugador.armaActual].ID)
                    {
                        case 0: // M1911
                            sonidoArma_M1911.PlayOneShot(sonidoArma_M1911.clip);
                            break;
                        case 1: // B23R
                            sonidoArma_B23R.PlayOneShot(sonidoArma_B23R.clip);
                            break;
                        case 2: // Remington870
                            sonidoArma_Remington.PlayOneShot(sonidoArma_Remington.clip);
                            break;
                        case 3: // SPAS12
                            sonidoArma_Spas.PlayOneShot(sonidoArma_Spas.clip);
                            break;
                        case 4: // MP5
                            sonidoArma_MP5.PlayOneShot(sonidoArma_MP5.clip);
                            break;
                        case 5: // Uzi
                            sonidoArma_Uzi.PlayOneShot(sonidoArma_Uzi.clip);
                            break;
                        case 6: // AK47
                            sonidoArma_ak47.PlayOneShot(sonidoArma_ak47.clip);
                            break;
                        case 7: // M16
                            sonidoArma_M16.PlayOneShot(sonidoArma_M16.clip);
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
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Zombie" || hit.gameObject.tag == "ZombieHead")
        {
            Debug.Log("Estamos cerca, cuidado");
            if(vignette.intensity.value <= 0.1f)
            {
                vignette.intensity.value = 0.1f;
            }
        }
    }
    public void Golpeado()
    {
        if (!golpeado_recientemente)
        {
            Debug.Log("Me electrocutaste pedrito");
            datosJugador.vida -= 40;
            vignette.intensity.value = 0.8f;
            retrocesoActual = -objetoRotacionCamara.transform.forward * fuerzaRetroceso;
            tiempoRetroceso = duracionRetroceso;

            if (datosJugador.vida <= 0)
            {
                Debug.Log("GAMEEEEEEEEEEEE OVERRRRRRRRRRRRR");
                SceneManager.LoadScene("GameOver");
            }

            golpeado_recientemente = true;
        }
    }
    public void RecargarArma()
    {
        var arma = datosJugador.armasJugador[datosJugador.armaActual]; 
        if (arma.Balas == arma.MaximoBalas)
            return; 
        if (arma.MunicionBalas <= 0)
        {
            Debug.Log("No tienes más balas...");
            return;
        } 
        int espacioEnCargador = arma.MaximoBalas - arma.Balas; 
        if (arma.MunicionBalas >= espacioEnCargador)
        {
            arma.Balas += espacioEnCargador;
            arma.MunicionBalas -= espacioEnCargador;
        }
        else
        { 
            arma.Balas += arma.MunicionBalas;
            arma.MunicionBalas = 0;
        }
         
        contadorRecarga = datosJugador.velocidadRecargaPorSegundo;
    }
    public void CompraNuevaArma(I_Armas armaEnviada)
    {
        Debug.Log("Muchas felicidades que acabas de obtener un arma");
        Debug.Log("El arma en cuestion se llama: "+armaEnviada.Nombre);
        if(armaEnviada.Tipo == "Subfusiles" || armaEnviada.Tipo == "Pistolas")
        {
            if (datosJugador.armasJugador.Count >= 1)
            {
                datosJugador.armasJugador.RemoveAt(0);
            }
            datosJugador.armasJugador.Insert(0, armaEnviada);
            CambioA_Pistola();
        }else if(armaEnviada.Tipo == "Rifles de asalto" || armaEnviada.Tipo == "Escopetas") {
            if (datosJugador.armasJugador.Count == 0)
            {
                datosJugador.armasJugador.Add(null);
            }
            if (datosJugador.armasJugador.Count >= 2)
            {
                datosJugador.armasJugador.RemoveAt(1);
            }
            if (datosJugador.armasJugador.Count == 1)
            {
                datosJugador.armasJugador.Add(armaEnviada);
            }
            else
            {
                datosJugador.armasJugador.Insert(1, armaEnviada);
            }
            CambioA_Rifle();
        }
    }
    public void CambioA_Pistola()
    {
        Destroy(modeloArma);
        datosJugador.armaActual = 0;
        switch (datosJugador.armasJugador[datosJugador.armaActual].Nombre)
        {
            case "M1911":
                modeloArma = Instantiate(prefabM1619, new Vector3(0, 0, 0), Quaternion.identity);
                modeloArma.transform.SetParent(camara.transform, false);
                break;
            case "B23R":
                modeloArma = Instantiate(prefabB23R, new Vector3(0, 0, 0), Quaternion.identity);
                modeloArma.transform.SetParent(camara.transform, false);
                break;
            case "Uzi":
                modeloArma = Instantiate(prefabUzi, new Vector3(0, 0, 0), Quaternion.identity);
                modeloArma.transform.SetParent(camara.transform, false);
                break;
            case "MP5":
                modeloArma = Instantiate(prefabMP5, new Vector3(0, 0, 0), Quaternion.identity);
                modeloArma.transform.SetParent(camara.transform, false);
                break;
        }
        countdownTiempoPorBala = 0;
        I_Armas arma = datosJugador.armasJugador[datosJugador.armaActual];
        Debug.Log("Usando arma: " + arma.Nombre + " con " + arma.Balas + " balas.");
    }
    public void CambioA_Rifle()
    {
        Destroy(modeloArma);
        datosJugador.armaActual = 1;
        switch (datosJugador.armasJugador[datosJugador.armaActual].Nombre)
        {
            case "AK-47":
                modeloArma = Instantiate(prefabAK_47, new Vector3(0, 0, 0), Quaternion.identity);
                modeloArma.transform.SetParent(camara.transform, false);
                break;
            case "Remington 870":
                modeloArma = Instantiate(prefabRemington, new Vector3(0, 0, 0), Quaternion.identity);
                modeloArma.transform.SetParent(camara.transform, false);
                break;
            case "SPAS-12":
                modeloArma = Instantiate(prefabS12, new Vector3(0, 0, 0), Quaternion.identity);
                modeloArma.transform.SetParent(camara.transform, false);
                break;
            case "M16":
                modeloArma = Instantiate(prefabM16, new Vector3(0, 0, 0), Quaternion.identity);
                modeloArma.transform.SetParent(camara.transform, false);
                break;
        }
        countdownTiempoPorBala = 0;
        I_Armas arma = datosJugador.armasJugador[datosJugador.armaActual];
        Debug.Log("Usando arma: " + arma.Nombre + " con " + arma.Balas + " balas.");
    }

}
