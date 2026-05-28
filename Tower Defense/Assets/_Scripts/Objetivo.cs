using UnityEngine;

public class Objetivo : MonoBehaviour, IAtacable
{
    public int vida = 100;

    public delegate void ObjetivoDestruido();
    public event ObjetivoDestruido EnObjetivoDestruido;

  public void RecibirDano(int dano = 20) 
    {
        vida -= dano;
        if (vida <= 0)
        {
            if (EnObjetivoDestruido != null)
            {
                EnObjetivoDestruido();
            }
            Destroy(this.gameObject,0.2f);
        }
    }
}
