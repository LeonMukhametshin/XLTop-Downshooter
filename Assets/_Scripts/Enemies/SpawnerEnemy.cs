using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private Enemy[] m_enemies;
    [SerializeField] private EnemyData[] m_datas;

    [SerializeField] private Transform[] m_spawnPoints;

    private List<Enemy> m_currentEnemies = new();

    public void Spawn()
    {
        var playerTransfrom = ServiceLocator
            .Resolved<IPlayerFactory>()
            .Create()
            .transform;

        foreach(var point in m_spawnPoints)
        {
            var enemy = GetEnemy();
            var enemyData = GetEnemyData();

            var enemyInstance = Instantiate(enemy, point);
            enemyInstance.Initialize(enemyData, playerTransfrom);

            enemy.Died += OnDied;

            m_currentEnemies.Add(enemy);
        }
    }

    public void DespawnEnemyAll()
    {
        foreach(var enemy in m_currentEnemies)
        {
            DestroyEnemy(enemy);
        }

        m_currentEnemies.Clear();
    }

    private void OnDied(Enemy enemy)
    {
        m_currentEnemies.Remove(enemy);
        DestroyEnemy(enemy);
    }

    private Enemy GetEnemy() =>
        m_enemies[Random.Range(0, m_enemies.Length)];

    private EnemyData GetEnemyData() =>
        m_datas[Random.Range(0, m_datas.Length)];

    private void DestroyEnemy(Enemy enemy)
    {
        enemy.Died -= OnDied;
        Destroy(enemy.gameObject);
    }
}