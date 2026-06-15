using UnityEngine;

// Camera em terceira pessoa que segue o jogador suavemente.
public class CameraFollow : MonoBehaviour
{
    public Transform alvo;
    public Vector3 deslocamento = new Vector3(0f, 12f, -10f);
    public float suavidade = 6f;

    void LateUpdate()
    {
        if (alvo == null) return;
        Vector3 desejada = alvo.position + deslocamento;
        transform.position = Vector3.Lerp(transform.position, desejada, suavidade * Time.deltaTime);
        transform.LookAt(alvo.position + Vector3.up);
    }
}
