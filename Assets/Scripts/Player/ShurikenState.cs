using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenState : State
{
    public ShurikenState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        character.animator.SetTrigger("shootShuriken");
        character.StartCoroutine(EndState());
    }



    IEnumerator EndState()
    {
        yield return new WaitForSeconds(character.AttackTimings);
        character.animator.SetTrigger("move");
        stateMachine.ChangeState(character.standing);

    }

}
