using Fusion;
using UnityEngine;
using UnityEngine.UIElements;

public class BlocksMovement : NetworkBehaviour
{
     private Rigidbody2D _controller { get; set;}
    [SerializeField] private float _moveDistance = 30f;
    private bool _canMove { get; set; }
    private bool _canScore { get; set; }
    private KauaGameManager _gameManager;
    [SerializeField] private Vector2 moveVector;
    [SerializeField] private bool processMove = false;
    private void Awake()
    {
        _controller = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _canMove = true;
    }

    private void Update()
    {
        /*if (Runner.LocalPlayer.AsIndex - 1 != PhotonManager.Instance.currentTurnIndex)
        {
            Debug.Log($"Player atual: {Runner.LocalPlayer.AsIndex} / Turno: {PhotonManager.Instance.currentTurnIndex}");
            return;
        }*/

        if (Input.GetKeyDown(KeyCode.RightArrow) && _canMove)
        {
            moveVector = Vector2.right;
            processMove = true;
            Debug.Log("indo pra direita");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && _canMove)
        {
            moveVector = Vector2.left;
            processMove = true;
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

    public override void FixedUpdateNetwork()
    {
        if (!_canMove) return;
        if (processMove && moveVector != Vector2.zero)
        {
            MoveBlock(moveVector);
            processMove = false;
            moveVector = Vector2.zero;
        }
        //{
        //    if (Input.GetKeyDown(KeyCode.RightArrow) && _canMove)
        //    {
        //        MoveBlock(Vector2.right);
        //        Debug.Log("indo pra direita");
        //    }
        //    else if (Input.GetKeyDown(KeyCode.LeftArrow) && _canMove)
        //    {
        //        MoveBlock(Vector2.left);
        //        Debug.Log("indo pra esquerda");
        //    }

        //    else if (Input.GetKeyDown(KeyCode.UpArrow) && _canMove)
        //    {
        //        RotateBlock(90f);
        //        Debug.Log("Rotacionando 90 graus");
        //    }

        //    else if (Input.GetKeyDown(KeyCode.DownArrow) && _canMove)
        //    {
        //        MoveBlock(Vector2.down);
        //        Debug.Log("Descendo");
        //    }
        //}
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _canMove = false;
            Debug.Log("Movimento desativado devido à colisão com o chão");
        }

        else if (collision.gameObject.CompareTag("Bricks"))
        {
            _canMove = false;
        }

        else if (collision.gameObject.CompareTag("Ground") && !_canMove)
        {
            //nada por enquanto, adicionar contagem em breve 
        }

        else if (collision.gameObject.CompareTag("Ground") && _canScore)
        {
            _gameManager?.IncrementScore();
            _canScore = false;

        }
    }
}
