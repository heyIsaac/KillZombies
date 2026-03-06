using UnityEngine;
using UnityEngine.AI;

public class ZumbiVida : MonoBehaviour
{
    public int vidas = 3;
    private Animator animator;
    private NavMeshAgent agente;
    private CapsuleCollider colisor;

    void Start()
    {
        // Pega todos os componentes necessários do zumbi
        animator = GetComponent<Animator>();
        agente = GetComponent<NavMeshAgent>();
        colisor = GetComponent<CapsuleCollider>();
    }

    public void ReceberDano()
    {
        if (vidas <= 0) return; // Se já morreu, ignora novos tiros

        vidas--;
        Debug.Log("Zumbi tomou um tiro! Vidas restantes: " + vidas);

        if (vidas <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Debug.Log("Zumbi eliminado!");

        // 1. Toca a animação de cair morto
        if (animator != null) animator.SetTrigger("morreu");

        // 2. Desliga o motor de passo para ele parar de te seguir instantaneamente
        if (agente != null) agente.enabled = false;

        // 3. Desliga o colisor para você conseguir caminhar por cima do corpo
        if (colisor != null) colisor.enabled = false;

        // 4. Destrói o corpo depois de 4 segundos para limpar a cena (otimização!)
        Destroy(gameObject, 4f);
    }
}
