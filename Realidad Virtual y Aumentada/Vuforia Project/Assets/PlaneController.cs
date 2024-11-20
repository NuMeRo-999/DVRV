using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlaneController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Button accelerateButton;
    public Button brakeButton;
    public Button upButton;
    public Button downButton;
    public Button leftButton;
    public Button rightButton;

    private float speed = 0f;
    private float maxSpeed = 1f; 
    private float acceleration = 0.5f;
    private float rotationSpeed = 20f;

    private Animator animator;

    private bool isAccelerating = false;
    private bool isBraking = false;
    private bool isMovingUp = false;
    private bool isMovingDown = false;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    void Start()
    {
        accelerateButton.onClick.AddListener(() => isAccelerating = true);
        brakeButton.onClick.AddListener(() => isBraking = true);
        upButton.onClick.AddListener(() => isMovingUp = true);
        downButton.onClick.AddListener(() => isMovingDown = true);
        leftButton.onClick.AddListener(() => isMovingLeft = true);
        rightButton.onClick.AddListener(() => isMovingRight = true);
    }

    void Update()
    {
        if (isAccelerating)
        {
            Accelerate();
        }
        if (isBraking)
        {
            Brake();
        }
        if (isMovingUp)
        {
            MoveUp();
        }
        if (isMovingDown)
        {
            MoveDown();
        }
        if (isMovingLeft)
        {
            MoveLeft();
        }
        if (isMovingRight)
        {
            MoveRight();
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (speed > 0)
        {
            if (animator != null)
            {
                animator.SetBool("isMoving", true);
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    void Accelerate()
    {
        speed = Mathf.Clamp(speed + acceleration * Time.deltaTime, 0, maxSpeed);
    }

    void Brake()
    {
        speed = Mathf.Clamp(speed - acceleration * Time.deltaTime, 0, maxSpeed);
    }

    void MoveUp()
    {
        transform.Rotate(Vector3.right, -rotationSpeed * Time.deltaTime);
    }

    void MoveDown()
    {
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }

    void MoveLeft()
    {
        transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
    }

    void MoveRight()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerPress == accelerateButton.gameObject)
        {
            isAccelerating = true;
        }
        else if (eventData.pointerPress == brakeButton.gameObject)
        {
            isBraking = true;
        }
        else if (eventData.pointerPress == upButton.gameObject)
        {
            isMovingUp = true;
        }
        else if (eventData.pointerPress == downButton.gameObject)
        {
            isMovingDown = true;
        }
        else if (eventData.pointerPress == leftButton.gameObject)
        {
            isMovingLeft = true;
        }
        else if (eventData.pointerPress == rightButton.gameObject)
        {
            isMovingRight = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerPress == accelerateButton.gameObject)
        {
            isAccelerating = false;
        }
        else if (eventData.pointerPress == brakeButton.gameObject)
        {
            isBraking = false;
        }
        else if (eventData.pointerPress == upButton.gameObject)
        {
            isMovingUp = false;
        }
        else if (eventData.pointerPress == downButton.gameObject)
        {
            isMovingDown = false;
        }
        else if (eventData.pointerPress == leftButton.gameObject)
        {
            isMovingLeft = false;
        }
        else if (eventData.pointerPress == rightButton.gameObject)
        {
            isMovingRight = false;
        }
    }
}
