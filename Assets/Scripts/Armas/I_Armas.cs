using UnityEngine;

public interface I_Armas
{
    public int ID { get; set; }
    public string Nombre { get; set; }
    public string Tipo { get; set; }
    public float Rango {  get; set; }
    public int Balas { get; set; }
    public int MaximoBalas {  get; set; }
    public int MunicionBalas { get; set; }
    
    public float DanoPorBala { get; set; }
    public float VelocidadPorBala { get; set; }
    public float Recoil {  get; set; }
    public float Precision {  get; set; }
    public int CostoPuntos { get; set; }

}
