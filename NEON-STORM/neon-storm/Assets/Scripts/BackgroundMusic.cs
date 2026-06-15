using UnityEngine;

// Garante que a musica de fundo toque em loop. Coloque este script no mesmo
// objeto que tem o AudioSource da trilha (ex.: no menu).
[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    void Start()
    {
        AudioSource src = GetComponent<AudioSource>();
        src.loop = true;
        if (!src.isPlaying) src.Play();
    }
}
