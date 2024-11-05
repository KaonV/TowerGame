using Fusion;
using UnityEngine;

public class RandomSpawner : NetworkBehaviour
{
    public GameObject[] brickTypes;

    private void OnColisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))

        {
            int randomIndex = Random.Range(0, brickTypes.Length);
            Vector3 SpawnPosition = new Vector3(-30, 604, 0);

            Runner.Spawn(brickTypes[randomIndex], SpawnPosition, Quaternion.identity);
        }
    }


    
}
