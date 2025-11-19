using Unity.Burst.Intrinsics;
using UnityEngine;

// PISTOLAS
public class M1911 : I_Armas
{
    public int ID { get; set; } = 0;
    public string Nombre { get; set; } = "M1911";

    public string Tipo { get; set; } = "Pistolas";

    public float Rango { get; set; } = 25f;
    public int Balas { get; set; } = 8;
    public float DanoPorBala { get; set; } = 15f;
    public float VelocidadPorBala { get; set; } = 0.2f;
    public float Recoil { get; set; } = 0.2f;
    public float Precision { get; set; } = 0.9f;
}
public class B23R : I_Armas
{
    public int ID { get; set; } = 1;
    public string Nombre { get; set; } = "B23R";

    public string Tipo { get; set; } = "Pistolas";
    public float Rango { get; set; } = 30f;
    public int Balas { get; set; } = 15;
    public float DanoPorBala { get; set; } = 25f;
    public float VelocidadPorBala { get; set; } = 80f;
    public float Recoil { get; set; } = 0.4f;
    public float Precision { get; set; } = 0.8f;
}
// ESCOPETAS
public class Remington870 : I_Armas
{
    public int ID { get; set; } = 2;
    public string Nombre { get; set; } = "Remington 870";
    public string Tipo { get; set; } = "Escopetas";

    public float Rango { get; set; } = 12f;
    public int Balas { get; set; } = 6;
    public float DanoPorBala { get; set; } = 40f;
    public float VelocidadPorBala { get; set; } = 55f;
    public float Recoil { get; set; } = 0.6f;
    public float Precision { get; set; } = 0.7f;
}

public class SPAS12 :I_Armas
{
    public int ID { get; set; } = 3;
    public string Nombre { get; set; } = "SPAS-12";
    public string Tipo { get; set; } = "Escopetas";

    public float Rango { get; set; } = 10f;
    public int Balas { get; set; } = 8;
    public float DanoPorBala { get; set; } = 30f;
    public float VelocidadPorBala { get; set; } = 60f;
    public float Recoil { get; set; } = 0.4f;
    public float Precision { get; set; } = 0.75f;
}
// SUBFUSILES
public class MP5 : I_Armas
{
    public int ID { get; set; } = 4;
    public string Nombre { get; set; } = "MP5";
    public string Tipo { get; set; } = "Subfusiles";

    public float Rango { get; set; } = 40f;
    public int Balas { get; set; } = 30;
    public float DanoPorBala { get; set; } = 20f;
    public float VelocidadPorBala { get; set; } = 90f;
    public float Recoil { get; set; } = 0.25f;
    public float Precision { get; set; } = 0.85f;
}

public class Uzi : I_Armas
{
    public int ID { get; set; } = 5;
    public string Nombre { get; set; } = "Uzi";
    public string Tipo { get; set; } = "Subfusiles";

    public float Rango { get; set; } = 35f;
    public int Balas { get; set; } = 32;
    public float DanoPorBala { get; set; } = 18f;
    public float VelocidadPorBala { get; set; } = 95f;
    public float Recoil { get; set; } = 0.35f;
    public float Precision { get; set; } = 0.75f;
}
// RIFLE DE ASALTO
public class AK47 : I_Armas
{
    public int ID { get; set; } = 6;
    public string Nombre { get; set; } = "AK-47";
    public string Tipo { get; set; } = "Rifles de asalto";

    public float Rango { get; set; } = 60f;
    public int Balas { get; set; } = 30;
    public float DanoPorBala { get; set; } = 35f;
    public float VelocidadPorBala { get; set; } = 0.1f;
    public float Recoil { get; set; } = 0.5f;
    public float Precision { get; set; } = 0.8f;
}

public class M16 :I_Armas
{
    public int ID { get; set; } = 7;
    public string Nombre { get; set; } = "M16";
    public string Tipo { get; set; } = "Rifles de asalto";

    public float Rango { get; set; } = 65f;
    public int Balas { get; set; } = 30;
    public float DanoPorBala { get; set; } = 28f;
    public float VelocidadPorBala { get; set; } = 105f;
    public float Recoil { get; set; } = 0.3f;
    public float Precision { get; set; } = 0.9f;
}
// KABOOOM
public class GranadaFragmentacion : I_Armas
{
    public int ID { get; set; } = 8;
    public string Nombre { get; set; } = "Granada de fragmentación";
    public string Tipo { get; set; } = "Explosivos";

    public float Rango { get; set; } = 10f;
    public int Balas { get; set; } = 1;
    public float DanoPorBala { get; set; } = 120f;
    public float VelocidadPorBala { get; set; } = 40f;
    public float Recoil { get; set; } = 0.3f;
    public float Precision { get; set; } = 0.9f;
}

public class Molotov : I_Armas
{
    public int ID { get; set; } = 9;
    public string Nombre { get; set; } = "Molotov";
    public string Tipo { get; set; } = "Explosivos";

    public float Rango { get; set; } = 8f;
    public int Balas { get; set; } = 1;
    public float DanoPorBala { get; set; } = 80f;
    public float VelocidadPorBala { get; set; } = 35f;
    public float Recoil { get; set; } = 0.2f;
    public float Precision { get; set; } = 0.8f;
}

public class MonkeyBombs : I_Armas
{
    public int ID { get; set; } = 10;
    public string Nombre { get; set; } = "Monkey Bombs";
    public string Tipo { get; set; } = "Explosivos";

    public float Rango { get; set; } = 9f;
    public int Balas { get; set; } = 1;
    public float DanoPorBala { get; set; } = 100f;
    public float VelocidadPorBala { get; set; } = 30f;
    public float Recoil { get; set; } = 0.1f;
    public float Precision { get; set; } = 1f;
}

// ARMAS ESPECIALES
public class RayGun :I_Armas
{
    public int ID { get; set; } = 11;
    public string Nombre { get; set; } = "Ray Gun";
    public string Tipo { get; set; } = "Armas especiales";

    public float Rango { get; set; } = 100f;
    public int Balas { get; set; } = 20;
    public float DanoPorBala { get; set; } = 90f;
    public float VelocidadPorBala { get; set; } = 200f;
    public float Recoil { get; set; } = 0.4f;
    public float Precision { get; set; } = 0.95f;
}

// CUERPO A CUERPO
public class CuchilloCombate :I_Armas
{
    public int ID { get; set; } = 12;
    public string Nombre { get; set; } = "Cuchillo de combate";
    public string Tipo { get; set; } = "Cuerpo a cuerpo";

    public float Rango { get; set; } = 2f;
    public int Balas { get; set; } = 0;
    public float DanoPorBala { get; set; } = 50f;
    public float VelocidadPorBala { get; set; } = 0f;
    public float Recoil { get; set; } = 0f;
    public float Precision { get; set; } = 1f;
}