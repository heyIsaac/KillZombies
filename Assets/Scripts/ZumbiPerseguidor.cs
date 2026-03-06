using UnityEngine;
using UnityEngine.AI;

public class ZumbiPerseguidor : MonoBehaviour
{
    public Transform jogador;
    private NavMeshAgent agente;
    private Animator animator;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (jogador != null)
        {
            agente.SetDestination(jogador.position);

            // Se a velocidade do zumbi for maior que zero, ele ativa a animação de andar
            if (agente.velocity.magnitude > 0.1f)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
    }
}
