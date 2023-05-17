
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(Animator))]
public class DogController : MonoBehaviour
{
    [SerializeField] private Transform cam;

    [Header("Keyboard Input")]
    private DogInputActions dogInputActions;

    // Variables for movement input
    [SerializeField] Vector2 currentMovementInput;
    [SerializeField] Vector3 currentMovement;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSmoothTime = 0.15f;
    private float turnSmoothVelocity;

    [Header("Animations")]
    private Animator animator;
    private int isWalkingHash;
    private int directionHash;
    private int isInteractHash;

    // Variables for movement state
    [SerializeField] private bool isMovementPressed;
    [SerializeField] private bool isWalking;

    private void Awake()
    {
        dogInputActions = new DogInputActions();
        animator = GetComponent<Animator>();
        cam = Camera.main.transform;
        isWalkingHash = Animator.StringToHash("isWalking");
        directionHash = Animator.StringToHash("direction");
        isInteractHash = Animator.StringToHash("isInteract");

        dogInputActions.Player.Move.started += OnMovementInput;
        dogInputActions.Player.Move.canceled += OnMovementInput;
        dogInputActions.Player.Move.performed += OnMovementInput;

        dogInputActions.Player.Interact.performed += OnInteractInput;
    }

    private void OnEnable()
    {
        dogInputActions.Player.Enable();
    }
    private void OnDisable()
    {
        dogInputActions.Player.Disable();
    }
    private void Update()
    {
        HandleAnimations();
        if (isMovementPressed)
        {
            // Rotate the dog towards the movement direction
            float targetAngle = Mathf.Atan2(currentMovement.x, currentMovement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f).normalized;

            // Move the dog in the movement direction
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
        }
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        // Handle movement input
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.y = 0.0f;
        currentMovement.z = currentMovementInput.y;
        currentMovement = currentMovement.normalized;
        isMovementPressed = (currentMovement != Vector3.zero) && (currentMovement.magnitude >= 0.1f);
    }

    private void OnInteractInput(InputAction.CallbackContext context)
    {
        // Handle interact input
        Debug.Log("Interact!");
        animator.SetTrigger(isInteractHash);
    }
    private void HandleAnimations()
    {
        isWalking = animator.GetBool(isWalkingHash);
        // Determine idle or walking state based on movement input
        if (isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }
    }

}
