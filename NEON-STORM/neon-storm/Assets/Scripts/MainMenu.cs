using UnityEngine;
using UnityEngine.SceneManagement;

// Botoes do menu principal.
public class MainMenu : MonoBehaviour
{
    public string cenaDoJogo = "Game";

    public void Jogar()
    {
        SceneManager.LoadScene(cenaDoJogo);
    }

    public void Sair()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}
