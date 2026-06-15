using UnityEngine;

// Sistema de chuva dinamica: cria um ParticleSystem por codigo e liga/desliga
// a chuva em intervalos aleatorios, deixando o clima da fase imprevisivel.
// O emissor segue o jogador para a chuva cobrir sempre a area onde ele esta.
public class RainSystem : MonoBehaviour
{
    [Header("Ciclo de clima (segundos)")]
    public float minSeco = 6f;
    public float maxSeco = 14f;
    public float minChuva = 8f;
    public float maxChuva = 16f;

    [Header("Intensidade")]
    public float alturaSpawn = 18f;
    public float areaChuva = 40f;
    public int particulasPorSegundo = 1200;

    [Header("Audio (opcional)")]
    public AudioSource somChuva;

    private ParticleSystem ps;
    private ParticleSystem.EmissionModule emissao;
    private bool chovendo;
    private float proximaTroca;

    void Start()
    {
        CriarParticulas();
        DefinirChuva(false);   // comeca seco
        AgendarTroca();
    }

    void Update()
    {
        // mantem o emissor acima do jogador
        GameObject jog = GameObject.FindGameObjectWithTag("Player");
        if (jog != null)
            transform.position = new Vector3(jog.transform.position.x, alturaSpawn, jog.transform.position.z);

        // alterna chuva/seco quando o tempo agendado chega
        if (Time.time >= proximaTroca)
        {
            DefinirChuva(!chovendo);
            AgendarTroca();
        }
    }

    void DefinirChuva(bool ligar)
    {
        chovendo = ligar;
        emissao.rateOverTime = ligar ? particulasPorSegundo : 0f;
        if (somChuva != null)
        {
            if (ligar && !somChuva.isPlaying) somChuva.Play();
            else if (!ligar && somChuva.isPlaying) somChuva.Stop();
        }
    }

    void AgendarTroca()
    {
        float dur = chovendo ? Random.Range(minChuva, maxChuva)
                             : Random.Range(minSeco, maxSeco);
        proximaTroca = Time.time + dur;
    }

    // Monta o ParticleSystem da chuva inteiramente por codigo.
    void CriarParticulas()
    {
        ps = GetComponent<ParticleSystem>();
        if (ps == null) ps = gameObject.AddComponent<ParticleSystem>();

        var main = ps.main;
        main.startSpeed = 0f;                 // a gravidade faz as gotas cairem
        main.gravityModifier = 3.5f;
        main.startSize = 0.08f;
        main.startLifetime = 1.6f;
        main.startColor = new Color(0.65f, 0.75f, 1f, 0.55f);
        main.maxParticles = 4000;
        main.simulationSpace = ParticleSystemSimulationSpace.World;

        emissao = ps.emission;
        emissao.rateOverTime = 0f;

        var shape = ps.shape;                 // area plana de onde as gotas surgem
        shape.shapeType = ParticleSystemShapeType.Box;
        shape.scale = new Vector3(areaChuva, 0.1f, areaChuva);

        // gotas como riscos verticais (efeito de chuva)
        var render = ps.GetComponent<ParticleSystemRenderer>();
        render.renderMode = ParticleSystemRenderMode.Stretch;
        render.velocityScale = 0.12f;
        render.material = new Material(Shader.Find("Sprites/Default"));
    }
}
