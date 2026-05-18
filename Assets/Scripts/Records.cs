using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class Records : MonoBehaviour
{
    public static Records instancia;

    public TMP_InputField inputNombre;

    public TMP_Text textoLeaderboard;

    private List<TomaNombre> records =
        new List<TomaNombre>();

    private int puntajePendiente;

    private void Awake()
    {
        instancia = this;

        CargarRecords();
        ActualizarUI();
    }

    public void RevisarNuevoRecord(int puntaje)
    {
        puntajePendiente = puntaje;

        inputNombre.gameObject.SetActive(true);
    }

    public void GuardarRecord()
    {
        string nombre = inputNombre.text.ToUpper();

        if (nombre.Length > 5)
        {
            nombre = nombre.Substring(0, 5);
        }

        records.Add(new TomaNombre(nombre, puntajePendiente));

        records.Sort((a, b) =>
            b.puntaje.CompareTo(a.puntaje));

        if (records.Count > 10)
        {
            records.RemoveAt(10);
        }

        GuardarEnPlayerPrefs();

        ActualizarUI();

        inputNombre.gameObject.SetActive(false);
        SceneManager.LoadScene("Menu");
    }

    void GuardarEnPlayerPrefs()
    {
        for (int i = 0; i < records.Count; i++)
        {
            PlayerPrefs.SetString(
                "Nombre" + i,
                records[i].nombre);

            PlayerPrefs.SetInt(
                "Puntaje" + i,
                records[i].puntaje);
        }

        PlayerPrefs.Save();
    }

    void CargarRecords()
    {
        records.Clear();

        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.HasKey("Nombre" + i))
            {
                string nombre =
                    PlayerPrefs.GetString("Nombre" + i);

                int puntaje =
                    PlayerPrefs.GetInt("Puntaje" + i);

                records.Add(
                    new TomaNombre(nombre, puntaje));
            }
        }
    }

    void ActualizarUI()
    {
        textoLeaderboard.text = "";

        for (int i = 0; i < records.Count; i++)
        {
            textoLeaderboard.text +=
                (i + 1) + ". " +
                records[i].nombre +
                " - " +
                records[i].puntaje +
                "\n";
        }
    }
}