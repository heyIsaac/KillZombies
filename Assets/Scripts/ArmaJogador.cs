using UnityEngine;
using UnityEngine.InputSystem;

// Controla o comportamento da arma do jogador, incluindo detecção de input,
// disparo de raycast (tiro) e reprodução de efeitos sonoros.
public class ArmaJogador : MonoBehaviour
{
    [Header("Configurações de Disparo")]
    // Distância máxima que o tiro da arma pode alcançar.
    [Tooltip("Distância máxima que o tiro da arma pode alcançar em metros.")]
    public float distanciaTiro = 50f;

    [Header("Configurações de Som")]
    // Arquivo de áudio reproduzido no momento do disparo.
    [Tooltip("Arraste o arquivo de som do tiro para cá.")]
    public AudioClip somTiro;

    // Controla o volume do efeito sonoro do disparo.
    [Range(0f, 1f)]
    [Tooltip("Volume do som do tiro (0 = mudo, 1 = volume máximo).")]
    public float volumeTiro = 0.1f;

    // Componentes internos privados (não visíveis no Inspector)
    private AudioSource tocadorSom;
    private Camera cam;

    // Inicializa as referências necessárias antes do primeiro frame.
    // Configura a câmera principal e cria um AudioSource dinamicamente para a arma.
    void Start()
    {
        // Obtém a referência da câmera que representa a visão do jogador
        cam = Camera.main;

        // Adiciona e configura o componente de áudio via código para manter a hierarquia limpa
        tocadorSom = gameObject.AddComponent<AudioSource>();
        tocadorSom.playOnAwake = false;
    }

    // Chamado a cada frame. Responsável por escutar as entradas (inputs) do jogador.
    void Update()
    {
        // Verifica se o botão esquerdo do mouse foi pressionado neste frame exato
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Atirar();
        }
    }

    // Executa a lógica de disparo: reproduz o som e lança um Raycast do centro da tela
    // para detectar colisões com entidades que possuem o componente ZumbiVida.
    void Atirar()
    {
        // Trava de segurança: se a câmera não for encontrada, interrompe o tiro para evitar erros
        if (cam == null) return;

        // Reproduz o efeito sonoro uma única vez sobrepondo os anteriores, aplicando o volume configurado
        if (somTiro != null)
        {
            tocadorSom.PlayOneShot(somTiro, volumeTiro);
        }

        // Calcula a origem (centro da câmera) e a direção (para frente) do tiro
        Ray raio = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit colisao;

        // Lança o raio invisível (Raycast) baseando-se na distância configurada
        if (Physics.Raycast(raio, out colisao, distanciaTiro))
        {
            // Tenta obter o componente de vida do objeto atingido
            ZumbiVida zumbiAtingido = colisao.collider.GetComponent<ZumbiVida>();

            // Se o objeto atingido for um zumbi (possuir o script), aplica o dano
            if (zumbiAtingido != null)
            {
                zumbiAtingido.ReceberDano();
            }
        }
    }
}
