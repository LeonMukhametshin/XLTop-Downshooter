using Assets._Scripts.Infranstructure.States;
using Cameras;
using Players;
using UnityEngine.InputSystem;

public class GameplayState : IState
{
    private readonly StateMachine m_stateMachine;
    private readonly SpawnerEnemy m_spawnerEnemy;
    private readonly TargetMarkerObserver m_targetMarkerObserver;
    private readonly AIMLineMarker m_aimLineMarker;
    private readonly CameraFollow m_cameraFollower;
    private PlayerController m_playerController;

    public GameplayState(
        AIMLineMarker aIMLineMarker,
        CameraFollow cameraFollow,
        TargetMarkerObserver targetMarkerObserver,
        StateMachine stateMachine,
        SpawnerEnemy enemy)
    {
        m_stateMachine = stateMachine;
        m_spawnerEnemy = enemy;
        m_targetMarkerObserver = targetMarkerObserver;
        m_aimLineMarker = aIMLineMarker;
        m_cameraFollower = cameraFollow;
    }

    public void Enter()
    {
        var playerPosition = ServiceLocator.Resolved<PlayerSpawnpoint>();
        ServiceLocator.Resolved<IPlayerFactorySettings>().position = playerPosition.transform.position;
        m_playerController = ServiceLocator.Resolved<PlayerFactory>().Create();

        m_targetMarkerObserver.Initialize(m_playerController.GetComponent<PlayerMovement>());
        m_aimLineMarker.Initialize(m_playerController.transform);
        m_cameraFollower.SetTarget(playerPosition.transform);

        m_spawnerEnemy.Spawn();
        m_playerController.healh.died += OnDied;
    }

    public void Update()
    {
        if(Keyboard.current(Key.Escape).wasPressedThisFrame)
        {
            m_stateMachine.ChangeState<PauseMenuState>();
        }
    }

    public void Exit()
    {
        m_playerController.healh.died -= OnDied;
    }

    private void OnDied()
    {
        m_stateMachine.ChangeState<DeadState>();
    }
}