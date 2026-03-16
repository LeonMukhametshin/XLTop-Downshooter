using Players;

namespace Assets._Scripts.Infranstructure.States
{
    public class GameplayEntryState : IState
    {
        private PlayerController m_playerController;

        private readonly StateMachine m_fsm;
        private readonly SpawnerEnemy m_spawnerEnemy;
        private readonly TargetMarkerObserver m_targetMarkerObserver;
        private readonly AIMLineMarker m_aimLineMarker;

        public GameplayEntryState(
            StateMachine stateMachine,
            SpawnerEnemy spawnerEnemy,
            AIMLineMarker aimLineMarker,
            TargetMarkerObserver targetMarkerObserver)
        {
            m_fsm = stateMachine;
            m_spawnerEnemy = spawnerEnemy;
            m_targetMarkerObserver = targetMarkerObserver;
            m_aimLineMarker = aimLineMarker;
        }

        public void Enter()
        {
            var playerPosition = ServiceLocator.Resolved<PlayerSpawnpoint>();
            ServiceLocator.Resolved<IPlayerFactorySettings>().position = playerPosition.transform.position;
            m_playerController = ServiceLocator.Resolved<IPlayerFactory>().Create();
           
            m_targetMarkerObserver.Initialize(m_playerController.GetComponent<PlayerMovement>());
            m_aimLineMarker.Initialize(m_playerController.transform);

            m_spawnerEnemy.Spawn();
            m_fsm.ChangeState<GameplayState>();
        }

        public void Exit() { }
    }
}
