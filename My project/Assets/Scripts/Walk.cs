using UnityEngine;

public class Player_movement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator animator;
    private bool facingRight = true; // Tracks the direction the player is facing

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Get the horizontal input (A/D keys or Left/Right arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal");

        // If input is detected, move the player
        if (horizontalInput != 0)
        {
            // Set the velocity of the Rigidbody2D based on the input and speed
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        }
        else
        {
            // Stop horizontal movement when no input is detected
            body.velocity = new Vector2(0, body.velocity.y);
        }

        // Update the "Speed" parameter in the Animator with the absolute value of horizontalInput
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // Check if the player is moving right and needs to face right
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        // Check if the player is moving left and needs to face left
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }
    }

    // This method flips the player's sprite by inverting the x scale
    private void Flip()
    {
        // Toggle the direction the player is facing
        facingRight = !facingRight;

        // Get the current scale
        Vector3 scale = transform.localScale;

        // Invert the x-axis scale to flip the sprite
        scale.x *= -1;

        // Apply the new scale to the transform
        transform.localScale = scale;
    }
}
