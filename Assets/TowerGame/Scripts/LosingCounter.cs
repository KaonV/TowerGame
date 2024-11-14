using Fusion;
using UnityEngine;
using UnityEngine.Video;

public class LosingCounter : NetworkBehaviour
{
    private int vidas;
    [SerializeField] private GameObject telaDerrota;

    private void Start()
    {
        vidas = 3;
    }

    private void Update()
    {
        if (vidas <= 0)
        {
            Debug.Log("acabou :(");
            telaDerrota.SetActive(true);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bricks"))
        {
            Destroy(collision.gameObject);
            vidas--;
            Debug.Log("-1 vida");
        }
    }
}