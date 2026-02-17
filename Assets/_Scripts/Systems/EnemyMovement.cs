using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour, IAcceleration
{
    [SerializeField] private NavMeshAgent m_agent;

    private Transform m_target;
    private float m_speed;
    private float m_acceleration;
    private bool m_isMoving;
    private bool m_isInitilized;

    private void OnValidate()
    {
        if(!m_agent)
        {
            m_agent = GetComponent<NavMeshAgent>();
        }
    }

    public void Initialize(float speed, Transform target)
    {
        m_speed = speed;
        m_agent.speed = speed;
        m_target = target;
        m_isInitilized = true;
    }

    public void IncreaseAcceleration(float delta)
    {
        if (delta < 0)
        {
            throw new ArgumentException("Delta can`t be negative", nameof(delta));
        }

        m_acceleration += delta;
        SetSpeed();
    }

    public void DecreaseAcceleration(float delta)
    {
        if (delta < 0)
        {
            throw new ArgumentException("Delta can`t be negative", nameof(delta));
        }

        m_acceleration -= delta;
        SetSpeed();
    }

    private void Update()
    {
        if(!m_isInitilized || !m_isMoving || !m_target)
        {
            return;
        }

        m_agent.SetDestination(m_target.position);
    }

    public void StartMoving()
    {
        if(!m_isInitilized)
        {
            return;
        }

        m_isMoving = true;
        m_agent.isStopped = false;
    }

    public void StopMoving()
    {
        if (!m_isInitilized)
        {
            return;
        }

        m_isMoving = false;
        m_agent.isStopped = true;
        m_agent.velocity = Vector3.zero;
    }

    private void SetSpeed()
    {
        var acceleration = m_acceleration > 0
            ? m_acceleration
            : 1;

        m_agent.speed = m_speed * acceleration;
    }
}