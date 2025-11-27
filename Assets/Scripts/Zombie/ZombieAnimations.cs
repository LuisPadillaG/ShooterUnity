using Unity.VisualScripting;
using UnityEngine;

public class ZombieAnimations : MonoBehaviour
{
    GameObject boxCollider;
    BoxCollider mixamoBrazoIzquierdo, mixamoBrazoDerecho;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = this.transform.GetChild(6).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(1).gameObject;
        boxCollider.SetActive(false);
        mixamoBrazoDerecho = this.transform.GetChild(6).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetComponent<BoxCollider>();
        mixamoBrazoIzquierdo = this.transform.GetChild(6).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<BoxCollider>();

    }
    public void AtaqueActivado() { 
        boxCollider.SetActive(true);
        Debug.Log("se activo el ataque");
        /*mixamoBrazoDerecho.enabled = false;
        mixamoBrazoIzquierdo.enabled = false;*/
    }
    public void AtaqueDesctivado()
    {
        boxCollider.SetActive(false);
        Debug.Log("ya no");
        /*mixamoBrazoDerecho.enabled = true;
        mixamoBrazoIzquierdo.enabled = true;*/
    }
}
