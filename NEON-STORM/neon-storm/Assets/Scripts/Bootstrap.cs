using UnityEngine;

// (Opcional) Espalha orbs e inimigos pela arena no inicio da partida,
// evitando ter que posicionar cada objeto na mao no editor.
// Roda no Awake (antes do Start do GameManager), entao os orbs ja existem
// quando o GameManager conta o total.
public class Bootstrap : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject orbPrefab;
    public GameObject inimigoPrefab;

    [Header("Quantidades")]
    public int quantidadeOrbs = 10;
    public int quantidadeInimigos = 4;

    [Header("Area de spawn (raio a partir do centro)")]
    public float raioSpawn = 18f;
    public float alturaOrb = 1f;

    void Awake()
    {
        for (int i = 0; i < quantidadeOrbs; i++)
            Spawnar(orbPrefab, alturaOrb);
        for (int i = 0; i < quantidadeInimigos; i++)
            Spawnar(inimigoPrefab, 1f);
    }

    void Spawnar(GameObject prefab, float altura)
    {
        if (prefab == null) return;
        Vector2 c = Random.insideUnitCircle * raioSpawn;
        Instantiate(prefab, new Vector3(c.x, altura, c.y), Quaternion.identity);
    }
}
