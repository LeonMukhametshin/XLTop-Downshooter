using Assets._Scripts.Infranstructure.States;
using UnityEngine;

public class PauseMenuState : IState
{
    private StateMachine m_stateMachine;
    private PauseMenuView m_pauseMenu;
    private Loading m_loading;

    public PauseMenuState(
        StateMachine stateMachine,
        PauseMenuView pauseMenuView)
    {
        m_stateMachine = stateMachine;
        m_pauseMenu = pauseMenuView;
        m_loading =ServiceLocator.Resolved<Loading>();
    }

    public void Enter()
    {
        Time.timeScale = 0f;
        m_pauseMenu.gameObject.SetActive(false);
        m_pauseMenu.continueClicked += OnContinueCkicked;
        m_pauseMenu.mainMenuClicked += OnMainMenuClicked;
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        m_pauseMenu.gameObject.SetActive(true);
    }

    private void OnContinueCkicked() => 
        m_stateMachine.ChangeState<MainMenuState>();

    private void OnMainMenuClicked()
    {
        Exit();
        m_loading.LoadScene(GlobalConstants.Scenes.Main);
    }
}