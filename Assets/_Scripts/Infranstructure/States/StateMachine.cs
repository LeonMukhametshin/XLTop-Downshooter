using Assets._Scripts.Infranstructure.States;
using System.Collections.Generic;
using Assets._Scripts.UI;
using System;
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

    public DeadState(StateMachine stateMachine)
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

public class GameplayState : IState
{
    private StateMachine m_stateMachine;
    private SpawnerEnemy m_spawnerEnemy;

    public GameplayState(StateMachine stateMachine,
        SpawnerEnemy enemy)
    {
        m_stateMachine = stateMachine;
        m_spawnerEnemy = enemy;
    }

    public void Enter()
    {
        m_spawnerEnemy.Spawn();
    }

    public void Exit()
    {

    }
}
