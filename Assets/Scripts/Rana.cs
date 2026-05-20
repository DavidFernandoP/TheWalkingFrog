using UnityEngine;

public class Rana : MonoBehaviour
{
    public float velocidad = 3f;
    private Transform jugador;
    public float fuerzaSalto;
    private Rigidbody2D rb;
    public int danio;
    public int vida = 30;
    public int puntos = 1;

    private bool enSuelo = true;

    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject jugadorObj =
            GameObject.FindGameObjectWithTag("Jugador");

        if (jugadorObj != null)
        {
            jugador = jugadorObj.transform;
        }

        InvokeRepeating(nameof(Salto), 1f, 2f);
    }

    void Update()
    {
        if (jugador == null)
            return;

        animator.SetBool("salto", !enSuelo);
    }

    public void Salto()
    {
        if (!enSuelo)
            return;

        enSuelo = false;

        float direccion =
            Mathf.Sign(jugador.position.x - transform.position.x);

        rb.velocity = new Vector2(
            direccion * velocidad,
            fuerzaSalto
        );

        if (direccion > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true;

            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (collision.gameObject.CompareTag("Jugador"))
        {
            Jugador jugadorScript =
                collision.gameObject.GetComponent<Jugador>();

            if (jugadorScript != null)
            {
                Vector2 direccion =
                    (collision.transform.position -
                     transform.position).normalized;

                jugadorScript.RecibirDanio(danio, direccion);
            }
        }
    }

    public void RecibirDanio(int cantidad)
    {
        vida -= cantidad;

        animator.SetTrigger("dano");

        Debug.Log("Vida enemigo: " + vida);

        if (vida <= 0)
        {
            if (GameManager.instancia != null)
            {
                GameManager.instancia.SumarPuntos(puntos);
            }

            Destroy(gameObject);
        }
    }
}