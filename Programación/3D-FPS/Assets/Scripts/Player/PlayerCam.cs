using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Transform orientation;
    public Transform player;
    public PlayerMovement playerMovement;
    public Transform weapon; // Referencia al arma

    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        transform.position = player.position;
    }

    void LateUpdate()
    {
        float mouseX = playerMovement.lookInput.x * Time.deltaTime * sensX;
        float mouseY = playerMovement.lookInput.y * Time.deltaTime * sensY;


        // print(lookInput.x);
        // print(lookInput.y);


        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // player.Rotate(Vector3.up * (mouseX * Time.deltaTime) * sensX);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);

        // Asegúrate de que el arma siga la rotación de la cámara
        // if (weapon != null)
        // {
        // weapon.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        // }
    }
}