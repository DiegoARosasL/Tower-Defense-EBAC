using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{
    public List<GameObject> prefabsEnemigos;
    public int oleada;
    public List<int> enemigosPorOleada;

    private int enemigosDuranteEstaOleada;

    public delegate void OleadaTerminada();
    public event OleadaTerminada EnOleadaTerminada;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oleada = 0;
        ConfigurarCantidadDeEnemigos();
        InstanciarEnemigo();
    }
    
    public void TerminarOla() 
    {
        if (EnOleadaTerminada != null)
        {
            EnOleadaTerminada();
        }
    }

    public void ConfigurarCantidadDeEnemigos() 
    {
        enemigosDuranteEstaOleada = enemigosPorOleada[oleada];
    }

    public void InstanciarEnemigo() 
    {
        int indiceAleatorio = Random.Range(0, prefabsEnemigos.Count);
        Instantiate(prefabsEnemigos[indiceAleatorio], transform.position, transform.rotation);
        enemigosDuranteEstaOleada--;
        if (enemigosDuranteEstaOleada < 0) 
        {
            oleada++;
            ConfigurarCantidadDeEnemigos();
            TerminarOla();
            return;
        }
        Invoke("InstanciarEnemigo", 2);
    }
}
