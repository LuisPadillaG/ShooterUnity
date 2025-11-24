using UnityEngine;

// PISTOLAS
public class M1911 : I_Armas
{
    public int ID { get; set; } = 0;
    public string Nombre { get; set; } = "M1911";

    public string Tipo { get; set; } = "Pistolas"; 
    public float Rango { get; set; } = 25f;
    public int Balas { get; set; } = 8;
    public int MaximoBalas { get; set; } = 8;
    public int MunicionBalas { get; set; } = 32;
    public float DanoPorBala { get; set; } = 20f;
    public float VelocidadPorBala { get; set; } = 0.2f;
    public float Recoil { get; set; } = -70f;
    public float Precision { get; set; } = 0.9f;
    public int CostoPuntos { get; set; } = 100;
}
public class B23R : I_Armas
{
    public int ID { get; set; } = 1;
    public string Nombre { get; set; } = "B23R";

    public string Tipo { get; set; } = "Pistolas";
    public float Rango { get; set; } = 30f;
    public int Balas { get; set; } = 15;
    public int MaximoBalas { get; set; } = 15;
    public int MunicionBalas { get; set; } = 45;
    public float DanoPorBala { get; set; } = 120f;
    public float VelocidadPorBala { get; set; } = 0.2f;
    public float Recoil { get; set; } = -140f;
    public float Precision { get; set; } = 0.8f;
    public int CostoPuntos { get; set; } = 750;
}
// ESCOPETAS
public class Remington870 : I_Armas
{
    public int ID { get; set; } = 2;
    public string Nombre { get; set; } = "Remington 870";
    public string Tipo { get; set; } = "Escopetas"; 
    public float Rango { get; set; } = 12f;
    public int Balas { get; set; } = 8;
    public int MaximoBalas { get; set; } = 8;
    public int MunicionBalas { get; set; } = 32;
    public float DanoPorBala { get; set; } = 300f;
    public float VelocidadPorBala { get; set; } = 0.8f;
    public float Recoil { get; set; } = -210f;
    public float Precision { get; set; } = 0.7f;
    public int CostoPuntos { get; set; } = 500;
}

public class SPAS12 :I_Armas
{
    public int ID { get; set; } = 3;
    public string Nombre { get; set; } = "SPAS-12";
    public string Tipo { get; set; } = "Escopetas"; 
    public float Rango { get; set; } = 10f;
    public int Balas { get; set; } = 12;
    public int MaximoBalas { get; set; } = 12;
    public int MunicionBalas { get; set; } = 48;
    public float DanoPorBala { get; set; } = 160f;
    public float VelocidadPorBala { get; set; } = 0.8f;
    public float Recoil { get; set; } = -140f;
    public float Precision { get; set; } = 0.75f;
    public int CostoPuntos { get; set; } = 750;
}
// SUBFUSILES
public class MP5 : I_Armas
{
    public int ID { get; set; } = 4;
    public string Nombre { get; set; } = "MP5";
    public string Tipo { get; set; } = "Subfusiles"; 
    public float Rango { get; set; } = 40f;
    public int Balas { get; set; } = 30;
    public int MaximoBalas { get; set; } = 30;
    public int MunicionBalas { get; set; } = 120;
    public float DanoPorBala { get; set; } = 100f;
    public float VelocidadPorBala { get; set; } = 0.09f;
    public float Recoil { get; set; } = -87.5f;
    public float Precision { get; set; } = 0.85f;
    public int CostoPuntos { get; set; } = 1000;
}

public class Uzi : I_Armas
{
    public int ID { get; set; } = 5;
    public string Nombre { get; set; } = "Uzi";
    public string Tipo { get; set; } = "Subfusiles";
    public float Rango { get; set; } = 35f;
    public int Balas { get; set; } = 24;
    public int MaximoBalas { get; set; } = 24;
    public int MunicionBalas { get; set; } = 96;
    public float DanoPorBala { get; set; } = 80f;
    public float VelocidadPorBala { get; set; } = 0.08f;
    public float Recoil { get; set; } = -122.5f;
    public float Precision { get; set; } = 0.75f;
    public int CostoPuntos { get; set; } = 500;
}
// RIFLE DE ASALTO
public class AK47 : I_Armas
{
    public int ID { get; set; } = 6;
    public string Nombre { get; set; } = "AK-47";
    public string Tipo { get; set; } = "Rifles de asalto"; 
    public float Rango { get; set; } = 60f;
    public int Balas { get; set; } = 35;
    public int MaximoBalas { get; set; } = 35;
    public int MunicionBalas { get; set; } = 140;
    public float DanoPorBala { get; set; } = 120f;
    public float VelocidadPorBala { get; set; } = 0.1f;
    public float Recoil { get; set; } = -175f;
    public float Precision { get; set; } = 0.8f;
    public int CostoPuntos { get; set; } = 500;
}

