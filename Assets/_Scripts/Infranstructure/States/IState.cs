using Assets._Scripts.Infranstructure.States;

public interface IState 
{
    public void Enter() { }
    public void Update() { }
    public void Exit() { }
}