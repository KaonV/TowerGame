using Fusion;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : SimulationBehaviour
{
    private CharacterController controller;
    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * Runner.DeltaTime * moveSpeed;
        controller.Move(move);


        if (move != Vector3.zero)
        {
            transform.forward = move;
        }
    }
}