public class M16 :I_Armas
{
    public int ID { get; set; } = 7;
    public string Nombre { get; set; } = "M16";
    public string Tipo { get; set; } = "Rifles de asalto"; 
    public float Rango { get; set; } = 65f;
    public int Balas { get; set; } = 30;
    public int MaximoBalas { get; set; } = 30;
    public int MunicionBalas { get; set; } = 120;
    public float DanoPorBala { get; set; } = 120f;
    public float VelocidadPorBala { get; set; } = 0.12f;
    public float Recoil { get; set; } = -35f;
    public float Precision { get; set; } = 0.9f;
    public int CostoPuntos { get; set; } = 500;
}
// KABOOOM
public class GranadaFragmentacion : I_Armas
{
    public int ID { get; set; } = 8;
    public string Nombre { get; set; } = "Granada de fragmentación";
    public string Tipo { get; set; } = "Explosivos";

    public float Rango { get; set; } = 10f;
    public int Balas { get; set; } = 1;
    public int MaximoBalas { get; set; } = 1;
    public int MunicionBalas { get; set; } = 3;
    public float DanoPorBala { get; set; } = 120f;
    public float VelocidadPorBala { get; set; } = 40f;
    public float Recoil { get; set; } = 0.3f;
    public float Precision { get; set; } = 0.9f;
    public int CostoPuntos { get; set; } = 500;
}

public class Molotov : I_Armas
{
    public int ID { get; set; } = 9;
    public string Nombre { get; set; } = "Molotov";
    public string Tipo { get; set; } = "Explosivos";

    public float Rango { get; set; } = 8f;
    public int Balas { get; set; } = 1;
    public int MaximoBalas { get; set; } = 1;
    public int MunicionBalas { get; set; } = 3;
    public float DanoPorBala { get; set; } = 80f;
    public float VelocidadPorBala { get; set; } = 35f;
    public float Recoil { get; set; } = 0.2f;
    public float Precision { get; set; } = 0.8f;
    public int CostoPuntos { get; set; } = 500;
}

public class MonkeyBombs : I_Armas
{
    public int ID { get; set; } = 10;
    public string Nombre { get; set; } = "Monkey Bombs";
    public string Tipo { get; set; } = "Explosivos"; 
    public float Rango { get; set; } = 9f;
    public int Balas { get; set; } = 1;
    public int MaximoBalas { get; set; } = 1;
    public int MunicionBalas { get; set; } = 3;
    public float DanoPorBala { get; set; } = 100f;
    public float VelocidadPorBala { get; set; } = 30f;
    public float Recoil { get; set; } = 0.1f;
    public float Precision { get; set; } = 1f;
    public int CostoPuntos { get; set; } = 500;
}

// ARMAS ESPECIALES
public class RayGun :I_Armas
{
    public int ID { get; set; } = 11;
    public string Nombre { get; set; } = "Ray Gun";
    public string Tipo { get; set; } = "Armas especiales";
    public float Rango { get; set; } = 100f;
    public int Balas { get; set; } = 20;
    public int MaximoBalas { get; set; } = 20;
    public int MunicionBalas { get; set; } = 1000;
    public float DanoPorBala { get; set; } = 1500f;
    public float VelocidadPorBala { get; set; } = 0.15f;
    public float Recoil { get; set; } = 0.4f;
    public float Precision { get; set; } = 0.95f;
    public int CostoPuntos { get; set; } = 500;
}

// CUERPO A CUERPO
public class CuchilloCombate :I_Armas
{
    public int ID { get; set; } = 12;
    public string Nombre { get; set; } = "Cuchillo de combate";
    public string Tipo { get; set; } = "Cuerpo a cuerpo";
    public float Rango { get; set; } = 2f;
    public int Balas { get; set; } = 0;
    public int MaximoBalas { get; set; } = 0;
    public int MunicionBalas { get; set; } = 0;
    public float DanoPorBala { get; set; } = 50f;
    public float VelocidadPorBala { get; set; } = 0f;
    public float Recoil { get; set; } = 0f;
    public float Precision { get; set; } = 1f;
    public int CostoPuntos { get; set; } = 500;
}