using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public GameObject objetivo;
    public int vida = 100;

    public Animator Anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(objetivo.transform.position);
        Anim = GetComponent<Animator>();
        Anim.SetBool("IsMoving", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Objetivo") 
        {
            Anim.SetBool("IsMoving", false);
            Anim.SetTrigger("OnObjectiveReached");
        }
    }

    public void Danar() 
    {
        objetivo.GetComponent<Objetivo>().RecibirDano(40);
    }

    public void RecibirDano(int dano = 5) 
    {
        vida -= dano;
    }
}
