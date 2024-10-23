using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public void CriarSala()
    {
        SceneManager.LoadScene(1);
    }
    public void EntrarEmSala()
    {
        SceneManager.LoadScene(2);
    }
    public void Opcoes()
    {
        SceneManager.LoadScene(3);
    }
    public void Creditos()
    {
        SceneManager.LoadScene(4);
    }
    public void Sair()
    {
        Application.Quit();
    }
}
