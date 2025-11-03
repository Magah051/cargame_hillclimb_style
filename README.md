# 🏙️ Urban Race X

**Urban Race X** é um jogo de corrida 2D baseado em física, inspirado em *Hill Climb Racing*, mas ambientado em um **cenário urbano moderno**.  
Dirija pelas ladeiras da cidade, mantenha o tanque cheio e colete estrelas para alcançar a maior distância possível!

---

## 🎮 **Gameplay**

Você controla um carro que precisa **sobreviver o máximo possível**:
- Acelere com a tecla **`D`**
- Colete **⛽ galões de combustível** para não ficar sem gasolina
- Pegue **⭐ estrelas** para aumentar sua pontuação
- Mantenha o equilíbrio e evite capotar
- Escolha entre **diferentes carros**, cada um com estilo e performance únicos

O jogo termina quando:
- O combustível acaba, ou  
- O carro capota e toca o teto no chão.  

---

## 🧩 **Principais Funcionalidades**

- Física 2D realista com torque e gravidade
- Sistema de combustível com consumo dinâmico
- Coleta de estrelas e pontuação em tempo real
- HUD completo com velocímetro, pedal animado e barra de gasolina
- Menu com **seleção de carros animada**
- Sistema de **pause** e **retorno ao menu**
- Totalmente funcional em **WebGL** e **PC**

---

## 🛠️ **Tecnologias Utilizadas**

| Componente | Função |
|-------------|--------|
| **Unity 2D (C#)** | Motor do jogo |
| **Rigidbody2D + AddTorque()** | Física do carro |
| **Canvas UI** | Interface e HUD |
| **PlayerPrefs** | Salvamento de seleção de carro |
| **SceneManager** | Transição entre cenas |
| **Coroutines** | Transições suaves e animações de UI |
| **Physics2D** | Colisões e triggers (combustível/estrelas) |

---

## 🧱 **Scripts Principais**

| Script | Função |
|---------|--------|
| `CarMovement.cs` | Controla física e torque do carro |
| `Gas.cs` / `GasFuel.cs` | Sistema de combustível e coleta |
| `StarCount.cs` | Pontuação e UI de estrelas |
| `Speedometer.cs` | Calcula e exibe velocidade |
| `Accelerator.cs` | Mostra pedal pressionado |
| `CarSelectorUI.cs` / `BodyCar.cs` | Seleção e aplicação do carro escolhido |
| `GameController.cs` | Pause, menu e controle de cena |
| `MenuManager.cs` | Menu inicial e saída |

---

## 🕹️ **Como Jogar**

1. No menu principal, selecione seu carro preferido.  
2. Pressione **"Jogar"** para começar.  
3. Use a tecla **`D`** para acelerar.  
4. Colete galões de combustível para continuar rodando.  
5. Pegue estrelas para somar pontos.  
6. Evite capotar — o jogo reinicia se o carro virar.  
7. Pause a qualquer momento com **`ESC`**.

---

## ⚙️ **Configurações do Projeto**

- **Plataforma alvo:** PC / WebGL  
- **Resolução recomendada:** 1920x1080  
- **Física:**  
  - Gravity: (0, -9.81)  
  - Velocity Iterations: 8  
  - Position Iterations: 3  
- **Consumo de combustível:** 1f / 600f por segundo  
- **Reabastecimento:** +0.13f por galão  

---

## 🧠 **Arquitetura Técnica**

```mermaid
graph TD
    A[MenuManager] -->|Carrega cena| B[GameController]
    B --> C[CarSelectorUI]
    C -->|Salva ID| D[PlayerPrefs]
    D --> E[BodyCar]
    E --> F[CarMovement]
    F -->|Atualiza| G[Speedometer]
    F -->|Consome| H[Gas]
    H -->|Recarrega| I[GasFuel]
    F -->|Colide| J[StarCount]
    B -->|Pause| K[CanvasGroup]
