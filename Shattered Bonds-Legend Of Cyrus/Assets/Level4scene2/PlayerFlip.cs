using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFlipped = false;
    private bool canFlip = true;

    public LayerMask groundLayer;
    public LayerMask ceilingLayer;

    private bool isGrounded;
    private bool isOnCeiling;

    // Reference to PlayerMovement to inform about flip state
    private PlayerController2 playerMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerController2>();
    }

    void Update()
    {
        HandleFlipInput();
        //CheckPosition();
    }

    void HandleFlipInput()
    {
        if (Input.GetKeyDown(KeyCode.L) && canFlip)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFlipped = !isFlipped;

        // Invert gravity
        rb.gravityScale *= -1;

        // Flip the sprite
        Vector3 localScale = transform.localScale;
        localScale.y *= -1;
        transform.localScale = localScale;


       //NOT IMP
        //// Adjust position to prevent clipping
       // Vector3 position = transform.position;
        //position.y += isFlipped ? 1f : -1f; // Adjust as needed
      // transform.position = position;

        // Inform PlayerMovement about the flip
        playerMovement.SetFlipped(isFlipped);
    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    if (((1 << collision.gameObject.layer) & groundLayer) != 0)
    {
        isGrounded = true;
    }
    if (((1 << collision.gameObject.layer) & ceilingLayer) != 0)
    {
        isOnCeiling = true;
    }
    canFlip = isGrounded || isOnCeiling;
}

private void OnCollisionExit2D(Collision2D collision)
{
    if (((1 << collision.gameObject.layer) & groundLayer) != 0)
    {
        isGrounded = false;
    }
    if (((1 << collision.gameObject.layer) & ceilingLayer) != 0)
    {
        isOnCeiling = false;
    }
    canFlip = isGrounded || isOnCeiling;
}

}
