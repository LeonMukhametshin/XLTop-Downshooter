using Assets._Scripts.Infranstructure.States;
using Assets._Scripts.UI;
using UnityEngine;

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
