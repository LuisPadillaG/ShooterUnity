using UnityEngine;

public class Jugador : MonoBehaviour
{
    CharacterController characterController;
    Transform transformCamara;
    Transform transformModelo;
    Vector3 velocidad, rotacion;
    int puntosVelocidad;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
        velocidad = Vector3.zero;
        rotacion = Vector3.zero;
        puntosVelocidad= 4;
        transformCamara = this.transform.GetChild(1);
        transformModelo = this.transform.GetChild(0);
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
        characterController.Move(velocidad * Time.deltaTime);


        rotacion = transformCamara.rotation.eulerAngles;
        rotacion.x = 0;
        rotacion.z = 0;
        transformModelo.rotation = Quaternion.Euler(rotacion);
        //this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, transformCamara.rotation, 200 * Time.deltaTime);
        //this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, scriptFuncionesGenericas.transform.rotation, 400 * Time.deltaTime);
    }
}
