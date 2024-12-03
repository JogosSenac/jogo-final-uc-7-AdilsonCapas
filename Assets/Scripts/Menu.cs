using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu:MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Mapa");
    }
    public void Creditos()
    {
        SceneManager.LoadScene("Créditos");
    }
    public void Menuu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Sair()
    {
        Application.Quit();
    }
}
