using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    public int puntos;
    public TMP_Text textoPuntos;

    private bool juegoTerminado = false;

    private void Awake()
    {
        instancia = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        ActualizarTextoPuntos();
    }

    public void SumarPuntos(int cantidad)
    {
        if (juegoTerminado) return;

        puntos += cantidad;
        ActualizarTextoPuntos();
    }

    private void ActualizarTextoPuntos()
    {
        if (textoPuntos != null)
            textoPuntos.text = "" +puntos;
    }

    public void GameOver()
    {
        if (juegoTerminado) return;

        juegoTerminado = true;
        Time.timeScale = 0f;

        Records.instancia.RevisarNuevoRecord(puntos);
    }
}