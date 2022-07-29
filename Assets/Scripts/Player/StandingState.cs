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

    float timeForNextAttack;
    float currentAttackTime = 0;

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
        timeForNextAttack = character.TimeForNextAttack;
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

        if (dashAction.triggered&& GroundCheck && Mathf.Abs(input.x) > 0.5f)
        {
            if(UIManager.instance.ConsumeChakras(character.DashCost)){
                dash = true;
            }
        }
        
        if (attackAction.triggered&& GroundCheck && currentAttackTime >= timeForNextAttack)
        {
            attack = true;
            currentAttackTime = 0;
        }

        if(shootAction.triggered && GroundCheck)
        {
            if (UIManager.instance.ConsumeChakras(character.ShurikenCost))
            {
                shoot = true;
            }
        }

        /*		if (drawWeaponAction.triggered)
                {
                    drawWeapon = true;
                }*/

        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0,0);

        //velocity = velocity.x * character.cameraTransform.right.normalized;// + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;
     
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        character.animator.SetFloat("speed", Mathf.Abs(input.x), character.speedDampTime, Time.deltaTime);

        if (!GroundCheck && !falling)
        {
            character.animator.SetTrigger("fall");
            falling = true;
        }
        else if (GroundCheck && falling)
        {
            falling = false;
            character.animator.SetTrigger("move");
        }

        if (jump )
        {
            stateMachine.ChangeState(character.jumping);
        }
		if (dash)
		{
            stateMachine.ChangeState(character.dashing);
        }



        else if (attack)
        {
            stateMachine.ChangeState(character.attacking);
        }

        if (shoot)
        {
            stateMachine.ChangeState(character.shooting);
        }



        timePassed += Time.deltaTime;
        currentAttackTime += Time.deltaTime;
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
        Vector3 ray1Pos = new Vector3( character.transform.position.x + character.GroundCheckDisnatce, character.transform.position.y+0.5f, character.transform.position.z);
        Vector3 ray2Pos = new Vector3(character.transform.position.x - character.GroundCheckDisnatce, character.transform.position.y+0.5f, character.transform.position.z);

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


public class EnemyStandState : EnemyState
{

    float gravityValue;
    float playerSpeed;
    Vector3 currentVelocity;

    Vector3 cVelocity;
    bool grounded;
    bool attacking;

    Transform target;

    public EnemyStandState (Enemy _character, EnemyStateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        attacking = false;
        target = null;
        gravityVelocity.y = 0;
        velocity = character.playerVelocity;
        playerSpeed = character.playerSpeed;
        gravityValue = character.gravityValue;
        grounded = character.controller.isGrounded;
        currentVelocity = Vector3.zero;


    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        character.animator.SetFloat("Speed", Mathf.Abs(velocity.x), character.speedDampTime, Time.deltaTime);
        if (attacking)
        {
            stateMachine.ChangeState(character.attacking);

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // Gravity and ground check
        gravityVelocity.y += gravityValue * Time.deltaTime;

        grounded = character.controller.isGrounded;

        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }

        // Detection to run

        target = SearchForPlayer();
        if(character.canMove)
            SetVelocity();


        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, character.velocityDampTime);
        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);
        
        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.LookRotation(velocity);//Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity),character.rotationDampTime);
        }
        // detect to attack
        CheckToAttack();

    }


    Transform SearchForPlayer()
    {
        // player layer
        int layerMask = 1 << 8;
        //layerMask = ~layerMask;

        // cast ray

        Collider[] hitcollider = Physics.OverlapBox(
            new Vector3(character.transform.position.x, character.transform.position.y + character.yOffset, character.transform.position.z),
            new Vector3(character.RunRange + character.SizeOffset.x, 1 + character.SizeOffset.y, 1 + character.SizeOffset.z)/2, Quaternion.identity, layerMask);

        for (int i = 0; i < hitcollider.Length;)
        {
            return hitcollider[i].transform;
        }

        return null;
    }


    void SetVelocity()
    {
        if (target != null)
        {
            //check direction to move
            float ex = character.transform.position.x;
            float px = target.position.x;
            if (ex > px) velocity = new Vector3(-1, 0);
            else velocity = new Vector3(1, 0);

            //check to stop or not
            Vector3 eV = new Vector3(character.transform.position.x, 0);
            Vector3 pV = new Vector3(target.position.x, 0);
            if (Vector3.Distance(eV, pV) < character.stopDistance)
            {
                velocity = new Vector3(0, 0);
            }
        }
        else
        {
            velocity = new Vector3(0, 0);
        }
        
    }

    void CheckToAttack()
    {
        int layerMask = 1 << 8;
        //layerMask = ~layerMask;

        // cast ray

        Collider[] hitcollider = Physics.OverlapBox(
            new Vector3(character.transform.position.x, character.transform.position.y + character.yOffset, character.transform.position.z),
            new Vector3(character.AttackRange+character.SizeOffset.x, 1 + character.SizeOffset.y, 1 + character.SizeOffset.z)/2, Quaternion.identity, layerMask);

        for (int i = 0; i < hitcollider.Length;)
        {
            attacking = true;

            Vector3 AttackDirection;

            float ex = character.transform.position.x;
            float px = target.position.x;
            if (ex > px) AttackDirection = new Vector3(-1, 0);
            else AttackDirection = new Vector3(1, 0);

            character.transform.rotation = Quaternion.LookRotation(AttackDirection);//Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity),character.rotationDampTime);

            return;
        }

        attacking = false;
    }
}