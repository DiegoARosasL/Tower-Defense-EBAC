using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{
    public List<GameObject> prefabsEnemigos;
    public int oleada;
    public List<int> enemigosPorOleada;

    private int enemigosDuranteEstaOleada;

    public bool laOleadaHaIniciado;
    public List<GameObject> EnemigosGenerados;

    public delegate void EstadoOleada();
    public event EstadoOleada EnOleadaIniciada;
    public event EstadoOleada EnOleadaTerminada;
    public event EstadoOleada EnOleadaGanada;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oleada = 0;
    }

    public void FixedUpdate()
    {
        if (laOleadaHaIniciado && EnemigosGenerados.Count == 0) 
        {
            GanarOla();
        }
    }

    private void GanarOla()
    {
        if (laOleadaHaIniciado && EnOleadaGanada != null) 
        {
            EnOleadaGanada();
            laOleadaHaIniciado = false;
        }
    }

    public void EmpezarOleada() 
    {
        laOleadaHaIniciado = true;
        if (EnOleadaIniciada != null) 
        {
            EnOleadaIniciada();
            ConfigurarCantidadDeEnemigos();
            InstanciarEnemigo();
        }
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
        var enemioTemporal = Instantiate(prefabsEnemigos[indiceAleatorio], transform.position, transform.rotation);
        EnemigosGenerados.Add(enemioTemporal);
        
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
