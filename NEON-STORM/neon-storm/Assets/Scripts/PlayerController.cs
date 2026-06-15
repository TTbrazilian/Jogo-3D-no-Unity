using UnityEngine;

// Movimentacao do jogador por teclado (WASD/setas) relativa a camera,
// com pulo (barra de espaco) usando fisica de Rigidbody.
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimentacao")]
    public float velocidade = 8f;
    public float forcaPulo = 7f;
    public Transform referenciaCamera;

    [Header("Checagem de chao")]
    public LayerMask camadaChao = ~0;
    public float distanciaChao = 1.2f;

    private Rigidbody rb;
    private Vector3 entrada;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        if (referenciaCamera == null && Camera.main != null)
            referenciaCamera = Camera.main.transform;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        entrada = new Vector3(h, 0f, v).normalized;

        if (Input.GetButtonDown("Jump") && NoChao())
            rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        Vector3 direcao = entrada;
        if (referenciaCamera != null)
        {
            // converte a entrada para o referencial da camera (frente/lados)
            Vector3 frente = referenciaCamera.forward; frente.y = 0f; frente.Normalize();
            Vector3 direita = referenciaCamera.right;  direita.y = 0f; direita.Normalize();
            direcao = (frente * entrada.z + direita * entrada.x).normalized;
        }

        Vector3 alvo = direcao * velocidade;
        rb.velocity = new Vector3(alvo.x, rb.velocity.y, alvo.z);

        if (direcao.sqrMagnitude > 0.01f)
            transform.rotation = Quaternion.Slerp(
                transform.rotation, Quaternion.LookRotation(direcao), 12f * Time.fixedDeltaTime);
    }

    bool NoChao()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanciaChao, camadaChao);
    }
}
