using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LosingCounter : NetworkBehaviour
{
    private int vidas;
    public string _sceneName = "GameScene";
    public NetworkRunner _runnerGameplay = null;


    private void Start()
    {
        vidas = 3;
    }

    private void Update()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bricks"))
        {
            Destroy(collision.gameObject);
            vidas--;
        }
    }

    private void Derrota()
    {
        if (vidas <= 0)
        {
            Debug.Log("acabou :(");
            _runnerGameplay.LoadScene(_sceneName);
        }
    }
}