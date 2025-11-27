using UnityEngine;
using TMPro; 
using UnityEngine.UI;
using System.Collections;

public class ControladorUI_Juego : MonoBehaviour
{
    GameObject DatosJuego;
    DatosJugador datosJugador;
    public TMP_Text textoKills;
    public TMP_Text textoPoints;
    public TMP_Text textoNombreArmaJugador;
    public TMP_Text textoBalasArmaJugador;
    public TMP_Text textoRonda;
    public GameObject PerkHealth;
    public GameObject PerkSpeedCola;
    public GameObject PerkBalas;
    public TMP_Text textoPrecio;
    GameObject pressEnter;
    AudioSource audio_nuevaRonda;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DatosJuego = GameObject.Find("DatosJuego");
        datosJugador = DatosJuego.GetComponent<DatosJugador>();
        pressEnter = GameObject.FindWithTag("CanvaAction");
        pressEnter.SetActive(false);
        PerkHealth.SetActive(false);
        PerkSpeedCola.SetActive(false);
        PerkBalas.SetActive(false);
        audio_nuevaRonda = this.transform.GetChild(0).GetChild(1).GetComponent<AudioSource>();
        textoPrecio.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        textoKills.text = "Kills: " + datosJugador.kills;
        textoPoints.text = "Points: " + datosJugador.puntos;
        textoNombreArmaJugador.text = "" + datosJugador.armasJugador[datosJugador.armaActual].Nombre;
        textoBalasArmaJugador.text = "" + datosJugador.armasJugador[datosJugador.armaActual].Balas + "/" + datosJugador.armasJugador[datosJugador.armaActual].MunicionBalas;
    }
    public void ActivarPressEnter(int precio)
    {
        pressEnter.SetActive(true);
        textoPrecio.text = precio.ToString();
    }
    public void DesactivarPressEnter()
    {
        pressEnter.SetActive(false);
        textoPrecio.text = "";
    }
    public void ActivarPerkHealth()
    {
        PerkHealth.SetActive(true);
    }
    public void DesactivarPerkHealth()
    {
        PerkHealth.SetActive(false);
    }
    public void ActivarPerkSpeedCola()
    {
        PerkSpeedCola.SetActive(true);
    }
    public void DesactivarSpeedCola()
    {
        PerkSpeedCola.SetActive(false);
    }
    public void ActivarPerkBalas()
    {
        PerkBalas.SetActive(true);
    }
    public void DesactivarPerkBalas()
    {
        PerkBalas.SetActive(false);
    }
    public void CambioRonda(int numero)
    {
        StartCoroutine(AnimacionCambioRonda(numero));
    }

    private IEnumerator AnimacionCambioRonda(int numero)
    {
        textoRonda.gameObject.SetActive(true);  // SIEMPRE visible 

        // Valores base
        Color baseColor = textoRonda.color;
        Color rojo = new Color(1, 0.1f, 0.1f);

        // Escalas suaves
        Vector3 escalaInicial = textoRonda.transform.localScale;
        Vector3 escalaMin = escalaInicial * 0.85f;
        Vector3 escalaMax = escalaInicial * 1.15f;

        // Reset visual
        textoRonda.color = new Color(baseColor.r, baseColor.g, baseColor.b, 0f);
        textoRonda.transform.localScale = escalaMin;

        // =============================
        // 1. FLASH inicial (0.15s)
        // =============================
        for (float t = 0; t < 0.15f; t += Time.deltaTime)
        {
            float a = t / 0.15f;
            textoRonda.color = Color.Lerp(Color.white, baseColor, a);
            textoRonda.transform.localScale = Vector3.Lerp(escalaMin, escalaMax, a);
            yield return null;
        }

        // =============================
        // 2. Fade-in + pulso suave (0.6s)
        // =============================
        for (float t = 0; t < 0.6f; t += Time.deltaTime)
        {
            float a = t / 0.6f;
            textoRonda.color = new Color(baseColor.r, baseColor.g, baseColor.b, a);
            textoRonda.transform.localScale = Vector3.Lerp(escalaMax, escalaInicial, a);
            yield return null;
        }

        textoRonda.color = new Color(baseColor.r, baseColor.g, baseColor.b, 1f);
        textoRonda.transform.localScale = escalaInicial;

        // =============================
        // 3. Vibración SIN mover posición real (2s)
        // =============================
        float duracion = 2f;
        float intensidad = 2.2f; // en píxeles, súper leve

        Vector3 posOriginal = textoRonda.rectTransform.anchoredPosition;

        for (float t = 0; t < duracion; t += Time.deltaTime)
        {
            float flash = Mathf.PingPong(t * 12f, 1f);
            textoRonda.color = Color.Lerp(baseColor, rojo, flash);

            // Vibración visual sin cambiar posición real
            textoRonda.rectTransform.anchoredPosition =
                posOriginal + new Vector3(
                    Random.Range(-intensidad, intensidad),
                    Random.Range(-intensidad, intensidad)
                );

            yield return null;
        }
         
        textoRonda.rectTransform.anchoredPosition = posOriginal;
        textoRonda.color = baseColor;
         

        textoRonda.color = new Color(baseColor.r, baseColor.g, baseColor.b, 1f);
         
        textoRonda.text = "ROUND " + datosJugador.RondaActual;
        datosJugador.cambiandoLaRonda = false;
        audio_nuevaRonda.Play();
    }



}
