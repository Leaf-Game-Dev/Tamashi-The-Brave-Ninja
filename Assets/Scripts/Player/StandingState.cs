using UnityEngine;

public class StandingState: State
{  
    float gravityValue;
    bool jump;
    bool dash;
    bool attack;
    bool shoot;
    Vector3 currentVelocity;
    bool grounded;
    float playerSpeed;
    bool GroundCheck;
    float timePassed;
    float landingTime;
    bool falling;

    //bool drawWeapon;

    Vector3 cVelocity;

    public StandingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
	{
		character = _character;
		stateMachine = _stateMachine;
	}

    public override void Enter()
    {
        base.Enter();

        jump = false;
        dash = false;
        shoot = false;
        attack = false;
        GroundCheck = true;
        //drawWeapon = false;
        falling = false;
        input = Vector2.zero;
        
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;

        velocity = character.playerVelocity;
        playerSpeed = character.playerSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;

        timePassed = 0f;
        landingTime = character.LandDelay;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (jumpAction.triggered && timePassed > landingTime && GroundCheck)
        {
            jump = true;
		}

        if (dashAction.triggered&& GroundCheck)
        {
            dash = true;
        }
        
        if (attackAction.triggered&& GroundCheck)
        {
            attack = true;
        }

        if(shootAction.triggered && GroundCheck)
        {
            shoot = true;
        }

        /*		if (drawWeaponAction.triggered)
                {
                    drawWeapon = true;
                }*/

        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0,0);

        velocity = velocity.x * character.cameraTransform.right.normalized;// + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;
     
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        character.animator.SetFloat("speed", Mathf.Abs(input.x), character.speedDampTime, Time.deltaTime);

        if (jump )
        {
            stateMachine.ChangeState(character.jumping);
        }
		if (dash)
		{
            stateMachine.ChangeState(character.dashing);
        }
        if (attack)
		{
            stateMachine.ChangeState(character.attacking);
        }

        if (shoot)
        {
            stateMachine.ChangeState(character.shooting);
        }

        if (!GroundCheck && !falling)
        {
            character.animator.SetTrigger("fall");
            falling = true;
        }
        else if(GroundCheck && falling)
        {
            falling = false;
            character.animator.SetTrigger("move");
        }

        timePassed += Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;

        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }

        GroundCheck = CheckCollisionOverlap(character.transform.position + Vector3.down * character.normalColliderHeight);





        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity,ref cVelocity, character.velocityDampTime);
        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);
  
		if (velocity.sqrMagnitude>0)
		{
            character.transform.rotation = Quaternion.LookRotation(velocity);//Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity),character.rotationDampTime);
        }
        
    }

    public override void Exit()
    {
        base.Exit();

        gravityVelocity.y = 0f;
        character.playerVelocity = new Vector3(input.x, 0, input.y);

        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.LookRotation(velocity);
        }
    }

    public bool CheckCollisionOverlap(Vector3 targetPositon)
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        RaycastHit hit2;


        Vector3 direction = targetPositon - character.transform.position;
        Vector3 ray1Pos = new Vector3( character.transform.position.x + character.GroundCheckDisnatce, character.transform.position.y, character.transform.position.z);
        Vector3 ray2Pos = new Vector3(character.transform.position.x - character.GroundCheckDisnatce, character.transform.position.y, character.transform.position.z);

        bool ray1 = Physics.Raycast(ray1Pos, direction, out hit, character.normalColliderHeight / 4, layerMask);
        bool ray2 = Physics.Raycast(ray2Pos, direction, out hit2, character.normalColliderHeight / 4, layerMask);

        if (ray2|| ray1)
        {
            if (ray1) Debug.DrawRay(ray1Pos, direction * hit.distance, Color.yellow);
            else Debug.DrawRay(ray1Pos, direction * character.normalColliderHeight / 4, Color.white);
            if (ray2) Debug.DrawRay(ray2Pos, direction * hit2.distance, Color.yellow);
            else Debug.DrawRay(ray2Pos, direction * character.normalColliderHeight / 4, Color.white);

            return true;
        }
        else
        {
            Debug.DrawRay(ray1Pos, direction * character.normalColliderHeight / 4, Color.white);
            Debug.DrawRay(ray2Pos, direction * character.normalColliderHeight / 4, Color.white);

            return false;
        }
    }

}
