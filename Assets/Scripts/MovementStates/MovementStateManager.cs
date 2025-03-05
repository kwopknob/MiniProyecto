

using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public float currentMoveSpeed;
    public float walkSpeed = 3, walkBackSpeed = 2;
    public float runSpeed = 7, runBackSpeed = 5;
    [SerializeField] private float groundYOffset = 0.1f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity = -9.81f;
     MovementBaseState currentState;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public RunState Run = new RunState();

    [HideInInspector] public Animator animator;

    private CharacterController controller;
    public Vector3 velocity;
    public Vector3 movementDirection;
    public Vector3 spherePosition;

    public float horizontalInput;
    public float verticalInput;


    private void Start()
    {
  
        controller = GetComponent<CharacterController>();
        SwitchState(Idle);
        animator = GetComponentInChildren<Animator>();
   
    }

    private void Update()
    {
       
        GetDirection();

      
        ApplyGravity();
        currentState.UpdateState(this);
      
        Vector3 finalMove = movementDirection * currentMoveSpeed + velocity;
        controller.Move(finalMove * Time.deltaTime);

        animator.SetFloat("hzInput", horizontalInput);
        animator.SetFloat("vInput", verticalInput);
    }
    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    private void GetDirection()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        movementDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        controller.Move(movementDirection.normalized * currentMoveSpeed * Time.deltaTime);
    }
    private bool IsGrounded()
    {
        spherePosition = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        return Physics.CheckSphere(spherePosition, controller.radius - 0.05f, groundMask);
    }

    private void ApplyGravity()
    {
        if (IsGrounded())
        {
            // Reset gravity when grounded
            if (velocity.y < 0)
            {
                velocity.y = -2f; // Slight negative value to ensure contact with the ground
            }
        }
        else
        {
            // Apply gravity over time when not grounded
            velocity.y += gravity * Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the ground check sphere in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(
        new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z),
            controller != null ? controller.radius - 0.05f : 0.5f
        );
    }
}