using UnityEngine;

public class Objetivo : MonoBehaviour
{
    public int vida = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void RecibirDano(int dano = 20) 
    {
        vida -= dano;
        if (vida <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
