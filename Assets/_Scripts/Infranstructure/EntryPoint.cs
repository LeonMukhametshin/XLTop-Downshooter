using Assets._Scripts.UI;
using Cameras;
using UnityEngine;

namespace Assets._Scripts.Infranstructure.States
{
    class EntryPoint : MonoBehaviour
    {
        [SerializeField] private TargetMarkerObserver m_targetMarkerObserver;
        [SerializeField] private BoothrapState m_boothrapState;
        [SerializeField] private DeadMenuView m_deadMenuView;
        [SerializeField] private SpawnerEnemy m_enemySpawner;
        [SerializeField] private AIMLineMarker m_aIMLineMarker;
        [SerializeField] private CameraFollow m_cameraFollow;
        [SerializeField] private PauseMenuView m_pauseMenuView;

        private StateMachine m_fsm = new StateMachine();

        private void Awake()
        {
            m_boothrapState.Initialize(m_fsm);

            m_fsm.Initialize(
                m_boothrapState,
                new PauseMenuState(m_fsm, m_pauseMenuView),
                new DeadState(m_fsm, m_deadMenuView),
                new GameplayState(m_fsm, m_cameraFollow),
                new GameplayExitState(m_enemySpawner),
                new GameplayEntryState(
                    m_fsm, 
                    m_enemySpawner, 
                    m_aIMLineMarker,
                    m_targetMarkerObserver));

            m_fsm.ChangeState<BoothrapState>();            
        }

        private void Update()
        {
            m_fsm.Update();
        }
    }
}