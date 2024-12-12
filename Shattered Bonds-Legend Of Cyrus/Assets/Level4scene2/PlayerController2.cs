using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private bool isFlipped = false;

    private BoxCollider2D boxCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        HandleMovement();
    }

    public void SetFlipped(bool flipped)
    {
        isFlipped = flipped;
        // If necessary, adjust collider here
        // Example: boxCollider.offset = new Vector2(0, isFlipped ? newOffset : originalOffset);
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * (isFlipped ? -1 : 1));
        }
    }
}
