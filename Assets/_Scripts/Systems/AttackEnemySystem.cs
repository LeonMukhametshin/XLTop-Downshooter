using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public sealed class AttackEnemySystem : MonoBehaviour
{
    private Transform m_target;
    private IReadOnlyList<SpeelEnemyData> m_spells;
    private SpellCaster m_spellCaster;

    private float m_attackTime;
    private float m_cooldownTimer;

    private bool m_isInitialized;

    private int m_maxCount;
    private int m_count;

    private BaceSpellData m_baceSpellData;

    public void Initialize(
        BaceSpellData defaultSpell,
        IReadOnlyList<SpeelEnemyData> spell, 
        Transform target, 
        float attackTime)
    {
        if (m_isInitialized)
        {
            return;
        }

        m_baceSpellData = defaultSpell; 

        m_target = target;
        m_attackTime = attackTime;
        m_spellCaster = new(transform);
        m_spells = spell.OrderBy(s => s.count).ToArray();
        m_maxCount = m_spells.LastOrDefault().count;

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

        m_count++;
        var spell = m_spells.FirstOrDefault(spell => spell.count == m_count);

        if (spell.data is null)
        {
            m_spellCaster.Cast(m_baceSpellData, m_target.position);
        }
        else
        {
            m_spellCaster.Cast(spell.data, m_target.position);
        }

        if(m_count == m_maxCount)
        {
            m_count = 0;
        }

        m_cooldownTimer = m_attackTime;
        return true;
    }
}