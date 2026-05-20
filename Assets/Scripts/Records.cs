using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Records : MonoBehaviour
{
    public static Records instancia;
    public TMP_Text textoPodioPausa;

    [Header("Panel principal")]
    public GameObject panelRecords;

    [Header("Textos")]
    public TMP_Text textoPodio;
    public TMP_Text textoPuntajeFinal;

    [Header("Guardar nuevo récord")]
    public GameObject contenedorGuardar;
    public TMP_InputField inputNombre;

    [Header("No entró al podio")]
    public GameObject contenedorNoPodio;

    [Header("Escenas")]
    public string nombreEscenaMenu = "Menu";

    private List<TomaNombre> records = new List<TomaNombre>();
    private int puntajePendiente;

    private void Awake()
    {
        instancia = this;

        CargarRecords();
        ActualizarUI();

        if (panelRecords != null)
            panelRecords.SetActive(false);

        if (contenedorGuardar != null)
            contenedorGuardar.SetActive(false);

        if (contenedorNoPodio != null)
            contenedorNoPodio.SetActive(false);
    }

    public void RevisarNuevoRecord(int puntaje)
    {
        puntajePendiente = puntaje;

        panelRecords.SetActive(true);

        if (textoPuntajeFinal != null)
            textoPuntajeFinal.text = "Puntaje actual: " + puntaje;

        ActualizarUI();

        bool entraPodio = EntraAlPodio(puntaje);

        contenedorGuardar.SetActive(entraPodio);
        contenedorNoPodio.SetActive(!entraPodio);

        if (entraPodio)
        {
            inputNombre.text = "";
            inputNombre.Select();
        }
    }

    public void GuardarRecord()
    {
        string nombre = inputNombre.text.ToUpper().Trim();

        if (string.IsNullOrEmpty(nombre))
            nombre = "AAA";

        if (nombre.Length > 8)
            nombre = nombre.Substring(0, 8);

        records.Add(new TomaNombre(nombre, puntajePendiente));

        records.Sort((a, b) => b.puntaje.CompareTo(a.puntaje));

        if (records.Count > 3)
            records.RemoveAt(3);

        GuardarEnPlayerPrefs();
        ActualizarUI();

        Continuar();
    }

    public void Continuar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreEscenaMenu);
    }

    private bool EntraAlPodio(int puntaje)
    {
        if (records.Count < 3)
            return true;

        return puntaje > records[records.Count - 1].puntaje;
    }

    private void GuardarEnPlayerPrefs()
    {
        for (int i = 0; i < records.Count; i++)
        {
            PlayerPrefs.SetString("Nombre" + i, records[i].nombre);
            PlayerPrefs.SetInt("Puntaje" + i, records[i].puntaje);
        }

        PlayerPrefs.Save();
    }

    private void CargarRecords()
    {
        records.Clear();

        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.HasKey("Nombre" + i))
            {
                string nombre = PlayerPrefs.GetString("Nombre" + i);
                int puntaje = PlayerPrefs.GetInt("Puntaje" + i);

                records.Add(new TomaNombre(nombre, puntaje));
            }
        }

        records.Sort((a, b) => b.puntaje.CompareTo(a.puntaje));
    }

    private void ActualizarUI()
    {
        string podio = "";

        for (int i = 0; i < records.Count; i++)
        {
            podio +=
                (i + 1) + ". " +
                records[i].nombre +
                " - " +
                records[i].puntaje +
                "\n";
        }

        if (textoPodio != null)
            textoPodio.text = podio;

        if (textoPodioPausa != null)
            textoPodioPausa.text = podio;
    }
}