using Assets._Scripts.Infranstructure.States;
using Players;
using UnityEngine;

public class BoothrapState : MonoBehaviour, IState
{
    [SerializeField] private PlayerSpawnpoint m_spawnPoint;
    [SerializeField] private MouseResolver m_mouseResolver;

    private StateMachine m_stateMachine;

    public void Initialize(StateMachine stateMachine)
    {
        m_stateMachine = stateMachine;
    }

    public void Enter()
    {
        var playerFactory = new PlayerFactory("Assets/Resources/Prefabs/Player");

        ServiceLocator.Register(m_spawnPoint);
        ServiceLocator.Register<IPlayerFactory>(playerFactory);
        ServiceLocator.Register<IPlayerFactorySettings>(playerFactory);
        
        ServiceLocator.Register<MouseResolver>(m_mouseResolver);
        m_stateMachine.ChangeState<GameplayState>();
    }

    public void Exit()
    {

    }
}