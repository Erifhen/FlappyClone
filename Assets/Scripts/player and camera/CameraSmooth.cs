using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // Seu jogador/ninja
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Offset padrão para câmera 2D

    [Header("Follow Settings")]
    [Range(0f, 1f)] public float smoothSpeed = 0.125f;
    public float maxLookAhead = 2f; // Quanto a câmera pode "antecipar" o movimento
    public float lookAheadFactor = 0.5f; // Fator de suavização do look ahead
    public float verticalOffset = 1f; // Offset vertical para não ficar muito no chão

    [Header("Parallax Compatibility")]
    public float parallaxInfluence = 0.3f; // Quanto o paralax afeta o movimento da câmera
    public float minY = -3f; // Limite vertical mínimo
    public float maxY = 3f;  // Limite vertical máximo

    private Vector3 velocity = Vector3.zero;
    private Vector3 lastTargetPosition;
    private Vector3 lookAheadPos;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("SmoothCameraFollow: Nenhum alvo atribuído!");
            return;
        }

        // Inicializa a posição da câmera
        transform.position = target.position + offset;
        lastTargetPosition = target.position;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Calcula a direção do movimento
        Vector3 targetMovement = target.position - lastTargetPosition;
        lastTargetPosition = target.position;

        // Calcula o look ahead (antecipação do movimento)
        float lookAheadX = Mathf.Clamp(targetMovement.x * lookAheadFactor, -maxLookAhead, maxLookAhead);
        
        // Aplica um pouco do efeito paralax no movimento vertical
        float parallaxY = Mathf.Lerp(0, targetMovement.y, parallaxInfluence);
        
        // Posição alvo com offset e look ahead
        Vector3 targetPosition = target.position + offset + new Vector3(lookAheadX, verticalOffset + parallaxY, 0f);
        
        // Limita o movimento vertical para não sair dos limites
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        
        // Suaviza o movimento da câmera usando SmoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
    }
}