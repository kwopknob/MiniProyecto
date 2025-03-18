

using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public float currentMoveSpeed;
    public float walkSpeed = 3, walkBackSpeed = 2;
    public float runSpeed = 7, runBackSpeed = 5;
    [SerializeField] private float groundYOffset = 0.1f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] float jumpForce = 10;
    [HideInInspector] public bool jumped;

     MovementBaseState currentState;
    public MovementBaseState previousState;
   
    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public RunState Run = new RunState();
    public JumpState Jump = new JumpState();

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
        Falling();
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
    public bool IsGrounded()
    {
        spherePosition = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        return Physics.CheckSphere(spherePosition, controller.radius - 0.05f, groundMask);
    }

    public void Falling()
    {
        animator.SetBool("Falling", !IsGrounded());
    }

    private void ApplyGravity()
    {
        if (IsGrounded())
        {
           
            if (velocity.y < 0)
            {
                velocity.y = -2f; 
            }
        }
        else
        {
           
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

    public void Jumporce() => velocity.y += jumpForce;

    public void Jumped() => jumped = true;
}