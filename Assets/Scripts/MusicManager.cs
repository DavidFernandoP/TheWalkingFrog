using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instancia;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Música")]
    public AudioClip musicaMenu;
    public AudioClip musicaGameplay;

    private AudioClip musicaActual;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        CambiarMusicaSegunEscena(
            SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene escena, LoadSceneMode modo)
    {
        CambiarMusicaSegunEscena(escena.name);
    }

    void CambiarMusicaSegunEscena(string nombreEscena)
    {
        AudioClip nuevaMusica;

        if (nombreEscena == "Gallinero" ||
            nombreEscena == "Establo")
        {
            nuevaMusica = musicaGameplay;
        }
        else
        {
            nuevaMusica = musicaMenu;
        }

        if (musicaActual == nuevaMusica)
            return;

        musicaActual = nuevaMusica;

        audioSource.Stop();
        audioSource.clip = musicaActual;
        audioSource.loop = true;
        audioSource.Play();
    }
}