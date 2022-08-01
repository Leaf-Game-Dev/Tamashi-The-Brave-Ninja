using UnityEngine;

public class JumpingState:State
{
    bool grounded;

    float gravityValue;
    float jumpHeight;
    float playerSpeed;


    Vector3 airVelocity;

    public JumpingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
	{
		character = _character;
		stateMachine = _stateMachine;
	}

    public override void Enter()
	{
		base.Enter();

		grounded = false;
        gravityValue = character.gravityValue;
        jumpHeight = character.jumpHeight;
        playerSpeed = character.playerSpeed;
        gravityVelocity.y = 0;
        doublejumped = false;
        //character.controller.height = character.JumpColliderHeight;
        //character.controller.center = new Vector3(0f, 1 , 0f);
        character.animator.SetFloat("speed", 0);
        character.animator.SetTrigger("jump");
        Jump();
	}

    public override void Exit()
    {
        base.Exit();
        //character.controller.height = character.normalColliderHeight;
        //character.controller.center = new Vector3(0f, character.normalColliderHeight / 2f, 0f);
    }

    public override void HandleInput()
	{
		base.HandleInput();

        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, 0);

        velocity = velocity.x * character.cameraTransform.right.normalized;// + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;
    }
    bool doublejumped;
	public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (grounded)
		{
            character.animator.SetTrigger("move");
            stateMachine.ChangeState(character.standing);

            SoundManager.PlaySound(SoundManager.Sound.FootStep, 0.1f);
            //stateMachine.ChangeState(character.landing);
        }

        if (jumpAction.triggered && !doublejumped)
        {
            Jump();
            doublejumped = true;
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
		if (!grounded)
		{

            //velocity = character.playerVelocity;
            airVelocity = new Vector3(input.x, 0, 0);

            //velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
            //velocity.y = 0f;
            airVelocity = airVelocity.x * character.cameraTransform.right.normalized + airVelocity.z * character.cameraTransform.forward.normalized;
            airVelocity.y = 0f;
            character.controller.Move(gravityVelocity * Time.deltaTime+ (airVelocity*character.airControl+velocity*(1- character.airControl))*playerSpeed*Time.deltaTime);


            if (velocity.magnitude > 0)
            {
                character.transform.rotation = Quaternion.LookRotation(velocity);//Quaternion.Slerp(character.transform.rotation, , character.rotationDampTime);
            }

        }

        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;
    }

    void Jump(float additional = 0)
    {
        gravityVelocity.y = Mathf.Sqrt((jumpHeight+additional) * -3.0f * gravityValue);
    }

}

