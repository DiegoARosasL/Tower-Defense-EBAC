using UnityEngine;
using UnityEngine.AI;

public class Boss : EnemigoBase
{
    private void Awake()
    {
        vidaMaxima = 500;
        recursosGanados = 100;
    }

    public override void Danar(int dano = 0)
    {
        if (objetivo == null) return;

        Objetivo scriptObjetivo = objetivo.GetComponent<Objetivo>();
        if (scriptObjetivo == null) return;

        // El jefe hace mucho más daño que un enemigo común (ejemplo: 40)
        bool golpeDefinitivo = (scriptObjetivo.vida - 40) <= 0;

        scriptObjetivo.RecibirDano(40);

        if (golpeDefinitivo)
        {
            Debug.Log("¡El Boss ha destruido la base!");
            Anim.SetTrigger("OnObjectiveDestroyed");
        }
    }

    // Sobrescribimos el OnDestroy para afectar al contador de Jefes
    public override void OnDestroy()
    {
        // Ejecuta la lógica base (da los 100 recursos y se elimina del spawner)
        base.OnDestroy();

        // Lógica exclusiva del Boss: suma al contador de jefes derrotados
        if (referenciaAdminJuego != null)
        {
            referenciaAdminJuego.enemigosJefeDerrotados++;
        }
    }
}
