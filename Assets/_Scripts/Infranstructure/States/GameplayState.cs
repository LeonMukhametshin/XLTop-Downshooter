using Assets._Scripts.Infranstructure.States;
using Cameras;
using Players;
using UnityEngine;

public class GameplayState : IState
{
    private readonly StateMachine m_stateMachine;
    private readonly CameraFollow m_cameraFollower;

    private PlayerController m_playerController;

    public GameplayState(
        CameraFollow cameraFollow,
        StateMachine stateMachine)
    {
        m_stateMachine = stateMachine;
        m_cameraFollower = cameraFollow;
    }

    public void Enter()
    {
        m_playerController = ServiceLocator
            .Resolved<IPlayerFactory>()
            .Create();

        m_cameraFollower.SetTarget(m_playerController.transform);
        m_playerController.healh.died += OnDied;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            m_stateMachine.ChangeState<PauseMenuState>();
        }
    }

    public void Exit()
    {
        m_playerController.healh.died -= OnDied;
        m_playerController = null;
    }

    private void OnDied()
    {
        m_stateMachine.ChangeState<DeadState>();
    }
}