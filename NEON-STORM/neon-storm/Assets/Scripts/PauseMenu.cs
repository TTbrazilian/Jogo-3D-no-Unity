using UnityEngine;

// Pausa/despausa o jogo com a tecla ESC.
public class PauseMenu : MonoBehaviour
{
    public GameObject painelPausa;
    private bool pausado;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Alternar();
    }

    public void Alternar()
    {
        pausado = !pausado;
        if (painelPausa != null) painelPausa.SetActive(pausado);
        Time.timeScale = pausado ? 0f : 1f;
    }

    public void Continuar()
    {
        pausado = false;
        if (painelPausa != null) painelPausa.SetActive(false);
        Time.timeScale = 1f;
    }
}
