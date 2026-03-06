# 🧟‍♂️ VR KillZombies

![Unity](https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Meta Quest](https://img.shields.io/badge/Meta_Quest-045FEE?style=for-the-badge&logo=meta&logoColor=white)

## 📖 Sobre o Projeto

**Sanctuary** é um protótipo de ambiente interativo em primeira pessoa desenvolvido na Unity, criado com foco em otimização e imersão para **Realidade Virtual (Meta Quest)**. O projeto foi construído para demonstrar conceitos práticos de Level Design, Inteligência Artificial (NavMesh), interação baseada em Raycast e integração de Áudio (SFX/BGM).

O ambiente transporta o jogador para um vale misterioso "Low Poly" infestado por inimigos, onde a exploração e a sobrevivência andam lado a lado.

## ✨ Funcionalidades Principais (Features)

- 🚪 **Interação das "portas":** Portas automáticas com feedback visual (cor de destaque) e sonoro, operadas por meio de disparos de Raycast.
- 🧟 **IA Inimiga (Pathfinding):** Zumbis programados com `NavMeshAgent` que perseguem o jogador de forma inteligente, desviando de obstáculos
- 🔫 **Sistema de Combate:** Mecânica de tiro (Hitscan) direto do centro da câmera, contendo:
  - Arma visual _Sci-Fi_ vinculada à visão do jogador.
  - Mira centralizada em Canvas UI.
  - Sistema de vida para o inimigo (3 tiros para ser eliminado).
  - Animações de dano, perseguição e morte interligadas via _Animator Controller_.
- 💬 **UX/UI Integrada:** Sistema de "Toast" (Aviso em tela temporário) utilizando _TextMeshPro_ para guiar o jogador na primeira interação já que não tive tempo para melhorar o cenário de nascimento.
- 🎧 **Design de Som (AudioSFX):** Trilha sonora ambiente contínua e efeitos sonoros espacializados ao realizar disparos.
- 🥽 **Arquitetura VR-Ready:** Projeto configurado tecnicamente para rodar no sistema Android, utilizando o **Meta XR Core SDK**.

---

## 🎮 Como Jogar e Testar (Modo PC)

Embora o projeto seja focado no Meta Quest, toda a movimentação inicial e interações foram adaptadas para serem avaliadas e testadas diretamente no PC.

### Controles

- `W, A, S, D` - Movimentação do Personagem
- `Mouse` - Movimentação da Câmera (Visão)
- `Botão Esquerdo do Mouse` - Atirar
- `E` - Interagir com o cenário (Mirar para a porta azul para abrir/fechar)

### Passo a Passo para rodar na Unity:

1. Faça o clone ou download deste repositório.
2. Abra o projeto através do **Unity Hub** (Recomendado usar a mesma versão na qual o projeto foi salvo).
3. Na aba `Project`, navegue até a pasta `Assets/Scenes` e abra a **SampleScene**.
4. Clique no botão **Play** (▶) no topo da tela do editor.
5. Siga as instruções do _Toast_ inicial na tela para interagir com a porta e prepare-se para atirar nos zumbis!

---

## ⚙️ Como Funciona (Arquitetura Técnica)

O projeto foi construído seguindo boas práticas de engenharia de software com C#, utilizando scripts modulares, escaláveis e fortemente documentados:

- **InteracaoJogador.cs:** Controla as interações passíficas do jogador. Dispara um raio invisível (`Physics.Raycast`) limitando-se apenas a objetos na `LayerMask` de portas para otimização de performance física.
- **ArmaJogador.cs:** Gerencia a leitura de botões com o Novo _InputSystem_ da Unity. Aciona arquivos dinâmicos de `AudioSource` e detecta hits via Raycast para enviar a função de dano.
- **Porta.cs / ZumbiVida.cs:** Scripts reativos. Eles não "procuram" informações ativamente (o que gastaria muito processamento), apenas reagem quando métodos públicos como `AlternarPorta()` ou `ReceberDano()` são chamados externamente pelo jogador.
- **ZumbiPerseguidor.cs:** Utiliza a biblioteca `UnityEngine.AI` para calcular o menor caminho possível até o jogador em tempo real, baseando-se no _"Bake"_ do chão azul (NavMesh Surface). Ele controla as variáveis _Bool_ e _Trigger_ enviadas para o Cérebro de Animações do modelo 3D.
