using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Controla o estado da partida: pontuacao (energia coletada), vidas,
// tempo restante e as condicoes de vitoria/derrota.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Configuracao da partida")]
    public int vidasIniciais = 3;
    public float tempoLimite = 60f;

    [Header("Referencias de UI (HUD)")]
    public Text textoEnergia;
    public Text textoTempo;
    public Text textoVidas;
    public GameObject painelVitoria;
    public GameObject painelDerrota;

    private int totalOrbs;
    private int coletados;
    private int vidas;
    private float tempoRestante;
    private bool fimDeJogo;

    public bool FimDeJogo => fimDeJogo;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        vidas = vidasIniciais;
        tempoRestante = tempoLimite;
        // conta quantos orbs existem na cena no inicio da partida
        totalOrbs = FindObjectsOfType<Collectible>().Length;
        coletados = 0;
        fimDeJogo = false;
        Time.timeScale = 1f;
        if (painelVitoria) painelVitoria.SetActive(false);
        if (painelDerrota) painelDerrota.SetActive(false);
        AtualizarHUD();
    }

    void Update()
    {
        if (fimDeJogo) return;
        tempoRestante -= Time.deltaTime;
        if (tempoRestante <= 0f)
        {
            tempoRestante = 0f;
            Derrota();
        }
        AtualizarHUD();
    }

    public void ColetarOrb()
    {
        if (fimDeJogo) return;
        coletados++;
        AtualizarHUD();
        if (coletados >= totalOrbs) Vitoria();
    }

    public void PerderVida()
    {
        if (fimDeJogo) return;
        vidas--;
        AtualizarHUD();
        if (vidas <= 0) Derrota();
    }

    void Vitoria()
    {
        fimDeJogo = true;
        if (painelVitoria) painelVitoria.SetActive(true);
        Time.timeScale = 0f;
    }

    void Derrota()
    {
        fimDeJogo = true;
        if (painelDerrota) painelDerrota.SetActive(true);
        Time.timeScale = 0f;
    }

    void AtualizarHUD()
    {
        if (textoEnergia) textoEnergia.text = "Energia: " + coletados + " / " + totalOrbs;
        if (textoTempo)   textoTempo.text   = "Tempo: " + Mathf.CeilToInt(tempoRestante);
        if (textoVidas)   textoVidas.text   = "Vidas: " + vidas;
    }

    // Chamados pelos botoes dos paineis de vitoria/derrota
    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void VoltarAoMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
