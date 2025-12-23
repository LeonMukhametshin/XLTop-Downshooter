using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> died;

    [SerializeField] private HealthComponent m_healthComponent;
    [SerializeField] private EnemyData m_enemyData;

    private EnemyData m_data;

    public HealthComponent Health => m_healthComponent;

    //TODO add HealthComponent
    //TODO add Movement 
    //TODO add AttackComponent

    private void OnEnable()
    {
        m_healthComponent.valueChanged += () =>
        {
            Debug.Log($"Health changed: {m_healthComponent.value}");
        };

        m_healthComponent.died += OnDied;
    }

    private void OnDisable()
    {
        m_healthComponent.died -= OnDied;
    }

    private void Awake()
    {
        Initialize(m_enemyData);
    }

    public void Initialize(EnemyData data)
    {
        m_data = data;
        m_healthComponent.Initialize(data.health);
    }

    private void OnDied() =>
        died?.Invoke(this);
}