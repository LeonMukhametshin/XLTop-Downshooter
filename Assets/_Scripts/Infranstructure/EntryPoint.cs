using UnityEngine;
using Assets._Scripts.UI;
using Assets._Scripts.Infranstructure.States;
using Cameras;

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

        private void Awake()
        {
            var fsm = new StateMachine();
            m_boothrapState.Initialize(fsm);

            fsm.Initialize(
                m_boothrapState,
                new PauseMenuState(fsm),
                new DeadState(fsm, m_deadMenuView),
                new GameplayState(m_aIMLineMarker, 
                    m_cameraFollow, 
                    m_targetMarkerObserver, 
                    fsm, 
                    m_enemySpawner));

            fsm.ChangeState<BoothrapState>();            
        }
    }
}