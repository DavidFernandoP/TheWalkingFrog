using UnityEngine;

public class Baston : MonoBehaviour
{
    public int danio = 20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            Rana enemigo = other.GetComponent<Rana>();

            if (enemigo != null)
            {
                enemigo.RecibirDanio(danio);
            }
        }
    }
}