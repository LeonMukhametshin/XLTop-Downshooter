public class GameplayExitState : IState
{
    private readonly SpawnerEnemy m_spawnerEnemy;

    public GameplayExitState(SpawnerEnemy spawnerEnemy)
    {
        m_spawnerEnemy = spawnerEnemy;
    }

    public void Enter()
    {
        var loading = ServiceLocator.Resolved<Loading>();

        m_spawnerEnemy.DespawnEnemyAll();

        loading.LoadScene(GlobalConstants.Scenes.Main);
    }

    public void Exit() { }
}
