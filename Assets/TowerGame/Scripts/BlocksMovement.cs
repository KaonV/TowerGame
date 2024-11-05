using Fusion;
using UnityEngine;
using UnityEngine.UIElements;

public class BlocksMovement : NetworkBehaviour
{
    private Rigidbody2D _controller;
    [SerializeField] private float _moveDistance = 30f;
    private bool _canMove = true;
    private void Awake()
    {
        _controller = GetComponent<Rigidbody2D>();
    }

    public override void FixedUpdateNetwork()
    {
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && _canMove)
            {
                MoveBlock(Vector2.right);
                Debug.Log("indo pra direita");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && _canMove)
            {
                MoveBlock(Vector2.left);
                Debug.Log("indo pra esquerda");
            }

            else if (Input.GetKeyDown(KeyCode.UpArrow) && _canMove)
            {
                RotateBlock(90f);
                Debug.Log("Rotacionando 90 graus");
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow) && _canMove)
            {
                MoveBlock(Vector2.down);
                Debug.Log("Descendo");
            }
        }
    }

    private void MoveBlock(Vector2 direction)
    {
        // Movimenta o bloco na dire��o especificada
        _controller.MovePosition(_controller.position + direction * _moveDistance);
    }

    private void RotateBlock(float angle)
    {
        // Rotaciona o bloco em rela��o ao seu eixo central
        _controller.MoveRotation(_controller.rotation + angle);
    }

    private void OnColisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _canMove = false; // Desativa o movimento ao colidir com o objeto de tag "Ground"
            Debug.Log("Movimento desativado devido � colis�o com o ch�o");
        }
    }
    
}
