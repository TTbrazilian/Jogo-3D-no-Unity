using UnityEngine;

// IA simples de inimigo "Sentinela": detecta o jogador dentro de um raio,
// persegue ele no plano XZ e causa dano por contato (com cooldown),
// empurrando o jogador para longe.
public class EnemyAI : MonoBehaviour
{
    [Header("Perseguicao")]
    public float velocidade = 4f;
    public float raioDeteccao = 30f;
    public float forcaEmpurrao = 6f;

    [Header("Dano")]
    public float intervaloDano = 1.5f;

    private Transform jogador;
    private float proximoDano;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) jogador = p.transform;
    }

    void Update()
    {
        if (jogador == null) return;
        if (GameManager.Instance != null && GameManager.Instance.FimDeJogo) return;

        float distancia = Vector3.Distance(transform.position, jogador.position);
        if (distancia > raioDeteccao) return; // jogador fora do alcance: nao persegue

        Vector3 direcao = jogador.position - transform.position;
        direcao.y = 0f;
        direcao.Normalize();

        // move em direcao ao jogador e vira para ele
        transform.position += direcao * velocidade * Time.deltaTime;
        if (direcao.sqrMagnitude > 0.01f)
            transform.rotation = Quaternion.Slerp(
                transform.rotation, Quaternion.LookRotation(direcao), 8f * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col) { TentarDanificar(col.collider); }
    void OnTriggerEnter(Collider other) { TentarDanificar(other); }

    void TentarDanificar(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (Time.time < proximoDano) return;     // respeita o cooldown de dano
        proximoDano = Time.time + intervaloDano;

        if (GameManager.Instance != null) GameManager.Instance.PerderVida();

        // empurrao para dar feedback ao jogador
        Rigidbody rbJog = other.attachedRigidbody;
        if (rbJog != null)
        {
            Vector3 empurrao = (other.transform.position - transform.position).normalized;
            empurrao.y = 0.4f;
            rbJog.AddForce(empurrao * forcaEmpurrao, ForceMode.Impulse);
        }
    }
}
