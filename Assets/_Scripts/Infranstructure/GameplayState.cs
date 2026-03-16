using Assets._Scripts.Infranstructure.States;
using Cameras;
using Players;
using UnityEngine.InputSystem;

public class GameplayState : IState
{
    private PlayerController m_playerController;

    private readonly StateMachine m_stateMachine;
    private readonly CameraFollow m_cameraFollow;

    public GameplayState(
        StateMachine stateMachine,
        CameraFollow cameraFollow)
    {
        m_cameraFollow = cameraFollow;
        m_stateMachine = stateMachine;
    }

    public void Enter()
    {
        m_playerController = ServiceLocator.Resolved<IPlayerFactory>().Create();

        m_cameraFollow.SetTarget(m_playerController.transform);
        m_playerController.healh.died += OnDied;
    }

    public void Update()
    {
        if (Keyboard.current[Key.Escape].wasPressedThisFrame)
        {
            m_stateMachine.ChangeState<PauseMenuState>();
        }
    }

    public void Exit()
    {
        m_playerController.healh.died -= OnDied;
        m_playerController = null;
    }

    private void OnDied() =>
        m_stateMachine.ChangeState<DeadState>();
}