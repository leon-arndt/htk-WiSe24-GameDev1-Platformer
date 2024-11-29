using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.4f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Move the enemy
        rb.velocity = new Vector2(movingRight ? speed : -speed, rb.velocity.y);

        // Check for wall or edge
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, Vector2.right * (movingRight ? 1 : -1), 0.1f, groundLayer);
        bool isGroundAhead = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (wallHit.collider != null || !isGroundAhead)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize ground check
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}