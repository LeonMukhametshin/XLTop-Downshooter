using UnityEngine;
using Assets._Scripts.Infranstructure.States;
using Assets._Scripts.UI;

namespace Assets._Scripts.Infranstructure
{
    class EntryPoint : MonoBehaviour
    {
        [SerializeField] private SpawnerEnemy m_enemySpawner;
        [SerializeField] private MainMenuView m_mainMenuView;

        private void Awake()
        {
            var fsm = new StateMachine();

            fsm.Initialize(
                new MainMenuState(fsm, m_mainMenuView),
                new PauseMenuState(fsm),
                new DeadState(fsm),
                new GameplayState(fsm, m_enemySpawner));

            fsm.ChangeState<MainMenuState>();            
        }
    }
}