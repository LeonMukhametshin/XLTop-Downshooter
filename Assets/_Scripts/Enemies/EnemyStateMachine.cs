using System;

public class EnemyStateMachine 
{
    public EnemyState CurrentState {  get; private set; }
    public event Action<EnemyState, EnemyState> StateChanged;

    public EnemyStateMachine()
    {
        CurrentState = EnemyState.Idle;
    }

    public void ChangedState(EnemyState nextState)
    {
        if(CurrentState == nextState || CurrentState is EnemyState.Dead)
        {
            return;
        }

        var previousState = CurrentState;
        CurrentState = nextState;

        StateChanged?.Invoke(previousState, nextState);
    }
}