using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdministradorUI : MonoBehaviour
{
    public GameObject canvasPrincipal;
    public GameObject menuGameOver;
    public GameObject MenuOlaGanada;
    public GameObject MensajeFinOla;
    public SpawnerEnemigos referenciaSpawner;
    public Objetivo referenciaObjetivo;
    public AdminJuego referenciaAdminJuego;
    public TMPro.TMP_Text textoRecursos;
    public TMPro.TMP_Text textoOleada;
    public TMPro.TMP_Text textoEnemigos;
    public TMPro.TMP_Text textoJefes;


    private void OnEnable()
    {
        referenciaObjetivo.EnObjetivoDestruido += MostrarMenuGameOver;
        referenciaSpawner.EnOleadaIniciada += ActualizarOla;
        referenciaSpawner.EnOleadaTerminada += MostrarMensajeUltimoEnemigo;
        referenciaSpawner.EnOleadaGanada += MostrarMenuOlaGanada;
        referenciaAdminJuego.EnRecursosModificados += ActualizarRecursos;
    }

    private void OnDisable()
    {
        referenciaObjetivo.EnObjetivoDestruido -= MostrarMenuGameOver;
        referenciaSpawner.EnOleadaIniciada -= ActualizarOla;
        referenciaSpawner.EnOleadaTerminada -= MostrarMensajeUltimoEnemigo;
        referenciaSpawner.EnOleadaGanada -= MostrarMenuOlaGanada;
        referenciaAdminJuego.EnRecursosModificados -= ActualizarRecursos;
    }

    public void ActualizarRecursos()
    {
        textoRecursos.text = $"Recursos: {referenciaAdminJuego.recursos}";
    }

    public void MostrarMensajeUltimoEnemigo()
    {
        MensajeFinOla.SetActive(true);
        Invoke("OcultarMensajeUltimoEnemigo", 3);
    }

    public void OcultarMensajeUltimoEnemigo() 
    {
        MensajeFinOla.SetActive(false);
    }

    public void MostrarMenuOlaGanada()
    {
        textoEnemigos.text = $"ENEMIGOS: {referenciaAdminJuego.enemigosBaseDerrotados}";
        textoJefes.text = $"JEFES: {referenciaAdminJuego.enemigosJefeDerrotados}";
        MenuOlaGanada.SetActive(true);
    }

    public void OcultarMenuOlaGanada() 
    {
        MenuOlaGanada.SetActive(false);
    }
    public void ActualizarOla()
    {
        textoOleada.text = $"Oleada: {referenciaSpawner.oleada}";
        OcultarMenuOlaGanada();
    }

    public void MostrarMenuFinOleada() 
    {
    
    }

    public void OcultarMenuFinOleada() 
    {
    
    }

    public void MostrarMenuGameOver() 
    {
        menuGameOver.SetActive(true);
    }

    public void OcultarMenuGameOver() 
    {
        menuGameOver.SetActive(false);
    }

    public void FinalizarJuego() 
    {
        Application.Quit();
    }

    public void CargarMenuPrincipal() 
    {
        SceneManager.LoadScene(0);
    }

    public void ReintentarNivel() 
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(escenaActual);
    }
}
