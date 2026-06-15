using UnityEngine;

// Orb de energia: gira, flutua e some ao ser tocado pelo jogador,
// somando 1 ponto no GameManager.
[RequireComponent(typeof(Collider))]
public class Collectible : MonoBehaviour
{
    public float velocidadeRotacao = 90f;
    public float velocidadeFlutuacao = 2f;
    public float alturaFlutuacao = 0.25f;
    public AudioClip somColeta;
    public GameObject efeitoColeta;

    private Vector3 posBase;

    void Start()
    {
        posBase = transform.position;
        GetComponent<Collider>().isTrigger = true;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, velocidadeRotacao * Time.deltaTime, Space.World);
        float y = Mathf.Sin(Time.time * velocidadeFlutuacao) * alturaFlutuacao;
        transform.position = posBase + new Vector3(0f, y, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (GameManager.Instance != null) GameManager.Instance.ColetarOrb();
        if (somColeta != null) AudioSource.PlayClipAtPoint(somColeta, transform.position);
        if (efeitoColeta != null) Instantiate(efeitoColeta, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
