using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class WinCondition : NetworkBehaviour
{
    public string _sceneName = "GameScene";
    public NetworkRunner _runnerGameplay = null;

    // Variáveis para animação de subida e descida
    public float amplitude = 0.5f; // Altura do movimento
    public float frequency = 1f; // Velocidade do movimento

    private Vector3 _startPosition;

    private void Start()
    {
        // Salva a posição inicial do objeto
        _startPosition = transform.position;
    }

    private void Update()
    {
        // Aplica o movimento oscilatório
        transform.position = _startPosition + new Vector3(0, Mathf.Sin(Time.time * frequency) * amplitude, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bricks"))
        {
            if (Runner.IsSceneAuthority)
            {
                Debug.Log("acabou :(");
                var scene = SceneRef.FromIndex(3);
                var x = Runner.LoadScene(scene);
            }
        }
    }
}
