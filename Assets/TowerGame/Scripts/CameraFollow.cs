using Fusion;
using UnityEngine;

public class CameraFollow : NetworkBehaviour
{
    public Transform target; // Refer�ncia para o objeto que a c�mera seguir� (o "centro" da pilha)
    public float followThreshold = 5f; // Altura m�nima para a c�mera come�ar a subir
    public float followSpeed = 2f; // Velocidade com que a c�mera sobe

    private float initialY; // Posi��o inicial da c�mera no eixo Y
    [SerializeField] private float yOffset;
    private void Start()
    {
        initialY = transform.position.y;
    }

    private void Update()
    {
        if (target != null)
        {
            // Verifica se a altura do alvo est� al�m do limite
            if (target.position.y > initialY + followThreshold)
            {
                // Move a c�mera suavemente em dire��o � posi��o do alvo
                Vector3 newPosition = transform.position;
                newPosition.y = Mathf.Lerp(transform.position.y, target.position.y - followThreshold, Time.deltaTime * followSpeed);
                transform.position = newPosition - Vector3.up * yOffset;
            }
        }
    }
}
