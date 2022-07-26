using UnityEngine;
using UnityEngine.InputSystem;
public class Character : MonoBehaviour
{
    public Transform Mesh; 
    [Header("Controls")]
    public float playerSpeed = 5.0f;
    public float crouchSpeed = 2.0f;
    public float sprintSpeed = 7.0f;
    public float jumpHeight = 0.8f; 
    public float gravityMultiplier = 2;
    public float rotationSpeed = 5f;
    public float crouchColliderHeight = 1.35f;
    public float JumpColliderHeight = 1.35f;
    public float LandDelay = 0.75f;
    public float GroundCheckDisnatce = 0.3f;


    [Header("Animation Smoothing")]
    [Range(0, 1)]
    public float speedDampTime = 0.1f;
    [Range(0, 1)]
    public float velocityDampTime = 0.9f;
    [Range(0, 1)]
    public float rotationDampTime = 0.2f;
    [Range(0, 1)]
    public float airControl = 0.5f;

    [Header("Dash settings")]
    public float DashSpeed;
    public float DashTime;

    [Header("Attack Settings")]
    public float AttackTimings = .5f;

    [HideInInspector] public StateMachine movementSM;
    public StandingState standing;
    public JumpingState jumping;
    public DashState dashing;
    public CrouchingState crouching;
    public LandingState landing;

    //public SprintJumpState sprintjumping;
/*    public CombatState combatting;*/
    public AttackState attacking;
    public ShurikenState shooting;


    [HideInInspector]
    public float gravityValue = -9.81f;
    [HideInInspector]
    public float normalColliderHeight;
    [HideInInspector]
    public CharacterController controller;
    [HideInInspector]
    public PlayerInput playerInput;
    [HideInInspector]
    public Transform cameraTransform;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Vector3 playerVelocity;

    float Initz;

    private void Awake()
    {
        Initz = transform.position.z;
    }
    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;

        movementSM = new StateMachine();
        standing = new StandingState(this, movementSM);
        jumping = new JumpingState(this, movementSM);
        dashing = new DashState(this, movementSM);
        crouching = new CrouchingState(this, movementSM);
        landing = new LandingState(this, movementSM);
        attacking = new AttackState(this, movementSM);
        shooting = new ShurikenState(this, movementSM);

        movementSM.Initialize(standing);

        normalColliderHeight = controller.height;
        gravityValue *= gravityMultiplier;
    }

    private void Update()
    {
        movementSM.currentState.HandleInput();

        movementSM.currentState.LogicUpdate();

    }

    private void FixedUpdate()
    {
        movementSM.currentState.PhysicsUpdate();

        Constrains();

    }

    private void Constrains()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Initz);
        //Mesh.rotation = transform.rotation;
        Mesh.transform.position = transform.position;
    }

}
