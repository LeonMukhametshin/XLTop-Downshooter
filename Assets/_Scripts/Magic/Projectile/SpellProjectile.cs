using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour, ISpellProjectile
{
    [SerializeField] private Rigidbody m_rigidbody;

    private Collider m_collider;

    private float m_speed;
    private float m_targetDistance;
    private float m_traveledDistance;

    private Vector3 m_direction;
    private Vector3 m_targetPosition;

    private IReadOnlyList<IEffect> m_effects;

    private bool m_initialized;

    private void OnValidate()
    {
        if (!m_rigidbody)
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }
    }

    private void Awake()
    {
        if (!m_rigidbody)
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        m_collider = GetComponent<Collider>();
        if (!m_collider)
        {
            m_collider = GetComponentInChildren<Collider>();
        }

        if (m_collider && m_collider.gameObject != gameObject)
        {
            var relay = m_collider.GetComponent<ProjectileHitRelay>();
            if (!relay)
            {
                relay = m_collider.gameObject.AddComponent<ProjectileHitRelay>();
            }
            relay.Bind(this);
        }

        m_rigidbody.useGravity = false;
        m_rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    private void FixedUpdate()
    {
        if (!m_initialized) return;

        m_traveledDistance += m_speed * Time.fixedDeltaTime;

        if (m_traveledDistance >= m_targetDistance)
        {
            Destroy(gameObject);
        }
        else
        {
            SetLinearVelocity();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleHit(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleHit(collision.collider);
    }

    public void Initialize(Vector3 targetPosition, float speed, IReadOnlyList<IEffect> effects)
    {
        m_targetPosition = targetPosition;
        m_targetPosition.y = transform.position.y;

        m_speed = speed;
        m_effects = effects;

        m_direction = (m_targetPosition - transform.position).normalized;

        m_traveledDistance = 0f;
        m_targetDistance = Vector3.Distance(transform.position, m_targetPosition);

        if (m_direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(m_direction);

        m_initialized = true;

        SetLinearVelocity();
    }

    private void SetLinearVelocity() =>
        m_rigidbody.linearVelocity = m_direction * m_speed;

    internal void HandleHit(Collider other)
    {
        if (!m_initialized || !other)
        {
            return;
        }

        if (other.gameObject.layer == gameObject.layer)
        {
            return;
        }

        var effectables = other.GetComponents<IEffectable>();
        if (effectables.Length == 0)
        {
            effectables = other.GetComponentsInParent<IEffectable>();
        }

        if (effectables.Length == 0)
        {
            effectables = other.GetComponentsInChildren<IEffectable>();
        }

        m_effects.ApplyEffect(effectables);
        Destroy(gameObject);
    }
}
