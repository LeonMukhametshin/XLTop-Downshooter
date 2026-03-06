using Assets._Scripts.Infranstructure.States;
using Assets._Scripts.UI;

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
