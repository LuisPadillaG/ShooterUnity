using UnityEngine;

public class Jugador : MonoBehaviour
{
    CharacterController characterController;
    /*Transform transformCamara;
    Transform transformModelo;*/
    // version profe
    GameObject objetoRotacionCamara;
    GameObject camara;
    GameObject objetoModelo;
    Vector3 rotacionCamara;
    Vector3 posicionAnteriorMouse;

    Vector3 velocidad, rotacion;
    int puntosVelocidad;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
        velocidad = Vector3.zero;
        rotacion = Vector3.zero;
        puntosVelocidad= 4;
        /*transformCamara = this.transform.GetChild(1);
        transformModelo = this.transform.GetChild(0);*/
        camara = this.transform.GetChild(1).gameObject;
        objetoModelo = this.transform.GetChild(0).gameObject;
        objetoRotacionCamara = new GameObject();
        posicionAnteriorMouse = Input.mousePosition;
        rotacionCamara = camara.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        velocidad.x = 0;
        velocidad.z = 0;

        if(Input.GetAxis("Horizontal") != 0)
        {
            velocidad.x = Input.GetAxis("Horizontal") * puntosVelocidad;
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            velocidad.z = Input.GetAxis("Vertical") * puntosVelocidad;
        }

        // rotacion version profe
        objetoRotacionCamara.transform.rotation = camara.transform.rotation;
        objetoRotacionCamara.transform.rotation = Quaternion.Euler(0, objetoRotacionCamara.transform.rotation.eulerAngles.y, 0);
        characterController.Move(objetoRotacionCamara.transform.TransformDirection(velocidad) * Time.deltaTime);

        Vector3 movimientoMouse = Input.mousePosition - posicionAnteriorMouse;
        rotacionCamara.y += movimientoMouse.x * 0.5f;
        rotacionCamara.x -= movimientoMouse.y * 0.5f; 
        posicionAnteriorMouse = Input.mousePosition;
        camara.transform.rotation = Quaternion.Euler(rotacionCamara);
        objetoModelo.transform.rotation = Quaternion.Euler(0, rotacionCamara.y,0);
        //characterController.Move(velocidad * Time.deltaTime);

        // Mi rotacion de personaje
        /*rotacion = transformCamara.rotation.eulerAngles;
        rotacion.x = 0;
        rotacion.z = 0;
        transformModelo.rotation = Quaternion.Euler(rotacion);*/
        // Fin de mi rotacion de personaje

    }
}
