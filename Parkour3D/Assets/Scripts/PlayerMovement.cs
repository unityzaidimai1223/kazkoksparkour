using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f; // Speed of the player
    public float jumpForce = 5f; // Force of the jump
    public float groundCheckDistance = 1.1f; // Distance to check if the player is on the ground

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on the Player object. Please add a Rigidbody component.");
        }

        // Prevent the Rigidbody from tipping over
        rb.freezeRotation = true;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal"); // A/D keys or Left/Right arrows
        float vertical = Input.GetAxis("Vertical"); // W/S keys or Up/Down arrows

        // Calculate movement direction
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        // Apply movement
        Vector3 newPosition = rb.position + move * movementSpeed * Time.deltaTime;
        rb.MovePosition(newPosition);
    }

    void HandleJump()
    {
        // Check if the player is grounded
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        // Perform a raycast to check if the player is touching the ground
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }
}
