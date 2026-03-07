# 🧟‍♂️ VR KillZombies - Ambiente Interativo no Metaverso

![Unity](https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Meta Quest](https://img.shields.io/badge/Meta_Quest-045FEE?style=for-the-badge&logo=meta&logoColor=white)

---

## 1. Apresentando o Seu Projeto

**KillZombies** é um protótipo de ambiente interativo em primeira pessoa desenvolvido no motor gráfico Unity. Criado com foco na otimização e imersão para dispositivos de **Realidade Virtual (Meta Quest)**, o projeto demonstra a aplicação prática de conceitos avançados de Level Design, Inteligência Artificial adaptativa (NavMesh), interações modulares baseadas em *Raycast* e design sonoro espacial.

Nesta experiência, o jogador é transportado para um vale florestal recôndito (com estética *Low Poly*), infestado por ameaças, onde a exploração cautelosa e a sobrevivência andam lado a lado.

---

## 2. Contexto e Objetivos (O Papel no Metaverso)

O projeto **KillZombies** enquadra-se primordialmente no setor de **Entretenimento e Simulação** dentro do vasto ecossistema do Metaverso. 
O objetivo principal é criar uma experiência de imersão profunda (presença virtual), onde o utilizador se diverte jogando uma simulação de caos apocalíptico contra zumbis

---

## 3. Processo de Criação e Dificuldades

O desenvolvimento seguiu uma abordagem de prototipagem iterativa. Numa fase inicial, reuniu-se os modelos tridimensionais (assets) e estruturou-se o *Level Design* focado num ambiente fechado por montanhas, garantindo que o jogador se mantém nos limites estabelecidos sem recorrer a "paredes invisíveis". De seguida, a lógica em C# foi implementada de forma modular.

Durante este processo, surgiram os seguintes desafios técnicos e respetivas soluções:

### Dificuldade 1: Inteligência Artificial e Cálculo de Rotas
* **O Desafio:** Como fazer a IA (os *zombies*) perseguir o jogador contornando obstáculos naturais de forma fluida, sem consumir demasiados recursos do processador (vitais no Meta Quest)?
* **A Solução:** Foi utilizado o sistema nativo `NavMesh` (Navigation Mesh) da Unity. O maior obstáculo foi a porta automática: quando fechada, esta corrompia a malha de navegação. A solução técnica consistiu em aplicar um componente `NavMesh Modifier`.

### Dificuldade 2: Integração e Sincronização de Animações
* **O Desafio:** Integrar um modelo 3D importado e sincronizar corretamente as transições de estado (ficar parado, caminhar, morrer) de forma imersiva, evitando anomalias físicas (como o corpo do inimigo deslizar pelo chão após ser abatido).
* **A Solução:** Após a conversão de materiais para a **URP (Universal Render Pipeline)**, foi criada uma Máquina de Estados (*Animator Controller*). A sincronização foi atingida ao ler a velocidade vetorial do motor físico da IA (`agente.velocity.magnitude`). Quando a velocidade decresce (ex: colisão com porta ou ataqueção do jogador), regressa fluidamente à animação de paragem (*Idle*). 
* Para o momento de morte, a lógica desliga instantaneamente o `NavMeshAgent` (motor de movimento) e o `CapsuleCollider` (física), aciona a animação de queda, e após 4 segundos executa o método `Destroy()` para limpar a RAM do dispositivo VR, garantindo *frames* por segundo estáveis.

---

## ✨ 4. Funcionalidades de Destaque (Features)

- 🚪 **Interação Dinâmica:** Portas automáticas com *feedback* visual (emissão de cor azul indicativa de ação) operadas por *Raycast*.
- 🧟 **IA Autónoma (Pathfinding):** Inimigos dotados de navegação inteligente que reagem de forma independente à posição do jogador.
- 🔫 **Sistema de Combate Direto:** Mecânica *Hitscan* a partir da visão central (câmara), que conta com:
  - Modelo *Sci-Fi* adaptado à visão do jogador.
  - Retículo de mira central em *Canvas UI*.
  - Sistema de vitalidade escalável (3 disparos para eliminação).
- 💬 **Onboarding de Utilizador (UX/UI):** Sistema de "Toast" (aviso textual) através do *TextMeshPro* que orienta o jogador nos primeiros instantes e desaparece sozinho, contornando a falta de tempo para a criação de um ponto de *spawn* tutorializado. (eu nao tive tempo de melhorar a casinha que o jogador nasce kkkk)
- 🎧 **Áudio Imersivo:** Implementação espacializada de efeitos de tiro (SFX) conjugados com música ambiente contínua.
- 🥽 **Pronto para VR:** Projeto estruturado tecnicamente no XR Plugin Management para plataforma Android através do Meta XR Core SDK.

---

## 🎮 5. Como Jogar e Testar (Modo PC)

Apesar da otimização para óculos de Realidade Virtual, os controlos (inputs) foram mapeados para permitir testes ágeis e avaliação direta num ambiente Windows/Mac.

### Comandos de Jogo
- `W, A, S, D` - Movimentação da personagem.
- `Rato (Mouse)` - Controlo da câmara (visão direcional).
- `Botão Esquerdo do Rato` - Disparar arma.
- `E` - Interagir com elementos do ambiente (ex: focar na porta azul).

### Instalação e Execução:
1. Efetue o *Clone* ou descarregue o ficheiro ZIP deste repositório.
2. Adicione o projeto ao seu **Unity Hub**.
3. Navegue até ao diretório `Assets/Scenes` e abra o ficheiro **SampleScene**.
4. Pressione o botão **Play** (▶) no editor.

---

## ⚙️ 6. Arquitetura Técnica (Clean Code)

O projeto foi construído respeitando as boas práticas da engenharia de software (escalabilidade e separação de responsabilidades). Os guiões (scripts) são modulares e devidamente comentados:

- **`InteracaoJogador.cs`:** Utiliza `Physics.Raycast` altamente otimizado por via de `LayerMask`. O feixe de deteção apenas processa colisões com objetos inseridos na camada "Portas", poupando ciclos de processamento.
- **`Porta.cs` & `ZumbiVida.cs`:** Desenvolvidos segundo uma arquitetura **reativa**. Estes scripts não gastam recursos no método `Update()` a procurar o jogador. Ficam estáticos à espera que as suas funções públicas (`AlternarPorta()`, `ReceberDano()`) sejam acionadas externamente.
- **`ZumbiPerseguidor.cs`:** Faz a ponte inteligente entre a biblioteca `UnityEngine.AI` (lógica) e o sistema de Animação 3D (visual).

### 🚀 Próximos Passos (Futuro do Projeto)
* Melhoria no cenário e HUD (colocar tempo, pontuação para zumbis mortos e o round)
* Adição de colisão e dano por parte do inimigo em direção ao jogador.
* Sistema de rondas (*Waypoints*) para a IA quando o jogador não se encontra no seu campo de visão.
