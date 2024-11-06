using Fusion;
using UnityEngine;

public class KauaGameManager : NetworkBehaviour
{
    [SerializeField] private int scoreCounter;

    private void Start()
    {
        scoreCounter = 0;
    }
    public void IncrementScore()
    {
        scoreCounter++;
        Debug.Log("Pontua��o atual: " + scoreCounter);
    }
}
