using UnityEngine;

public sealed class AttackEnemySystem : MonoBehaviour
{
    private Transform m_target;
    private BaceSpellData m_spellData;
    private SpellCaster m_spellCaster;

    private float m_attackTime;
    private float m_cooldownTimer;

    private bool m_isInitialized;

    public void Initialize(BaceSpellData spell, Transform target, float attackTime)
    {
        if (m_isInitialized)
        {
            return;
        }

        m_spellData = spell;
        m_target = target;
        m_attackTime = attackTime;
        m_spellCaster = new(transform);

        m_isInitialized = true;
    }

    private void Update()
    {
        if(!m_isInitialized)
        {
            return;
        }
       
        if (m_cooldownTimer > 0)
        {
            m_cooldownTimer -= Time.deltaTime;
        }
    }

    public bool TryAttack()
    {
        if(!m_isInitialized || !m_target)
        {
            return false;
        }

        if(m_cooldownTimer > 0)
        {
            return false;
        }

        m_spellCaster.Cast(m_spellData, m_target.position);
        m_cooldownTimer = m_attackTime;

        return true;
    }
}