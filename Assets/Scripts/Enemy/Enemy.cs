using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Enemy : MonoBehaviour
{

    public Transform Mesh;
    [Header("Controls")]
    public bool canMove = false;
    public float playerSpeed = 5.0f;
    public float gravityMultiplier = 2;

    // states
    [HideInInspector] public EnemyStateMachine movementSM;
    public EnemyStandState standing;
    public EnemyAttackState attacking;
    public EnemyDamageState damaging;

    [HideInInspector]
    public float gravityValue = -9.81f;
    [HideInInspector]
    public float normalColliderHeight;
    [HideInInspector]
    public CharacterController controller;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Vector3 playerVelocity;

    [Header("Animation Smoothing")]
    [Range(0, 1)]
    public float speedDampTime = 0.1f;
    [Range(0, 1)]
    public float velocityDampTime = 0.9f;
    [Range(0, 1)]
    public float rotationDampTime = 0.2f;
    [Range(0, 1)]
    public float airControl = 0.5f;

    [Header("Attack Settings")]
    public float AttackTimings = .5f;
    public float AttackWaitTimings = .5f;
    public float Damagetime = .25f;



    [Header("Detection Settings")]
    public float RunRange;
    public float AttackRange;
    public float stopDistance;
    public Vector3 SizeOffset;
    public float yOffset;

    float Initz;

    private void Awake()
    {
        Initz = transform.position.z;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        movementSM = new EnemyStateMachine();
        standing = new EnemyStandState(this,movementSM);
        attacking = new EnemyAttackState(this, movementSM);
        damaging = new EnemyDamageState(this, movementSM);

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
        Mesh.transform.position = new Vector3(transform.position.x, transform.position.y - 0.039f, transform.position.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y+ yOffset, transform.position.z), new Vector3(RunRange+ SizeOffset.x, 1 + SizeOffset.y, 1 + SizeOffset.z));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z), new Vector3(AttackRange + SizeOffset.x, 1 + SizeOffset.y, 1 + SizeOffset.z));
    }

}
