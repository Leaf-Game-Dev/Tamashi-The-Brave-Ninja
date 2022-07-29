using UnityEngine;
using UnityEngine.InputSystem;

public class State
{
    public Character character;
    public StateMachine stateMachine;

    protected Vector3 gravityVelocity;
    protected Vector3 velocity;
    protected Vector2 input;

    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction jumpAction;
    public InputAction crouchAction;
    public InputAction sprintAction;
    public InputAction sprintReleaseAction;
    public InputAction attackAction;
    public InputAction shootAction;
    public InputAction comboAttackAction;

    public InputAction dashAction;


    public State(Character _character, StateMachine _stateMachine)
	{
        character = _character;
        stateMachine = _stateMachine;

        moveAction = character.playerInput.actions["Move"];
        lookAction = character.playerInput.actions["Look"];
        jumpAction = character.playerInput.actions["Jump"];
        crouchAction = character.playerInput.actions["Crouch"];
        sprintAction = character.playerInput.actions["Sprint"];
        sprintReleaseAction = character.playerInput.actions["SprintRelease"];
        dashAction = character.playerInput.actions["Dash"];
        attackAction = character.playerInput.actions["Attack"];
        shootAction = character.playerInput.actions["shoot"];
        comboAttackAction = character.playerInput.actions["ComboAttack"];
    }

    public virtual void Enter()
    {
        //StateUI.instance.SetStateText(this.ToString());
		Debug.Log("Enter State: "+this.ToString());
    }

    public virtual void HandleInput()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Exit()
    {
    }
}

public class EnemyState
{
    public Enemy character;
    public EnemyStateMachine stateMachine;

    protected Vector3 gravityVelocity;
    protected Vector3 velocity;

    public EnemyState(Enemy _character, EnemyStateMachine _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {
        //StateUI.instance.SetStateText(this.ToString());
        Debug.Log("Enter State: Enemy - " + this.ToString());
    }

    public virtual void HandleInput()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Exit()
    {
    }
}