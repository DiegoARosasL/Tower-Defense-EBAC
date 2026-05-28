using UnityEngine;
using UnityEngine.AI;

public class Enemigo : EnemigoBase
{
    private bool yaCorrio = false;
    private bool yaGateo = false;

    private void Awake()
    {
        vidaMaxima = 100;
    }

    protected override void Update()
    {
        // 1. Ejecuta el Update de EnemigoBase (detecta la tecla D y revisa la muerte)
        base.Update();

        // 2. Ejecuta la lógica propia del enemigo común (animaciones de salud)
        if (vidaActual > 0 && !objetivoAlcanzado)
        {
            ActualizarEstadoAnimacion();
        }
    }

    protected override void ActualizarEstadoAnimacion()
    {
        float porcentajeVida = (float)vidaActual / vidaMaxima;

        if (porcentajeVida <= 0.75f && porcentajeVida > 0.30f && !yaCorrio)
        {
            Anim.SetTrigger("OnRun");
            agent.speed = 5.5f;
            yaCorrio = true;
        }

        else if (porcentajeVida <= 0.30f && !yaGateo)
        {
            Anim.SetTrigger("OnCrawl");
            agent.speed = 1.5f;
            yaGateo = true;
        }
    }

    public override void Danar(int dano = 0)
    {
        
        if (objetivo == null) return;

        Objetivo scriptObjetivo = objetivo.GetComponent<Objetivo>();
        if (scriptObjetivo == null) return;

        
        bool golpeDefinitivo = (scriptObjetivo.vida - 20) <= 0;

       
        scriptObjetivo.RecibirDano(20);

       
        if (golpeDefinitivo)
        {
            Debug.Log("¡Base destruida tras varios golpes continuos!");
            Anim.SetTrigger("OnObjectiveDestroyed");
        }
    }

    public override void OnDestroy()
    {
        // Ejecuta lo que pusimos en la base (dar recursos y quitarse del spawner)
        base.OnDestroy();

        // Lógica exclusiva del enemigo común: sumar al contador correcto
        if (referenciaAdminJuego != null)
        {
            referenciaAdminJuego.enemigosBaseDerrotados++;
        }
    }
}
