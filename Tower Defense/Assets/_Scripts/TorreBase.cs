using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class TorreBase : MonoBehaviour
{
    public GameObject enemigo;
    public GameObject prefabBala;
    public List<GameObject> puntasCanon;

    private void Update()
    {
        if (enemigo != null) 
        {
            Apuntar();
        }
        else
        {
            // ⚠️ Si el enemigo murió, busca otro en este preciso frame
            BuscarObjetivoInmediato();
        }
    }

    public void Apuntar()
    {
        transform.LookAt(enemigo.transform);
    }

    public virtual void Disparar()
    {
        foreach (GameObject punta in puntasCanon)
        {
            var tempBala = Instantiate<GameObject>(prefabBala, punta.transform.position, Quaternion.identity);
            tempBala.GetComponent<Bala>().destino = enemigo.transform.position;
        }
    }

    public void BuscarObjetivoInmediato()
    {
        // Buscamos el script administrador en la escena para acceder a los enemigos
        var admin = Object.FindAnyObjectByType<AdministradorTorres>();
        if (admin != null && admin.referenciaSpawner != null && admin.referenciaSpawner.laOleadaHaIniciado)
        {
            float distanciaMasCorta = float.MaxValue;
            GameObject enemigoMasCercano = null;

            foreach (GameObject ene in admin.referenciaSpawner.EnemigosGenerados)
            {
                if (ene == null) continue;

                float dist = Vector3.Distance(ene.transform.position, transform.position);
                if (dist < distanciaMasCorta)
                {
                    distanciaMasCorta = dist;
                    enemigoMasCercano = ene;
                }
            }

            // Si encontró uno nuevo, lo asigna de inmediato
            if (enemigoMasCercano != null)
            {
                enemigo = enemigoMasCercano;
            }
        }
    }
}
