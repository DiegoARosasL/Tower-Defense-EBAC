using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoBase : MonoBehaviour, IAtacante, IAtacable
{
    [Header("Referencias Base")]
    protected GameObject objetivo;
    protected Animator Anim;
    protected NavMeshAgent agent;

    protected AdminJuego referenciaAdminJuego;
    protected SpawnerEnemigos referenciaSpawner;

    [Header("Atributos de Vida")]
    public int vidaMaxima = 100;
    protected int vidaActual;

    [Header("Configuracion Recompensas")]
    [SerializeField] protected int recursosGanados = 10;

    private bool yaMurio = false;

    protected bool objetivoAlcanzado = false;

    protected virtual void OnEnable()
    {
        GameObject adminObj = GameObject.Find("AdminJuego");
        if (adminObj != null) referenciaAdminJuego = adminObj.GetComponent<AdminJuego>();

        GameObject spawnerObj = GameObject.Find("SpawnerEnemigos");
        if (spawnerObj != null) referenciaSpawner = spawnerObj.GetComponent<SpawnerEnemigos>();

        if (objetivo != null)
        {
            Objetivo scriptObjetivo = objetivo.GetComponent<Objetivo>();
            if (scriptObjetivo != null) scriptObjetivo.EnObjetivoDestruido += Detener;
        }
    }

    protected virtual void OnDisable()
    {
        if (objetivo != null)
        {
            Objetivo scriptObjetivo = objetivo.GetComponent<Objetivo>();
            if (scriptObjetivo != null) scriptObjetivo.EnObjetivoDestruido -= Detener;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        objetivo = GameObject.Find("Objetivo");
        agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();

        vidaActual = vidaMaxima;

        // Inicia el movimiento hacia la base/jugador
        if (objetivo != null && agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(objetivo.transform.position);
            Anim.SetBool("IsMoving", true);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.dKey.wasPressedThisFrame && vidaActual > 0 && !objetivoAlcanzado)
        {
            RecibirDano(25);
        }

        if (vidaActual <= 0)
        {
            Anim.SetTrigger("OnDeath");
            if (agent != null && agent.isOnNavMesh) agent.SetDestination(transform.position);
            Destroy(gameObject, 3f);
        }
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Objetivo"))
        {
            objetivoAlcanzado = true;
            agent.isStopped = true; // Detiene el NavMeshAgent

            Anim.SetBool("IsMoving", false);
            Anim.SetTrigger("OnObjectiveReached"); // Activará "Laughing"
        }
    }

    public void RecibirDano(int dano)
    {
        if (yaMurio) return;

        vidaActual -= dano;
        Debug.Log("Vida del zombie: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir();
        }
        else
        {
            ActualizarEstadoAnimacion();
        }
    }

     protected virtual void Morir()
    {
        if (yaMurio) return;
        yaMurio = true;

        if (agent != null) agent.isStopped = true;

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Anim.SetTrigger("OnDeath");
    }

    private void Detener()
    {
        Anim.SetTrigger("OnObjectiveDestroyed");
        if (agent != null && agent.isOnNavMesh) agent.SetDestination(transform.position);
    }

    public virtual void Danar(int dano = 0)
    {
        if (objetivo == null) return;
        // Lógica base o vacía para sobreescribir en los hijos
    }

    protected virtual void ActualizarEstadoAnimacion()
    {
        // Vacío en la base, se hereda en los hijos
    }

    public virtual void OnDestroy()
    {
        if (referenciaAdminJuego != null) referenciaAdminJuego.ModificarRecursos(recursosGanados);
        if (referenciaSpawner != null) referenciaSpawner.EnemigosGenerados.Remove(this.gameObject);
    }

}
