using UnityEngine;

public class TorreMisiles : TorreBase
{
    [SerializeField] private float tiempoEntreDisparos = 2f;
    private float cronometroDisparo = 0f;

    private int indiceCañónActual = 0;

    private void Update()
    {
        if (enemigo != null)
        {
            Apuntar();

            
        }
        else
        {
            BuscarObjetivoInmediato();
        }
    }

    private void DispararMisilParabolico()
    {
        if (prefabBala != null && puntasCanon != null && puntasCanon.Count > 0)
        {
            // Aseguramos que el índice no se salga de los límites de la lista por seguridad
            if (indiceCañónActual >= puntasCanon.Count)
            {
                indiceCañónActual = 0;
            }

            // Tomamos el cañón que toca (Element 0 o Element 1)
            GameObject puntoDisparo = puntasCanon[indiceCañónActual];

            if (puntoDisparo != null)
            {
                // Instanciamos el misil en la posición del cañón actual
                GameObject nuevoMisil = Instantiate(prefabBala, puntoDisparo.transform.position, Quaternion.identity);

                MisilParabolico scriptMisil = nuevoMisil.GetComponent<MisilParabolico>();
                if (scriptMisil != null)
                {
                    scriptMisil.InicializarMisil(enemigo.transform);
                }

                // 🔄 Cambiamos al siguiente cañón para el próximo disparo
                indiceCañónActual++;
                if (indiceCañónActual >= puntasCanon.Count)
                {
                    indiceCañónActual = 0; // Regresa al primer cañón
                }
            }
        }
    }

    public override void Disparar()
    {
        DispararMisilParabolico();
    }
}
