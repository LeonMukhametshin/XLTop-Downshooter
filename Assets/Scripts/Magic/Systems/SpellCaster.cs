using System;
using UnityEngine;

public sealed class SpellCaster
{
    private readonly Transform m_casterTransform;

    public SpellCaster(Transform casterTransformer)
    {
        m_casterTransform = casterTransformer;
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

        if (m_casterTransform.TryGetComponent<IEffectable>(out var effectable))
        {
            foreach (var effect in spell.effects)
            {
                effect.Apply(effectable);
            }
        }
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
        var aoe = spell.visualEffect
               ? UnityEngine.Object.Instantiate(spell.visualEffect, m_casterTransform.position, Quaternion.identity)
               : new GameObject();
        SetLayer(aoe);

        aoe.transform.position = worldPosition;

        var spellAoe =
            aoe.GetComponent<ISpellAoe>() ??
            aoe.AddComponent<SpellAoe>();

        spellAoe.Initialize(worldPosition, spell.radius, spell.effects);
    }

    private void SetLayer(GameObject visualEffect) =>
        visualEffect.layer = m_casterTransform.gameObject.layer;
}