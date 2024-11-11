using Fusion;
using UnityEngine;

public class RandomSpawner : NetworkBehaviour
{
    public GameObject[] brickTypes;
    public bool _currentBlock;
    [SerializeField] private int lifes;

    public void Start()
    {
        _currentBlock = true;
        lifes = 3;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && _currentBlock)             
        {
            Debug.Log("Colidiu");
            int randomIndex = Random.Range(0, brickTypes.Length);
            Vector3 SpawnPosition = new Vector3(-30, 604, 0);
            _currentBlock = false;
            Runner.Spawn(brickTypes[randomIndex], SpawnPosition, Quaternion.identity);
        }

        else if (collision.gameObject.CompareTag("Limits"))
        {
            Debug.Log("-1 vida");
            lifes--;
        }
    }
}
