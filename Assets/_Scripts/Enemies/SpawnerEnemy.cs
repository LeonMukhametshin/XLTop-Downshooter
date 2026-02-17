using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private Enemy[] m_enemies;
    [SerializeField] private EnemyData[] m_datas;

    [SerializeField] private Transform[] m_spawnPoints;
    [SerializeField] private Transform m_playerTransfrom;

    //TODO Remove
    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        foreach(var point in m_spawnPoints)
        {
            var enemy = GetEnemy();
            var enemyData = GetEnemyData();

            var enemyInstance = Instantiate(enemy, point);
            enemyInstance.Initialize(enemyData, m_playerTransfrom);

            enemy.Died += OnDied;
        }
    }

    private void OnDied(Enemy enemy)
    {
        enemy.Died -= OnDied;
        Destroy(enemy.gameObject);
    }

    private Enemy GetEnemy() =>
        m_enemies[Random.Range(0, m_enemies.Length)];

    private EnemyData GetEnemyData() =>
        m_datas[Random.Range(0, m_datas.Length)];
}