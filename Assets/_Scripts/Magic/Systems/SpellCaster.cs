using System;
using UnityEngine;
using UnityEngine.Pool;

public sealed class SpellCaster
{
    private readonly Transform m_casterTransform;
    private ObjectPool<GameObject> m_visualEffectPool;

    private readonly bool m_isSingelSpell;

    public SpellCaster(Transform casterTransformer, bool isSingelSpell = false)
    {
        m_casterTransform = casterTransformer;
        m_isSingelSpell = isSingelSpell;
    }

    public void Cast(BaceSpellData spell, Vector3 worldPosition)
    {
        if(!spell)
        {
            return;
        }

        switch(spell)
        {
            case SelfSpellData selfSpell: CastSelf(selfSpell); break;
            case TargetSpellData selfSpell: CastTarget(selfSpell, worldPosition); break;
            case NonTargetSpellData selfSpell: CastNonTarget(selfSpell); break;
            case AoeSpellData selfSpell:
                {
                    CastAoe(selfSpell, selfSpell.isTarget
                        ? worldPosition
                        : m_casterTransform.position);
                }
                break;
        }
    }

    private void CastSelf(SelfSpellData spell) 
    {
        if (spell.visualEffect)
        {
            var visualEffect = UnityEngine.Object.Instantiate(spell.visualEffect, m_casterTransform.position, Quaternion.identity);
            SetLayer(visualEffect);
        }

        var effectable = m_casterTransform.GetComponent<IEffectable>();
        spell.effects.ApplyEffect(effectable);
    }

    private void CastTarget(TargetSpellData spell, Vector3 worldPosition)
    {
        if (!spell.visualEffect)
        {
            throw new NullReferenceException("Target spell must have visualEffect");
        }

        var projectile = UnityEngine.Object.Instantiate(spell.visualEffect, m_casterTransform.position, Quaternion.identity);
        SetLayer(projectile);

        var spellProjectile =
            projectile.GetComponent<ISpellProjectile>() ??
            projectile.AddComponent<SpellProjectile>();

        spellProjectile.Initialize(worldPosition, spell.speed, spell.effects);
    }

    private void CastNonTarget(NonTargetSpellData selfSpell) { }

    private void CastAoe(AoeSpellData spell, Vector3 worldPosition) 
    {
        GameObject aoe = null;

        if(m_isSingelSpell)
        {
            m_visualEffectPool ??= new ObjectPool<GameObject>(
                () => Create(),
                gm => gm.SetActive(true),
                gm => gm.SetActive(false),
                UnityEngine.Object.Destroy);
        }
        else
        {
            aoe = Create();
        }

        SetLayer(aoe);
        aoe.transform.position = worldPosition;

        var spellAoe =
            aoe.GetComponent<ISpellAoe>() ??
            aoe.AddComponent<SpellAoe>();

        spellAoe.Initialize(worldPosition, spell.radius, spell.effects);

        GameObject Create() =>
            UnityEngine.Object.Instantiate(spell.visualEffect, m_casterTransform.position, Quaternion.identity);
    }

    private void SetLayer(GameObject visualEffect) =>
        visualEffect.layer = m_casterTransform.gameObject.layer;
}