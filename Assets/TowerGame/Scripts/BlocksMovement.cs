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
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveBlock(Vector2.right);
                Debug.Log("indo pra direita");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveBlock(Vector2.left);
                Debug.Log("indo pra esquerda");
            }

            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                RotateBlock(90f);
                Debug.Log("Rotacionando 90 graus");
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveBlock(Vector2.down);
                Debug.Log("Rotacionando 90 graus");
            }
        }
    }



    private void MoveBlock(Vector2 direction)
    {
        // Movimenta o bloco na direção especificada
        _controller.MovePosition(_controller.position + direction * _moveDistance);
    }

    private void RotateBlock(float angle)
    {
        // Rotaciona o bloco em relação ao seu eixo central
        _controller.MoveRotation(_controller.rotation + angle);
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _canMove = false; // Desativa o movimento ao colidir com o objeto de tag "Ground"
            Debug.Log("Movimento desativado devido à colisão com o chão");
        }
    }
    */
}
