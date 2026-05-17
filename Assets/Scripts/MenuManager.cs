using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Zonas");
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void IrGallinero()
    {
        SceneManager.LoadScene("Gallinero");
    }

    public void IrEstablo()
    {
        SceneManager.LoadScene("Establo");
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}