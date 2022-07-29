public class StateMachine
{
    public State currentState;

    public void Initialize(State startingState)
    {
        currentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(State newState)
    {
        currentState.Exit();

        currentState = newState;
        newState.Enter();
    }

    
}

public class EnemyStateMachine
{
    public EnemyState currentState;

    public void Initialize(EnemyState startingState)
    {
        currentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(EnemyState newState)
    {
        currentState.Exit();

        currentState = newState;
        newState.Enter();
    }


}
