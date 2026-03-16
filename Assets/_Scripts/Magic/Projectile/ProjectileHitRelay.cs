using UnityEngine;

public sealed class ProjectileHitRelay : MonoBehaviour
{
    private SpellProjectile m_owner;

    public void Bind(SpellProjectile owner)
    {
        m_owner = owner;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_owner)
        {
            m_owner.HandleHit(other);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (m_owner)
        {
            m_owner.HandleHit(collision.collider);
        }
    }
}
