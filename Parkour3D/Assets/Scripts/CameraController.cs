using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensitivity for mouse movement
    public float movementSpeed = 5f; // Speed of player movement
    public float jumpForce = 5f; // Force of the jump
    public float groundCheckDistance = 1.1f; // Distance for ground check (adjustable in Inspector)

    private float xRotation = 0f;
    private Rigidbody rb;

    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Get the Rigidbody component from the player
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
        HandleMouseLook();
        HandleMovement();
        HandleJump();
    }

    void HandleMouseLook()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust camera rotation on the X-axis (vertical rotation)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent over-rotation

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.parent.Rotate(Vector3.up * mouseX); // Rotate the player horizontally
    }

    void HandleMovement()
    {
        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal"); // A/D keys or Left/Right arrows
        float vertical = Input.GetAxis("Vertical"); // W/S keys or Up/Down arrows

        // Calculate movement direction
        Vector3 move = transform.parent.right * horizontal + transform.parent.forward * vertical;

        // Apply movement
        rb.MovePosition(rb.position + move * movementSpeed * Time.deltaTime);
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
        // Check if the player is touching the ground
        return Physics.Raycast(transform.parent.position, Vector3.down, groundCheckDistance);
    }
}
