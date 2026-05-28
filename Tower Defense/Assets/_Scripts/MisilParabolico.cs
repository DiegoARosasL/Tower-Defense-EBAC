using UnityEngine;

public class MisilParabolico : MonoBehaviour
{
    private Transform objetivoEnemigo;
    private Vector3 puntoInicial;
    private Vector3 puntoIntermedio;
    private Vector3 puntoFinal;

    private float tiempoTranscurrido = 0f;
    [SerializeField] private float velocidadMisil = 1.5f;
    [SerializeField] private float alturaParabola = 5f;
    [SerializeField] private int danoMisil = 25;

    public void InicializarMisil(Transform enemigo)
    {
        objetivoEnemigo = enemigo;
        puntoInicial = transform.position;
        puntoFinal = enemigo.position;

        Vector3 puntoMedioHorizontal = Vector3.Lerp(puntoInicial, puntoFinal, 0.5f);
        puntoIntermedio = puntoMedioHorizontal + Vector3.up * alturaParabola;
    }

    private void Update()
    {
        if (objetivoEnemigo != null)
        {
            puntoFinal = objetivoEnemigo.position;
        }

        tiempoTranscurrido += Time.deltaTime * velocidadMisil;

        if (tiempoTranscurrido >= 1f)
        {
            Impactar();
            return;
        }

        Vector3 m1 = Vector3.Lerp(puntoInicial, puntoIntermedio, tiempoTranscurrido);
        Vector3 m2 = Vector3.Lerp(puntoIntermedio, puntoFinal, tiempoTranscurrido);

        transform.position = Vector3.Lerp(m1, m2, tiempoTranscurrido);

        Vector3 direccion = (m2 - m1).normalized;
        if (direccion != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direccion) * Quaternion.Euler(90, 0, 0);
        }
    }

    private void Impactar()
    {
        if (objetivoEnemigo != null)
        {
            EnemigoBase scriptEnemigo = objetivoEnemigo.GetComponent<EnemigoBase>();
            if (scriptEnemigo != null)
            {
                scriptEnemigo.RecibirDano(danoMisil);
            }
        }

        Destroy(gameObject);
    }
}
