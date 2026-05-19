using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject objetivo;
    private Animator anim;
    private NavMeshAgent agent;

    [Header("Atributos de Vida")]
    public int vidaMaxima = 100;
    private int vidaActual;

    private bool objetivoAlcanzado = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objetivo = GameObject.Find("Objetivo");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        vidaActual = vidaMaxima;

        // Inicia el movimiento hacia la base/jugador
        if (objetivo != null)
        {
            agent.SetDestination(objetivo.transform.position);
            anim.SetBool("IsMoving", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.dKey.wasPressedThisFrame && vidaActual > 0 && !objetivoAlcanzado)
        {
            RecibirDano(25);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Objetivo"))
        {
            objetivoAlcanzado = true;
            agent.isStopped = true; // Detiene el NavMeshAgent

            anim.SetBool("IsMoving", false);
            anim.SetTrigger("OnObjectiveReached"); // Activará "Laughing"
        }
    }

    public void RecibirDano(int dano)
    {
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

    void ActualizarEstadoAnimacion()
    {
        // Calcula el porcentaje de vida restante
        float porcentajeVida = (float)vidaActual / vidaMaxima;

        // Si pierde el primer golpe (ej. queda en 75% o menos) -> Corre
        if (porcentajeVida <= 0.75f && porcentajeVida > 0.30f)
        {
            anim.SetTrigger("OnRun");
            agent.speed = 5.5f; // Aumenta velocidad en NavMeshAgent
        }
        // Si le queda muy poca vida (30% o menos) -> Gatea
        else if (porcentajeVida <= 0.30f)
        {
            anim.SetTrigger("OnCrawl");
            agent.speed = 1.5f; // Se vuelve muy lento al gatear
        }
    }

    void Morir()
    {
        agent.isStopped = true;
        GetComponent<Collider>().enabled = false; // Evita colisiones muertas
        anim.SetTrigger("OnDeath"); // Activará "Prone Death"
    }

    public void Danar()
    {
        // Si el objetivo ya no existe en la escena, no hacemos nada
        if (objetivo == null) return;

        Objetivo scriptObjetivo = objetivo.GetComponent<Objetivo>();
        if (scriptObjetivo == null) return;

        // 1. Calculamos si el próximo golpe de 20 puntos va a destruir el cubo
        bool golpeDefinitivo = (scriptObjetivo.vida - 20) <= 0;

        // 2. Ejecutamos tu función del cubo (esta le baja vida y lo destruye si llega a 0)
        scriptObjetivo.RecibirDano(20);

        // 3. Si fue el golpe definitivo, activamos la risa del zombie
        if (golpeDefinitivo)
        {
            Debug.Log("¡Base destruida tras varios golpes continuos!");
            anim.SetTrigger("OnObjectiveDestroyed");
        }
    }
}
