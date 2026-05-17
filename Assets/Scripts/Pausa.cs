using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    [Header("Canvas de pausa")]
    public GameObject panelPausa;

    private bool juegoPausado = false;

    void Start()
    {
        panelPausa.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Pausar()
    {
        panelPausa.SetActive(true);

        Time.timeScale = 0f;

        juegoPausado = true;
    }

    public void Reanudar()
    {
        panelPausa.SetActive(false);

        Time.timeScale = 1f;

        juegoPausado = false;
    }

    public void VolverMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Menu");
    }
}