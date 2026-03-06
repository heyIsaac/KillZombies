using UnityEngine;
using UnityEngine.InputSystem;

public class InteracaoJogador : MonoBehaviour
{
    public float distanciaInteracao = 3f;
    public LayerMask layerPorta;
    private Camera cam;

    void Start()
    {
        cam = Camera.main; // Usa a câmera principal do jogador
    }

    void Update()
    {
        // Verifica se a tecla 'E' foi pressionada neste exato frame usando o sistema novo
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            TentarInteragir();
        }
    }

    void TentarInteragir()
    {
        // Cria um raio invisível que sai do centro da câmera para frente
        Ray raio = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit colisao;

        // Se o raio bater em algo na distância certa e que seja da Layer configurada...
        if (Physics.Raycast(raio, out colisao, distanciaInteracao, layerPorta))
        {
            // Tenta achar o script "Porta" no objeto que o raio bateu
            Porta portaEncontrada = colisao.collider.GetComponentInParent<Porta>();

            if (portaEncontrada != null)
            {
                portaEncontrada.AlternarPorta();
            }
        }
    }
}
