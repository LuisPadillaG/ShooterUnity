using UnityEngine;

public class Camara : MonoBehaviour
{
    public Vector3 offset; // distancia entre la camara y el jugador
    private Transform target; // el target es el jugador
    // Los rangos dice a unity que la variable tendra un valor de entre el 1er al 2do numero
    [Range (0,1)]public float lerpValue;
    public float sensibilidad;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.Find("Personaje").transform; // target será igual al personaje del transform
    }

    private void LateUpdate()
    {
        // Vector3.Lep mueve la posicion de un objeto desde su vector hasta otro vector pero de una forma suavizada
        // lerp dice la velocidad
        transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpValue);
        // Vector.up el eje que mira siempre hacia arriba es el eje Y, por lo que si queremos girar al rededor de ese eje es Vector.up
        // Le anadimos un angulo que el raton tenga del eje x, y lo vamos a multiplicar por si mismo para que se aplique al offset y se ponga correctamente hacia el jugador.
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * sensibilidad, Vector3.up) * offset;


        transform.LookAt(target); // hara que mire a target
    }
}
