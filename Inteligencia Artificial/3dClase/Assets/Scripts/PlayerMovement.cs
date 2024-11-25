using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Look Settings")]
    public float lookSensitivity = 1f; // Ajusta la sensibilidad del ratón

    private CharacterController characterController;

    private Vector2 inputDirection;
    private Vector2 lookInput;
    private Vector3 velocity;
    private bool isGrounded;

    private float xRotation = 0f; // Rotación acumulada en el eje X (para la cámara)

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        // Verificar si está en el suelo
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Mantener al jugador pegado al suelo
        }

        // Movimiento horizontal
        Vector3 move = new Vector3(inputDirection.x, 0, inputDirection.y);
        move = transform.TransformDirection(move); // Convertir a espacio mundial
        characterController.Move(move * moveSpeed * Time.deltaTime);

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void RotatePlayer()
    {
        // Rotación horizontal del jugador (eje Y)
        transform.Rotate(0, lookInput.x * lookSensitivity * Time.deltaTime, 0);

        // Rotación vertical (eje X, generalmente para la cámara)
        xRotation -= lookInput.y * lookSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitar ángulo vertical
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void OnMove(InputValue value)
    {
        // Guardar la dirección del movimiento
        inputDirection = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        // Manejar el salto
        if (isGrounded && value.isPressed)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void OnLook(InputValue value)
    {
        // Guardar la entrada de mirar alrededor
        lookInput = value.Get<Vector2>();
    }
}
