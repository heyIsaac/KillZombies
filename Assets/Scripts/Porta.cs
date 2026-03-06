using UnityEngine;

public class Porta : MonoBehaviour
{
    private Animator animator;
    private bool estadoAberta = false;

    void Start()
    {
        // Pega o cérebro de animação que acabamos de configurar
        animator = GetComponent<Animator>();
    }

    public void AlternarPorta()
    {
        estadoAberta = !estadoAberta; // Se está fechada, abre. Se está aberta, fecha.
        animator.SetBool("isAberta", estadoAberta);
    }
}
