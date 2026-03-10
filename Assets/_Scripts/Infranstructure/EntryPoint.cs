using Assets._Scripts.Infranstructure.States;
using Assets._Scripts.UI;
using Cameras;
using UnityEngine;
using StateMachine = Assets._Scripts.Infranstructure.States.StateMachine;

namespace Assets._Scripts.Infranstructure
{
    class EntryPoint : MonoBehaviour
    {
        [SerializeField] private BoothrapState m_boothrapState;

        [SerializeField] private SpawnerEnemy m_enemySpawner;
        [SerializeField] private DeadMenuView m_deadMenuView;
        [SerializeField] private TargetMarkerObserver m_targetMarkerObserver;
        [SerializeField] private AIMLineMarker m_aIMLineMarker;
        [SerializeField] private CameraFollow m_cameraFollow;
        [SerializeField] private PauseMenuView m_pauseMenuView;

        private StateMachine fsm = new StateMachine();

        private void Awake()
        {
            m_boothrapState.Initialize(fsm);

            fsm.Initialize(
                m_boothrapState,
                new PauseMenuState(fsm, m_pauseMenuView),
                new DeadState(fsm, m_deadMenuView),
                new GameplayEntryState(fsm,
                    m_enemySpawner,
                    m_aIMLineMarker,
                    m_targetMarkerObserver,
                    m_cameraFollow, 
                    m_enemySpawner));

            fsm.ChangeState<BoothrapState>();            
        }

        private void Update()
        {
            fsm.Update();
        }
    }
}