using System.Collections;
using UnityEngine;

public class SpawnRanas : MonoBehaviour
{
    public GameObject[] tipoDeRana;

    public float tiempoEntreOleadas = 3f;
    public float tiempoEntreSpawns = 0.5f;

    public int oleadaActual = 1;

    private int cantidadRanas = 1;

    void Start()
    {
        StartCoroutine(SistemaOleadas());
    }

    IEnumerator SistemaOleadas()
    {
        while (true)
        {
            Debug.Log("Oleada: " + oleadaActual);

            for (int i = 0; i < cantidadRanas; i++)
            {
                Generar();

                yield return new WaitForSeconds(tiempoEntreSpawns);
            }

            yield return new WaitUntil(() =>
                GameObject.FindGameObjectsWithTag("Enemigo").Length == 0
            );

            Debug.Log("Oleada completada");

            yield return new WaitForSeconds(tiempoEntreOleadas);

            cantidadRanas++;

            oleadaActual++;
        }
    }

    void Generar()
    {
        int indice = Random.Range(0, tipoDeRana.Length);

        GameObject prefabElegido = tipoDeRana[indice];

        Instantiate(prefabElegido,
                    transform.position,
                    Quaternion.identity);
    }
}