using UnityEngine;

// Controla o comportamento de abrir e fechar da porta animada
public class Porta : MonoBehaviour
{
    // Guarda a referência do componente de animação da porta
    private Animator animator;

    // Variável que controla se a porta está atualmente aberta (true) ou fechada (false)
    private bool estadoAberta = false;

    // Chamado uma vez quando o jogo começa
    void Start()
    {
        // Busca o componente Animator que está anexado a este mesmo objeto
        animator = GetComponent<Animator>();

        // Avisa no console caso o Animator não seja encontrado, para evitar bugs invisíveis
        if (animator == null)
        {
            Debug.LogError("ERRO: O script Porta não encontrou o componente Animator na Porta_Automatica!");
        }
    }

    // Função pública que pode ser chamada por outros scripts (como o InteracaoJogador)
    public void AlternarPorta()
    {
        // Inverte o estado atual da porta (se for false vira true, se for true vira false)
        estadoAberta = !estadoAberta;

        // Mostra no console o estado atual para ajudar nos testes do jogo
        Debug.Log("4. A porta agora deve estar: " + (estadoAberta ? "ABERTA" : "FECHADA"));

        // Se o componente de animação foi encontrado com sucesso...
        if (animator != null)
        {
            // ...atualiza a regra "isAberta" lá na janela do Animator para rodar a animação certa
            animator.SetBool("isAberta", estadoAberta);
        }
    }
}
