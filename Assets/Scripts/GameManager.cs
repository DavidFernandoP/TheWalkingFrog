using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    public int puntos;

    public TMP_Text textoPuntos;

    public GameObject panelGameOver;

    private void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        panelGameOver.SetActive(false);
    }

    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;

        textoPuntos.text = "Puntos: " + puntos;
    }

    public void GameOver()
    {
        panelGameOver.SetActive(true);

        Time.timeScale = 0f;

        Records.instancia
            .RevisarNuevoRecord(puntos);
    }
}