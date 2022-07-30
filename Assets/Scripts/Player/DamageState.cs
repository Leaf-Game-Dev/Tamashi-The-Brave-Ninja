using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DamageState : State
{
    public DamageState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        /*
                character.animator.SetTrigger("Damage");

                //await Task.Delay((int)(character.Damagetime * 1000));

                character.animator.SetTrigger("move");
                stateMachine.ChangeState(character.standing);*/

        character.StartCoroutine(TakeDamage());

    }

    IEnumerator TakeDamage()
    {
        character.animator.SetTrigger("Damage");

        //await Task.Delay((int)(character.Damagetime * 1000));
        yield return new WaitForSeconds(character.Damagetime);
        character.animator.SetTrigger("move");
        stateMachine.ChangeState(character.standing);
    }

}

public class EnemyDamageState : EnemyState
{
    public EnemyDamageState(Enemy _character, EnemyStateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        /*
                character.animator.SetTrigger("Damage");

                //await Task.Delay((int)(character.Damagetime * 1000));

                character.animator.SetTrigger("move");
                stateMachine.ChangeState(character.standing);*/

        character.StartCoroutine(TakeDamage());

    }

    IEnumerator TakeDamage()
    {
        character.animator.SetTrigger("Damage");

        //await Task.Delay((int)(character.Damagetime * 1000));
        yield return new WaitForSeconds(character.Damagetime);
        character.animator.SetTrigger("Move");
        stateMachine.ChangeState(character.standing);
    }

}
