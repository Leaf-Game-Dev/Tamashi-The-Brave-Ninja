using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AttackState : State
{

    int attackIndex = 0;
    public AttackState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

     public override void Enter()
    {
        base.Enter();

        attackIndex ++;
        attackIndex = attackIndex % 3;
        character.animator.SetTrigger("Attack");

        character.animator.SetInteger("AttackIndex",attackIndex);
        //await Task.Delay((int)(character.AttackTimings * 1000));
        //character.animator.SetTrigger("move");
        //stateMachine.ChangeState(character.standing);
        character.StartCoroutine(EndState());
    }

    IEnumerator EndState()
    {
        yield return new WaitForSeconds(character.AttackTimings);
        character.animator.SetTrigger("move");
        stateMachine.ChangeState(character.standing);

    }

}


public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy _character, EnemyStateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        character.animator.SetTrigger("Attack");
        character.StartCoroutine(EndState());
    }

    IEnumerator EndState()
    {
        yield return new WaitForSeconds(character.AttackTimings);
        character.animator.SetFloat("Speed", 0);
        character.animator.SetTrigger("Move");
        yield return new WaitForSeconds(character.AttackWaitTimings);
        stateMachine.ChangeState(character.standing);

    }
}