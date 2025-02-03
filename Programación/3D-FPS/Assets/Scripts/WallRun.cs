using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [SerializeField] Transform orientation;

    [Header("Wall Running")]
    [SerializeField] float wallDistance = 0.6f;
    [SerializeField] float minimumJumpHeight = 1.5f;
    [SerializeField] float wallRunGravity = 1f;

    [Header("Camera")]
    [SerializeField] Camera cam;
    [SerializeField] float fov;
    [SerializeField] float wallRunFOV;
    [SerializeField] float wallRunFOVTime = 10f;
    [SerializeField] float camTilt;
    [SerializeField] float camTiltTime = 10f;

    public ConstantForce cf;
    public float tilt { get; private set; }

    bool wallLeft = false;
    bool wallRight = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;

    private Rigidbody rb;
    public bool isWallRunning { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cf = GetComponent<ConstantForce>();
    }

    public bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }

    void CheckWall()
    {
        wallLeft = CheckWallSide(-orientation.right);
        wallRight = CheckWallSide(orientation.right);
    }

    bool CheckWallSide(Vector3 direction)
    {
        RaycastHit hit;
        Vector3[] rayDirections = new Vector3[]
        {
            direction,
            direction + transform.forward * 0.5f,
            direction - transform.forward * 0.5f
        };

        foreach (var rayDirection in rayDirections)
        {
            if (Physics.Raycast(transform.position, rayDirection, out hit, wallDistance))
            {
                return true;
            }
        }
        return false;
    }

    private void Update()
    {
        CheckWall();

        if (CanWallRun())
        {
            if (wallLeft || wallRight)
            {
                StartWallRun();
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }
    }

    void StartWallRun()
    {
        isWallRunning = true;
        rb.useGravity = false;

        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunFOV, wallRunFOVTime * Time.deltaTime);

        if (wallLeft)
        {
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        }
        else if (wallRight)
        {
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (wallLeft)
            {
                rb.AddForce(orientation.right * 10, ForceMode.Impulse);
                rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            }
            else if (wallRight)
            {
                rb.AddForce(-orientation.right * 10, ForceMode.Impulse);
                rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            }
        }
    }

    void StopWallRun()
    {
        isWallRunning = false;
        rb.useGravity = true;

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallRunFOVTime * Time.deltaTime);
        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (orientation == null) return;

        Gizmos.color = Color.red;

        Vector3[] rayDirections = new Vector3[]
        {
            -orientation.right,
            -orientation.right + transform.forward * 0.5f,
            -orientation.right - transform.forward * 0.5f,
            orientation.right,
            orientation.right + transform.forward * 0.5f,
            orientation.right - transform.forward * 0.5f
        };

        foreach (var rayDirection in rayDirections)
        {
            Gizmos.DrawRay(transform.position, rayDirection * wallDistance);
        }
    }
}
