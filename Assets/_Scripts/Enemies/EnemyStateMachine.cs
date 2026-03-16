using System;

public class EnemyStateMachine 
{
    public EnemyState currentState {  get; private set; }
    public event Action<EnemyState, EnemyState> stateChanged;

    public EnemyStateMachine()
    {
        currentState = EnemyState.Idle;
    }

    public void ChangedState(EnemyState nextState)
    {
        if(currentState == nextState || currentState is EnemyState.Dead)
        {
            return;
        }

        var previousState = currentState;
        currentState = nextState;

        stateChanged?.Invoke(previousState, nextState);
    }
}