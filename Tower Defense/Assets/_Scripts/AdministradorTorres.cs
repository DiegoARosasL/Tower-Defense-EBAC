using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class AdministradorTorres : MonoBehaviour
{
    public AdministradorToques referenciaAdminToques;

    public enum TorreSeleccionada 
    {
        Torre1,Torre2,Torre3,Torre4,Torre5
    }

    public TorreSeleccionada torreSeleccionada;
    public List<GameObject> prefabsTorres;

    private void OnEnable()
    {
        referenciaAdminToques.EnPlataformaTocada += CrearTorre;
    }

    private void OnDisable()
    {
        referenciaAdminToques.EnPlataformaTocada -= CrearTorre;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CrearTorre(GameObject plataforma)
    {
        if (plataforma.transform.childCount == 0)
        {
            Debug.Log("Creando Torre");
            int indiceTorre = (int)torreSeleccionada;
            Vector3 posParaInstanciar = plataforma.transform.position;
            posParaInstanciar.y += 0.5f;
            GameObject torreInstanciada = Instantiate<GameObject>(prefabsTorres[indiceTorre], posParaInstanciar, Quaternion.identity);
            torreInstanciada.transform.SetParent(plataforma.transform);
        }
    }

    public void ConfigurarTorre(int torre) 
    {
        if (Enum.IsDefined(typeof(TorreSeleccionada), torre)) 
        {
            torreSeleccionada = (TorreSeleccionada)torre;
        }
        else 
        {
            Debug.LogError("esa torre no esta definida");
        }
    }
}
