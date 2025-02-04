using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class WallRun : MonoBehaviour
{
    [SerializeField] Transform orientation;
    [SerializeField] CinemachineCamera vcam; // Cinemachine Camera

    [Header("Wall Running")]
    [SerializeField] float wallDistance = 0.6f;
    [SerializeField] float minimumJumpHeight = 1.5f;
    [SerializeField] float wallRunGravity = 1f;

    [Header("Camera")]
    [SerializeField] float fov;
    [SerializeField] float wallRunFOV;
    [SerializeField] float wallRunFOVTime = 10f;
    [SerializeField] float camTilt;
    [SerializeField] float camTiltTime = 10f;

    private Rigidbody rb;
    public bool isWallRunning { get; private set; }

    bool wallLeft = false;
    bool wallRight = false;

    public LayerMask wallMask;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight, wallMask);
    }

    void CheckWall()
    {
        wallLeft = CheckWallSide(-orientation.right);
        wallRight = CheckWallSide(orientation.right);
    }

    bool CheckWallSide(Vector3 direction)
    {
        return Physics.Raycast(transform.position, direction, wallDistance, wallMask);
    }

    private void Update()
    {
        CheckWall();

        if (CanWallRun() && (wallLeft || wallRight))
            StartWallRun();
        else
            StopWallRun();
    }

    void StartWallRun()
    {
        isWallRunning = true;
        rb.useGravity = false;
        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

        // Ajustar FOV
        vcam.Lens.FieldOfView = Mathf.Lerp(vcam.Lens.FieldOfView, wallRunFOV, wallRunFOVTime * Time.deltaTime);

        // Ajustar inclinación lateral con Dutch
        if (wallLeft)
            vcam.Lens.Dutch = Mathf.Lerp(vcam.Lens.Dutch, -camTilt, camTiltTime * Time.deltaTime);
        else if (wallRight)
            vcam.Lens.Dutch = Mathf.Lerp(vcam.Lens.Dutch, camTilt, camTiltTime * Time.deltaTime);

        // Salto desde la pared
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 jumpDirection = (wallLeft ? orientation.right : -orientation.right) + Vector3.up;
            rb.AddForce(jumpDirection * 10, ForceMode.Impulse);
        }
    }

    void StopWallRun()
    {
        isWallRunning = false;
        rb.useGravity = true;

        // Restaurar FOV y Dutch
        vcam.Lens.FieldOfView = Mathf.Lerp(vcam.Lens.FieldOfView, fov, wallRunFOVTime * Time.deltaTime);
        vcam.Lens.Dutch = Mathf.Lerp(vcam.Lens.Dutch, 0, camTiltTime * Time.deltaTime);
    }
}
