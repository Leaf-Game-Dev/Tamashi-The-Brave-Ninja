using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    float dashTime;
    float dashSpeed;

    public DashState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        dashing = false;
        dashTime = character.DashTime;
        dashSpeed = character.DashSpeed;

        character.animator.SetFloat("speed", 0);
        character.animator.SetTrigger("dash");
    }

    bool dashing;

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!dashing)
        {
            character.StartCoroutine(Dash());
            dashing = true;
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;
        Debug.Log("Dashing");
        while (Time.time < startTime+dashTime)
        {
            character.controller.Move(character.transform.forward * dashSpeed * Time.deltaTime);
            yield return null;
        }
        character.animator.SetTrigger("move");

        stateMachine.ChangeState(character.standing);

    }

}
