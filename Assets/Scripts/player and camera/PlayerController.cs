using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 8f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Teste no PC: tecla espaço ou clique do mouse
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        // No mobile: toque na tela
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.linearVelocity = Vector2.up * jumpForce;
    }
}
