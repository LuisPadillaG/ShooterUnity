using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorLoading : MonoBehaviour
{
    float contador;
    public TMP_Text textoLoading;
    public GameObject Continue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        contador = 0;
        textoLoading.text = "LOADING";
        Continue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if (contador > 0.5f)
        {
            textoLoading.text = "LOADING.";
        }
        if (contador > 1)
        {
            textoLoading.text = "LOADING..";
        }
        if(contador > 1.5f)
        {
            textoLoading.text = "LOADING...";

        }
        if (contador > 3)
        {
            textoLoading.text = "LOADING....";
            Continue.SetActive(true);
            SceneManager.LoadScene("PruebasConMapa");
        }

    }
}
