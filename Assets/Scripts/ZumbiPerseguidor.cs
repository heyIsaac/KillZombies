using UnityEngine;
using UnityEngine.AI;

// Controla a inteligência do zumbi para perseguir o alvo e ativar animações
public class ZumbiPerseguidor : MonoBehaviour
{
    // O alvo que o zumbi vai seguir (arrastamos o jogador para cá no Inspector)
    public Transform jogador;

    // O "motor" invisível que faz o zumbi andar no chão azul e desviar de paredes
    private NavMeshAgent agente;

    // O cérebro das animações do zumbi
    private Animator animator;

    // Chamado uma vez quando o jogo começa
    void Start()
    {
        // Pega os componentes automaticamente no próprio objeto do zumbi
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Chamado repetidamente a cada frame do jogo
    void Update()
    {
        // Garante que o jogador foi configurado no script para evitar erros
        if (jogador != null)
        {
            // Manda o zumbi ir até a posição atual do jogador o tempo todo
            agente.SetDestination(jogador.position);

            // Se a velocidade atual do zumbi for maior que quase zero, significa que ele está em movimento
            if (agente.velocity.magnitude > 0.1f)
            {
                // Avisa o Animator para ligar a animação de caminhar
                animator.SetBool("isWalking", true);
            }
            else
            {
                // Se estiver parado (esbarrou em algo ou chegou no alvo), desliga a animação de caminhar
                animator.SetBool("isWalking", false);
            }
        }
    }
}
