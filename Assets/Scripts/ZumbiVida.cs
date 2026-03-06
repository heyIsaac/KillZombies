using UnityEngine;
using UnityEngine.AI;

// Gerencia a vida, o recebimento de dano e a sequência de morte do zumbi
public class ZumbiVida : MonoBehaviour
{
    // Quantidade de tiros necessários para o zumbi ser eliminado
    public int vidas = 3;

    // Referências para os componentes do zumbi
    private Animator animator;
    private NavMeshAgent agente;
    private CapsuleCollider colisor;

    // Chamado uma vez quando o jogo começa
    void Start()
    {
        // Pega todos os componentes que estão no próprio zumbi para usarmos depois
        animator = GetComponent<Animator>();
        agente = GetComponent<NavMeshAgent>();
        colisor = GetComponent<CapsuleCollider>();
    }

    // Função pública chamada pela arma do jogador quando um tiro acerta este zumbi
    public void ReceberDano()
    {
        // Se a vida já acabou, sai da função na hora para não rodar a morte duas vezes
        if (vidas <= 0) return;

        // Tira 1 ponto de vida
        vidas--;

        // Mostra no console a vida restante para ajudar nos testes
        Debug.Log("Zumbi tomou um tiro! Vidas restantes: " + vidas);

        // Verifica se a vida chegou a zero após este tiro
        if (vidas <= 0)
        {
            Morrer();
        }
    }

    // Sequência de ações de quando o zumbi é derrotado
    void Morrer()
    {
        Debug.Log("Zumbi eliminado!");

        // 1. Toca a animação de cair morto mudando a regra lá no Animator
        if (animator != null) animator.SetTrigger("morreu");

        // 2. Desliga o motor de passo para ele parar de seguir o jogador instantaneamente
        if (agente != null) agente.enabled = false;

        // 3. Desliga a colisão física para o jogador conseguir caminhar por cima do corpo sem tropeçar
        if (colisor != null) colisor.enabled = false;

        // 4. Destrói o corpo do jogo depois de 4 segundos para limpar o cenário e não pesar o celular (otimização!)
        Destroy(gameObject, 4f);
    }
}
