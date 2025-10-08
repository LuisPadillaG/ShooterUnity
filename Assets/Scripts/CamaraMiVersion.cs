using UnityEngine;

public class CamaraMiVersion : MonoBehaviour
{
    float mouseX, mouseY;
    Vector3 rotacion;
    float contadorParaDebugLog;

    public float sensibilidad = 400f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotacion = Vector3.zero;
        contadorParaDebugLog = 0;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;

        rotacion.y += mouseX;
        rotacion.x -= mouseY;
        rotacion.x = Mathf.Clamp(rotacion.x, -70f, 70f);

        this.transform.rotation = Quaternion.Euler(rotacion);

        /*if (mouseX != 0 && mouseY != 0) {
            rotacion.y = FuncionesGenericas.CalcularAnguloBidimensional(new Vector2(0,0), new Vector2(mouseX, mouseY));
        }*/
        contadorParaDebugLog += Time.deltaTime;
        if (contadorParaDebugLog > 1) { 
            Debug.Log(rotacion);
            contadorParaDebugLog = 0;
        }
        
    }
}
