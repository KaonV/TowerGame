using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosingCounter : NetworkBehaviour
{
    private int vidas;
    public string _sceneName = "GameScene";
    public NetworkRunner _runnerGameplay = null;


    private void Start()
    {
        vidas = 3;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bricks"))
        {
            Debug.Log("entrou aqui");
            Runner.Despawn(collision.gameObject.GetComponent<NetworkObject>());
            vidas--;
            Derrota();
        }
    }

    private void Derrota()
    {
        if (vidas <= 0)
        {
            if (Runner.IsSceneAuthority)
            {
                Debug.Log("acabou :(");
                var scene = SceneRef.FromIndex(2);
                var x = Runner.LoadScene(scene);
            }
        }
    }
}