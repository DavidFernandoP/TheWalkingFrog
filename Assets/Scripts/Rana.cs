using UnityEngine;

public class Rana : MonoBehaviour
{
    public float velocidad = 3f;
    public Transform jugador;
    public float fuerzaSalto;
    private Rigidbody2D rb;
    public int danio;
    public int vida = 30;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating(nameof(Salto), 2f, 2f);
    }

    void Update()
    {
        
        float direccion = Mathf.Sign(jugador.position.x - transform.position.x);
        rb.velocity = new Vector2(direccion * velocidad, rb.velocity.y);
        
    }

    public void Salto()
    {
        
        rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            Jugador jugador =
            collision.gameObject.GetComponent<Jugador>();

            if (jugador != null)
            {
                Vector2 direccion = (collision.transform.position - transform.position).normalized;
                jugador.RecibirDanio(danio, direccion);
            }
        }
    }

    public void RecibirDanio(int cantidad)
    {
        vida -= cantidad;

        Debug.Log("Vida enemigo: " + vida);

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}