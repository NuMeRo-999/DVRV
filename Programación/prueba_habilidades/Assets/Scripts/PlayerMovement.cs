using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Camera cam;
    
    private Vector3 _velocity;
    private float _rotationY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Physics.gravity = Vector3.down * gravity;
    }

    private void FixedUpdate()
    {
        
        // Apply movement velocity to the Rigidbody
        Vector3 moveDirection = cam.transform.forward * _velocity.z + cam.transform.right * _velocity.x;
        moveDirection.y = 0; // Keep movement on the horizontal plane
        rb.linearVelocity = moveDirection * speed + new Vector3(0, rb.linearVelocity.y, 0); // Maintain existing Y velocity (gravity)
        
    }


    #region Movement

    // Handle movement input
    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _velocity = new Vector3(input.x, 0, input.y);
    }
    
    // Handle jumping
    public void OnJump()
    {
        if (!CheckForGround()) return;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // Handle looking/turning
    public void OnLook(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        // Adjust player rotation (Y-axis)
        transform.Rotate(Vector3.up * input.x * sensitivity);

        // Adjust camera rotation (X-axis, clamped)
        _rotationY -= input.y * sensitivity;
        _rotationY = Mathf.Clamp(_rotationY, -80f, 75f);
        cam.transform.localEulerAngles = new Vector3(_rotationY, 0f, 0f);
    }
    
    
    // Check if the player is on the ground
    private bool CheckForGround()
    {
        return Physics.Raycast(groundCheck.position, Vector3.down, out _, 0.3f);
    }
    #endregion

    #region Camera

    public Camera GetCamera()
    {
        return cam;
    }

    #endregion

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * 0.3f);
    }
}