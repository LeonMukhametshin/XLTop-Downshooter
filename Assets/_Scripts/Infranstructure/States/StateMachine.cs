using Assets._Scripts.Infranstructure.States;
using Assets._Scripts.UI;
using Cameras;
using Players;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Infranstructure.States
{
    public class StateMachine
    {
        private IState m_state;

        private Dictionary<Type, IState> m_states = new();

        public void Initialize(params IState[] states)
        {
            if(m_states.Count > 0)
            {
                return;
            }

            foreach(var state in states)
            {
                m_states.Add(state.GetType(), state);
            }
        }

        public void ChangeState<T>() where T: IState
        {
            m_state?.Exit();
            m_state = m_states[typeof(T)];
            m_state.Enter();
        }
    }
}

public class MainMenuState : IState
{
    private StateMachine m_stateMachine;
    private MainMenuView m_mainMenuView;

    public MainMenuState(StateMachine stateMachine, MainMenuView mainMenuView)
    {
        m_stateMachine = stateMachine;
        m_mainMenuView = mainMenuView;

        m_mainMenuView.gameObject.SetActive(false);
    }

    public void Enter()
    {
        m_mainMenuView.gameObject.SetActive(true);
        m_mainMenuView.playClicked += OnPlayerClicked;
        m_mainMenuView.exitClicked += OnExitClicked;
    }

    public void Exit()
    {
        m_mainMenuView.gameObject.SetActive(false);
        m_mainMenuView.playClicked -= OnPlayerClicked;
    }
    private void OnExitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif

        Application.Quit();
    }

    private void OnPlayerClicked()
    {
        m_stateMachine.ChangeState<GameplayState>();
    }
}

public class PauseMenuState : IState
{
    private StateMachine m_stateMachine;

    public PauseMenuState(StateMachine stateMachine)
    {
        m_stateMachine = stateMachine;
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }
}

public class DeadState : IState
{
    private StateMachine m_stateMachine;
    private DeadMenuView m_deadMenuView;

    public DeadState(StateMachine stateMachine,
        DeadMenuView deadMenuView)
    {
        m_stateMachine = stateMachine;
        m_deadMenuView = deadMenuView;

        m_deadMenuView.gameObject.SetActive(false);
    }

    public void Enter()
    {
        m_deadMenuView.goToMenuClicked += OnGoToMenuClicked;
        m_deadMenuView.gameObject.SetActive(true);
    }

    public void Exit()
    {
        m_deadMenuView.goToMenuClicked -= OnGoToMenuClicked;
        m_deadMenuView.gameObject.SetActive(false);
    }

    private void OnGoToMenuClicked()
    {
        m_stateMachine.ChangeState<MainMenuState>();
    }
}

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

    public void Exit()
    {
        m_playerController.healh.died -= OnDied;
    }

    private void OnDied()
    {
        m_stateMachine.ChangeState<DeadState>();
    }
}