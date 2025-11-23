using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InicioJuego : MonoBehaviour
{
    // animacion inicial logo
    public GameObject logo;
    Vector3 scaleLogo;
    float contadorLogo;
    bool interfazActiva;
    public GameObject zombieSoundBox, changeSoundBox;
    
    AudioSource zombieSoundFX, changeSoundFX;
    public GameObject selectSolo, selectExit;
    Image imgSelectSolo;
    Color colorImgSelectSolo;
    // Variables de control para el movimiento
    int seleccion; // 0: solo game. 1: exit game. .74


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // animacion inicial logo
        scaleLogo = new Vector3(20,20,20);
        logo.transform.localScale = scaleLogo;
        imgSelectSolo = selectSolo.GetComponent<Image>(); //agarramos la propiedad imagen
        colorImgSelectSolo = imgSelectSolo.color; //agarramos ahora solo el color de esa propiedad imagen, siento que es mucha vaina pero equis.
        contadorLogo = 0;
        interfazActiva = false;
        zombieSoundFX = zombieSoundBox.transform.GetComponent<AudioSource>();
        changeSoundFX = changeSoundBox.transform.GetComponent<AudioSource>();
        selectSolo.SetActive(false);
        selectExit.SetActive(false);
        seleccion = 0;
        colorImgSelectSolo.a = 0.1f;
        imgSelectSolo.color =colorImgSelectSolo;
    }

    // Update is called once per frame
    void Update()
    {
        if (!interfazActiva) {
            scaleLogo.x -= (2.5f + contadorLogo) * Time.deltaTime;
            scaleLogo.y -= (2.5f + contadorLogo) * Time.deltaTime;
            scaleLogo.z -= (2.5f + contadorLogo) * Time.deltaTime;
            contadorLogo += 3 * Time.deltaTime;
            if (scaleLogo.x <= 1)
            {
                scaleLogo.x = 1;
                scaleLogo.y = 1;
                scaleLogo.z = 1;
                contadorLogo = 0;
                interfazActiva=true;
                zombieSoundFX.Play();
                selectSolo.SetActive(true);
            }
            logo.transform.localScale = scaleLogo;
        }
        else
        {
            colorImgSelectSolo.a = 0.1f + contadorLogo;
            contadorLogo += 2 * Time.deltaTime;
            imgSelectSolo.color = colorImgSelectSolo;
            if(colorImgSelectSolo.a >= 0.74f)
            {
                colorImgSelectSolo.a = 0.74f;
            }
            //Debug.Log("Ya se puede presionar botones");
            //
            switch (seleccion)
            {
                case 0:
                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        SceneManager.LoadScene("PruebasConMapa");
                    }

                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        selectExit.SetActive(true);
                        selectSolo.SetActive(false);
                        seleccion = 1;
                        changeSoundFX.Play();
                    }
                    break;
                case 1:
                    if (Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                    if (Input.GetKeyDown(KeyCode.UpArrow) )
                    {
                        selectExit.SetActive(false);
                        selectSolo.SetActive(true);
                        seleccion = 0;
                        changeSoundFX.Play();
                    }
                    break;
            }
            
        }
    }
}
