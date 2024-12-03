using Fusion;
using UnityEngine;

public class CameraFollow : NetworkBehaviour
{
    public Transform target; // Referência para o objeto que a câmera seguirá (o "centro" da pilha)
    public float followThreshold = 5f; // Altura mínima para a câmera começar a subir
    public float followSpeed = 2f; // Velocidade com que a câmera sobe

    private float initialY; // Posição inicial da câmera no eixo Y
    [SerializeField] private float yOffset;
    private void Start()
    {
        initialY = transform.position.y;
    }

    private void Update()
    {
        if (target != null)
        {
            // Verifica se a altura do alvo está além do limite
            if (target.position.y > initialY + followThreshold)
            {
                // Move a câmera suavemente em direção à posição do alvo
                Vector3 newPosition = transform.position;
                newPosition.y = Mathf.Lerp(transform.position.y, target.position.y - followThreshold, Time.deltaTime * followSpeed);
                transform.position = newPosition - Vector3.up * yOffset;
            }
        }
    }
}
