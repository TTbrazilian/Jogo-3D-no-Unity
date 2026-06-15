# Guia de montagem no Unity — NEON STORM

Os scripts já estão prontos em `Assets/Scripts`. Falta montar as duas cenas
(`Menu` e `Game`). Siga os passos abaixo — leva ~30 min.

> Recomendado: **Unity 2021.3 LTS** ou mais novo, template **3D (Built-in)**.

---

## 0. Tags e camadas

1. Selecione o objeto do jogador e, no topo do Inspector, crie/atribua a tag **`Player`**
   (Tag → Add Tag → "Player"). **Isso é obrigatório**: os inimigos, a chuva e os orbs
   procuram o jogador por essa tag.
2. (Opcional) Crie uma layer **`Chao`** para o piso e use-a no campo *Camada Chao* do
   `PlayerController` para o pulo funcionar melhor.

---

## 1. Cena do MENU

1. `File → New Scene` → salve como **`Menu`** em `Assets/Scenes`.
2. `GameObject → UI → Canvas` (um EventSystem é criado junto).
3. Dentro do Canvas, adicione:
   - Um **Image** de fundo (pode usar `Imagens/banner.png` como sprite) ou um Text com o título.
   - Dois **Button** (UI → Button): "Jogar" e "Sair".
4. Crie um GameObject vazio chamado **`MenuController`** e adicione o script **`MainMenu`**.
5. No botão **Jogar**: *On Click ()* → arraste `MenuController` → função `MainMenu.Jogar`.
   No botão **Sair**: → `MainMenu.Sair`.
6. **Música de fundo:** crie um GameObject **`Musica`**, adicione um **Audio Source**,
   arraste sua trilha (`.mp3`/`.wav`) no campo *AudioClip*, marque **Loop** e
   **Play On Awake**, e adicione o script **`BackgroundMusic`** (garante o loop).
   > Áudio grátis: incompetech.com, freesound.org, pixabay.com/music.

---

## 2. Cena do JOGO (`Game`)

1. `File → New Scene` → salve como **`Game`**.
2. **Piso:** `GameObject → 3D Object → Plane`, escale para ~`(5,1,5)`. Adicione paredes
   (Cubes finos nas bordas) para o jogador não cair da arena.
3. **Jogador:**
   - `GameObject → 3D Object → Sphere`, posição `(0,1,0)`.
   - Adicione **Rigidbody** (deixe *Use Gravity* marcado).
   - Atribua a **tag `Player`**.
   - Adicione o script **`PlayerController`**.
4. **Câmera:** selecione a *Main Camera*, adicione **`CameraFollow`** e arraste o
   jogador para o campo *Alvo*.
5. **Material neon (visual):** crie materiais (Project → Create → Material), ative
   *Emission* e escolha cores vivas (ciano, magenta) para jogador, orbs e inimigos.

### 2.1. Orbs (energia)
- Crie uma **Sphere** menor (~0.5), material emissivo.
- No **Sphere Collider**, marque **Is Trigger**.
- Adicione o script **`Collectible`**.
- Arraste para `Assets/Prefabs` para virar **prefab** (`Orb`).

### 2.2. Inimigos (Sentinelas)
- Crie um **Cube**, adicione **Rigidbody** (marque *Freeze Rotation* X/Y/Z) e o script
  **`EnemyAI`**.
- Salve como prefab (`Sentinela`).

### 2.3. Spawn automático (recomendado)
- Crie um GameObject vazio **`Spawner`** e adicione **`Bootstrap`**.
- Arraste os prefabs `Orb` e `Sentinela` para os campos correspondentes e defina as
  quantidades (ex.: 10 orbs, 4 inimigos).
- *Alternativa manual:* em vez do Bootstrap, basta espalhar vários `Orb` e `Sentinela`
  na cena na mão.

### 2.4. Chuva
- Crie um GameObject vazio **`Chuva`** e adicione **`RainSystem`**.
- (Opcional) Adicione um Audio Source com som de chuva e arraste no campo *Som Chuva*.
- Não precisa configurar partícula: o script cria tudo sozinho.

### 2.5. HUD e GameManager
1. Crie um GameObject vazio **`GameManager`** e adicione o script **`GameManager`**.
2. `GameObject → UI → Canvas`. Dentro dele crie 3 **Text** (ou TextMeshPro):
   energia, tempo e vidas. Crie também 2 painéis (Panel) **Vitória** e **Derrota**
   (deixe-os desativados — o script liga quando precisa).
3. No Inspector do `GameManager`, arraste cada Text/Panel para o campo correspondente.
4. Nos painéis, adicione botões "Reiniciar" e "Menu" ligados a
   `GameManager.Reiniciar` e `GameManager.VoltarAoMenu`.
5. **Pausa (opcional):** GameObject `PauseController` + script `PauseMenu`, com um Panel
   de pausa arrastado no campo.

---

## 3. Build Settings

1. `File → Build Settings`.
2. Com a cena **`Menu`** aberta, clique **Add Open Scenes** (Menu vira índice 0).
3. Abra **`Game`** e clique **Add Open Scenes** (índice 1).
4. Teste: dê Play no menu → Jogar deve carregar o jogo.

---

## 4. Dicas finais

- Se o pulo não funcionar, ajuste *Distancia Chao* no `PlayerController`.
- Se os inimigos atravessarem o chão, confirme que o piso tem Collider e os inimigos têm
  Rigidbody com *Freeze Rotation*.
- Para capturar os **prints**: use a janela *Game* (ícone de câmera no canto, ou a tecla
  de print do sistema). Salve em `Imagens/` com os nomes `menu.png`, `gameplay1.png`,
  `gameplay2.png`, `chuva.png`, `inimigos.png` (substituindo os placeholders).
- Para o **vídeo**: grave uma gameplay de até 5 min (OBS Studio, Xbox Game Bar `Win+G`,
  etc.), suba no YouTube como *não listado* e cole o link no README.
