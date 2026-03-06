using UnityEngine;
using UnityEngine.InputSystem;

// Controla o sistema de interação do jogador com objetos do cenário
public class InteracaoJogador : MonoBehaviour
{
    // Define até qual distância o jogador consegue interagir com a porta
    public float distanciaInteracao = 5f;

    // Filtro para o raio acertar apenas objetos que sejam portas (Layer Interagivel)
    public LayerMask layerPorta;

    // Guarda a referência da câmera do jogador
    private Camera cam;

    // Chamado uma vez quando o jogo começa
    void Start()
    {
        // Busca automaticamente a câmera principal da cena
        cam = Camera.main;

        // Avisa no console caso a câmera principal não seja encontrada
        if (cam == null)
        {
            Debug.LogError("ERRO: Câmera não encontrada! A câmera do jogador precisa ter a tag 'MainCamera'.");
        }
    }

    // Chamado repetidamente a cada frame do jogo
    void Update()
    {
        // Verifica se o teclado existe e se a tecla 'E' foi pressionada exatamente neste frame
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("1. Tecla E pressionada!");

            // Chama a função que dispara o raio de interação
            TentarInteragir();
        }
    }

    // Lógica responsável por atirar um raio invisível e ver se bateu numa porta
    void TentarInteragir()
    {
        // Aborta a função se a câmera não existir para não causar erros
        if (cam == null) return;

        // Cria um raio invisível saindo da posição da câmera e indo para frente
        Ray raio = new Ray(cam.transform.position, cam.transform.forward);

        // Variável que vai guardar as informações do que o raio bater
        RaycastHit colisao;

        // Desenha um laser vermelho na aba SCENE para você ver para onde está olhando
        Debug.DrawRay(raio.origin, raio.direction * distanciaInteracao, Color.red, 2f);

        // Dispara o raio e verifica se ele colidiu com algo que tenha a Layer configurada (layerPorta)
        if (Physics.Raycast(raio, out colisao, distanciaInteracao, layerPorta))
        {
            Debug.Log("2. O raio BATEU EM: " + colisao.collider.gameObject.name);

            // Tenta encontrar o script 'Porta' no objeto atingido ou no "pai" dele
            Porta portaEncontrada = colisao.collider.GetComponentInParent<Porta>();

            // Se encontrou o script, significa que é uma porta válida
            if (portaEncontrada != null)
            {
                Debug.Log("3. Script da Porta ENCONTRADO! Acionando...");

                // Aciona a animação de abrir ou fechar
                portaEncontrada.AlternarPorta();
            }
            else
            {
                // Avisa se bateu num objeto com a Layer certa, mas que não tem o script da porta
                Debug.LogWarning("O raio bateu na porta, mas não achou o script 'Porta.cs' no objeto pai.");
            }
        }
        else
        {
            // Avisa que o raio não acertou nada interativo dentro da distância permitida
            Debug.Log("O raio foi disparado, mas não acertou nada que tenha a Layer 'Interagivel'.");
        }
    }
}
