using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float velocidad;
    public float fuerzaSalto;

    private bool enSuelo;
    private Rigidbody2D rb;

    public Animator animator;
    public Collider2D bastonCollider;
    public int vida;
    public float fuerzaKnockback = 8f;
    public float tiempoKnockback = 0.6f;
    private bool recibiendoKnockback;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bastonCollider.enabled = false;
    }

    void Update()
    {
        if (recibiendoKnockback)
        {
            return;
        }
        float movimientoX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movimientoX * velocidad, rb.velocity.y);

        
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(ActivarColisionBaston());
        }

        animator.SetFloat("movement", Mathf.Abs(movimientoX));

        if (movimientoX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (movimientoX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
            enSuelo = false;
        }

        animator.SetBool("ensuelo", enSuelo);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true;
        }

        

    }

    IEnumerator ActivarColisionBaston()
    {
        animator.SetBool("golpe", true);
        bastonCollider.enabled = true;

        yield return new WaitForSeconds(0.3f);

        bastonCollider.enabled = false;
        animator.SetBool("golpe", false);
    }

    IEnumerator Knockback(Vector2 direccion)
    {
        recibiendoKnockback = true;
        animator.SetTrigger("dano");

        rb.velocity = Vector2.zero;

        rb.AddForce(direccion * fuerzaKnockback, ForceMode2D.Impulse);

        yield return new WaitForSeconds(tiempoKnockback);

        recibiendoKnockback = false;
    }

    public void RecibirDanio(int cantidad, Vector2 direccion)
    {
        vida -= cantidad;

        Debug.Log("Vida restante: " + vida);

        StartCoroutine(Knockback(direccion));

        if (vida <= 0)
        {
           GameManager.instancia.GameOver();
           Destroy(gameObject);
        }
    }

     
}