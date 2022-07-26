using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    int attackindex;
    public AttackState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        attackindex++;
        attackindex = attackindex % 3;
        Debug.Log(attackindex);
        character.animator.SetInteger("AttackIndex", attackindex);
        character.animator.SetTrigger("Attack");
        character.StartCoroutine(EndState());
    }



    IEnumerator EndState()
    {
        yield return new WaitForSeconds(character.AttackTimings);
        character.animator.SetTrigger("move");
        stateMachine.ChangeState(character.standing);

    }

}
