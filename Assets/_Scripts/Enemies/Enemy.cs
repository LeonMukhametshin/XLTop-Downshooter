using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> Died;

    [SerializeField] private HealthComponent m_healthComponent;
    [SerializeField] private AttackEnemySystem m_attackEnemySystem;
    [SerializeField] private EnemyMovement m_movement;

    private EnemyData m_data;
    private Transform m_playerTransfrom;
    private EnemyStateMachine m_stateMachine;

    private void Awake()
    {
        m_stateMachine = new EnemyStateMachine();
    }

    private void OnEnable()
    {
        m_healthComponent.died += OnDied;
        m_stateMachine.StateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        m_healthComponent.died -= OnDied;
        m_stateMachine.StateChanged -= OnStateChanged;
    }

    private void Update()
    {
        if(m_stateMachine.CurrentState is EnemyState.Dead || !m_data)
        {
            return;
        }

        UpdateState();
    }

    public void Initialize(EnemyData data, Transform playerTransfrom)
    {
        m_data = data;
        m_playerTransfrom = playerTransfrom;
        m_healthComponent.Initialize(data.health);
        m_attackEnemySystem.Initialize(data.defaultSpell, data.spell, playerTransfrom, data.attackTime);
        m_movement.Initialize(data.speed, playerTransfrom);

        m_stateMachine ??= new EnemyStateMachine();

        if(m_data.enemyType == AttackEnemyType.Melee)
        {
            m_stateMachine.ChangedState(EnemyState.Move);
        }
    }

    private void UpdateState()
    {
        var isInAttackRange = IsInRanged();

        switch(m_stateMachine.CurrentState)
        {
            case EnemyState.Idle: HeandleIdleState(isInAttackRange); break;
            case EnemyState.Move: HeandleMoveState(isInAttackRange); break;
            case EnemyState.Attack: HeandleAttackState(isInAttackRange); break;
        }
    }

    private void HeandleAttackState(bool isInAttackRange)
    {
        m_attackEnemySystem.TryAttack();

        if(!isInAttackRange)
        {
            if(m_data.enemyType == AttackEnemyType.Melee)
            {
                m_stateMachine.ChangedState(EnemyState.Move);
            }
            else
            {
                m_stateMachine.ChangedState(EnemyState.Idle);
            }
        }
    }

    private void HeandleMoveState(bool isInAttackRange)
    {
        if(isInAttackRange)
        {
            m_stateMachine.ChangedState(EnemyState.Attack);
        }
    }  

    private void HeandleIdleState(bool isInAttackRange)
    {
        if(m_data.enemyType == AttackEnemyType.Range && isInAttackRange)
        {
            m_stateMachine.ChangedState(EnemyState.Attack);
        }
    }

    private bool IsInRanged()
    {
        if(!m_playerTransfrom)
        {
            return false;
        }

        var distance = Vector3.Distance(transform.position, m_playerTransfrom.position);
        return distance < m_data.attackRange;
    }

    private void OnDied() =>
        Died?.Invoke(this);

    private void OnStateChanged(EnemyState previousState, EnemyState nextState)
    {
        if(previousState is EnemyState.Move)
        {
            m_movement.StopMoving();
        }

        if(nextState is EnemyState.Move)
        {
            m_movement.StartMoving();
        }
    }
}