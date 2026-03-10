using JetBrains.Annotations;
using Players;
using Unity.VisualScripting;

namespace Assets._Scripts.Infranstructure.States
{
    public class GameplayEntryState : IState
    {
        private readonly StateMachine m_stateMachine;
        private readonly SpawnerEnemy m_spawnerEnemy;
        private PlayerController m_playerController;
        private readonly TargetMarkerObserver m_targetMarkerObserver;
        private readonly AIMLineMarker m_aimLineMarker;

        public GameplayEntryState(StateMachine stateMachine, 
            SpawnerEnemy spawnerEnemy, 
            TargetMarkerObserver targetMarkerObserver, 
            AIMLineMarker aimLineMarker)
        {
            m_stateMachine = stateMachine;
            m_spawnerEnemy = spawnerEnemy;
            m_targetMarkerObserver = targetMarkerObserver;
            m_aimLineMarker = aimLineMarker;
        }

        public void Enter()
        {
            var playerPosition = ServiceLocator.Resolved<PlayerSpawnpoint>();
            ServiceLocator.Resolved<IPlayerFactorySettings>().position = playerPosition.transform.position;
            m_playerController = ServiceLocator.Resolved<PlayerFactory>().Create();
           
            m_targetMarkerObserver.Initialize(m_playerController.GetComponent<PlayerMovement>());
            m_aimLineMarker.Initialize(m_playerController.transform);

            m_spawnerEnemy.Spawn();
            m_stateMachine.ChangeState<GameplayState>();
        }

        public void Exit()
        {

        }
    }
}

public class GameplayerExitState : IState
{
    public void Enter()
    {
        var loading = ServiceLocator.Resolved<Loading>();
        
        var spawner = ServiceLocator.Resolved<SpawnerEnemy>();
        spawner.DespawnEnemyAll();

        loading.LoadScene(GlobalConstants.Scenes.Main);
    }

    public void Exit()
    {

    }
}
