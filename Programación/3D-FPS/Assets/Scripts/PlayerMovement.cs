using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float movementMultiplier = 10f;
    float playerHeight = 2f;
    [SerializeField] float airMultiplier = 0.4f;
    [SerializeField] Transform orientation;

    [Header("Jumping")]
    [SerializeField] float jumpForce = 5f;

    [Header("Springting")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float runSpeed = 6f;
    [SerializeField] float acceleration = 10f;

    [Header("Crouching and Sliding")]
    [SerializeField] KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] float crouchSpeed = 3f;
    [SerializeField] float slideSpeed = 8f;
    [SerializeField] float crouchHeight = 1f;
    [SerializeField] float slideDuration = 1f;
    [SerializeField] CapsuleCollider playerCollider;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Drag")]
    float groundDrag = 6f;
    float airDrag = 2f;

    [Header("Ground Detection")]
    [SerializeField] LayerMask groundMask;
    public bool isGrounded;
    float groundDistance = 0.4f;

    [Header("AirDash")]
    public float dashForce = 10f;
    public float dashCounter = 1f;

    [Header("Double Jump")]
    public bool canDoubleJump = true;
    public float jumpCounter = 1f;


    public float gravity = -15f;
    public ConstantForce cf;

    private WallRun wallRun;

    float horizontalMovement;
    float verticalMovement;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;

    RaycastHit slopeHit;

    private bool isCrouching = false;
    private bool isSliding = false;
    private float originalHeight;

    private bool onSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            return false;
        }
        return false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cf = GetComponent<ConstantForce>();
        wallRun = GetComponent<WallRun>();

        originalHeight = playerHeight;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 1, 0), groundDistance, groundMask);

        if (Input.GetKeyDown(jumpKey) && isGrounded && !isCrouching)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(jumpKey) && !isGrounded && jumpCounter != 0 && !wallRun.isWallRunning)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCounter--;
        }

        if (!isGrounded && !wallRun.isWallRunning)
        {
            cf.force = new Vector3(0, gravity, 0);
        }
        else
        {
            cf.force = new Vector3(0, 0, 0);
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        MyInput();
        ControlDrag();
        ControlSpeed();
        HandleCrouchAndSlide();
        AirDash();
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    void ControlSpeed()
    {
        if (isSliding)
        {
            moveSpeed = slideSpeed;
        }
        else if (Input.GetKey(sprintKey) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, runSpeed, acceleration * Time.deltaTime);
        }
        else if (isCrouching)
        {
            moveSpeed = crouchSpeed;
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void HandleCrouchAndSlide()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            isCrouching = true;
            playerHeight = crouchHeight;
            playerCollider.height = crouchHeight;

            if (Input.GetKey(sprintKey) && isGrounded)
            {
                StartCoroutine(Slide());
            }
        }

        if (Input.GetKeyUp(crouchKey))
        {
            if (!Physics.Raycast(transform.position, Vector3.up, 2f))
            {
                playerHeight = originalHeight;
                playerCollider.height = originalHeight;
            }
                isCrouching = false;
        }

        if (!isCrouching && playerCollider.height != originalHeight)
        {
            if (!Physics.Raycast(transform.position, Vector3.up, 2f))
            {
                playerCollider.height = originalHeight;
            }
        }

    }

    /*bool IsObstacleAbove()
    {
        float capsuleRadius = playerCollider.radius;
        float capsuleHeight = originalHeight - crouchHeight;
        Vector3 capsuleStart = transform.position + Vector3.up * (crouchHeight / 2);
        Vector3 capsuleEnd = capsuleStart + Vector3.up * capsuleHeight;

        return Physics.CheckCapsule(capsuleStart, capsuleEnd, capsuleRadius, 6);
    }

    private void OnDrawGizmos()
    {
        float capsuleRadius = playerCollider.radius;
        float capsuleHeight = originalHeight - crouchHeight;
        Vector3 capsuleStart = transform.position + Vector3.up * (crouchHeight / 2);
        Vector3 capsuleEnd = capsuleStart + Vector3.up * capsuleHeight;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(capsuleStart, capsuleRadius);
        Gizmos.DrawWireSphere(capsuleEnd, capsuleRadius);
        Gizmos.DrawLine(capsuleStart + Vector3.forward * capsuleRadius, capsuleEnd + Vector3.forward * capsuleRadius);
        Gizmos.DrawLine(capsuleStart - Vector3.forward * capsuleRadius, capsuleEnd - Vector3.forward * capsuleRadius);
        Gizmos.DrawLine(capsuleStart + Vector3.right * capsuleRadius, capsuleEnd + Vector3.right * capsuleRadius);
        Gizmos.DrawLine(capsuleStart - Vector3.right * capsuleRadius, capsuleEnd - Vector3.right * capsuleRadius);
    }*/

    IEnumerator Slide()
    {
        isSliding = true;
        yield return new WaitForSeconds(slideDuration);
        isSliding = false;
    }

    void AirDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isGrounded && dashCounter != 0)
        {
            if(horizontalMovement != 0 || verticalMovement != 0)
            {
                rb.AddForce(moveDirection.normalized * dashForce, ForceMode.Impulse);
                dashCounter--;
            }
            else
            {
                rb.AddForce(orientation.forward * dashForce, ForceMode.Impulse);
                dashCounter--;
            }
        }

        if (isGrounded)
        {
            dashCounter = 1f;
            jumpCounter = 1f;
        }
    }

    private void FixedUpdate()
    {
        if (!isGrounded && !onSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && onSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
    }
}
