using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speedLeft = 4f, maxSpeedLeft = 18f;
    public float speedUp = 2f, maxSpeedUp = 12f;
    public int difficulty = 1;
    private float verticalDirection = 1f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // Mantém movimento sem ser afetado pela gravidade
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Evita giros indesejados
        StartCoroutine(DifficultyIncrease());
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        float currentSpeedLeft = Mathf.Min(speedLeft * difficulty, maxSpeedLeft);
        float currentSpeedUp = Mathf.Min(speedUp * difficulty, maxSpeedUp);
        
        rb.linearVelocity = new Vector2(-currentSpeedLeft, currentSpeedUp * verticalDirection);
        
        if (transform.position.y >= 4.5f || transform.position.y <= -4.5f)
        {
            verticalDirection *= -1;
        }
    }

    IEnumerator DifficultyIncrease()
    {
        while (difficulty < 12)
        {
            yield return new WaitForSeconds(30f);
            difficulty++;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Faz o jogador deslizar ao invés de ficar travado
                playerRb.linearVelocity = new Vector2(rb.linearVelocity.x, playerRb.linearVelocity.y);
            }
        }
    }
}
