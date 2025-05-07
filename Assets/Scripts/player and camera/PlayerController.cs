using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.linearVelocity = Vector2.up * jumpForce;
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            PlayerStatus status = GetComponent<PlayerStatus>();
            if(status != null)
            {
                status.TakeDamage(1);
                if(status.lifePoints <= 0)
                {
                    this.enabled = false;
                }
            }     

        }
    }
}
