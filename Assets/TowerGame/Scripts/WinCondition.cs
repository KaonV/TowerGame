using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class WinCondition : MonoBehaviour
{

    private void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bricks"))
        {
            SceneManager.LoadScene("Win");

        }
    }
}