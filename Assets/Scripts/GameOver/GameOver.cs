using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TMP_Text textoScore;
    public TMP_Text textoKills;
    public TMP_Text textoRounds;
    public TMP_Text textoHeadshots;
    public TMP_Text textoPrincipal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        textoScore.text = ""+ DatosParaEnviarAGameOver.puntos;
        textoKills.text = DatosParaEnviarAGameOver.kills.ToString();
        textoHeadshots.text = DatosParaEnviarAGameOver.headshot_acertados.ToString();
        textoRounds.text = DatosParaEnviarAGameOver.RondaActual.ToString();
        if(DatosParaEnviarAGameOver.RondaActual == 1)
        {
            textoPrincipal.text = "YOU SURVIVED " + DatosParaEnviarAGameOver.RondaActual + " ROUND";
        }
        else
        {
            textoPrincipal.text = "YOU SURVIVED " + DatosParaEnviarAGameOver.RondaActual + " ROUNDS";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("Inicio");
        }

    }
}
