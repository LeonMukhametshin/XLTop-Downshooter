using UnityEngine;

public class SpellCuster
{
    private Transform m_casterTransformer;

    public SpellCuster(Transform casterTransformer)
    {
        m_casterTransformer = casterTransformer;
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
                        : m_casterTransformer.position);
                }
                break;
        }
    }

    private void CastSelf(SelfSpellData spell) { }

    private void CastTarget(TargetSpellData spell, Vector3 worldPosition) { }

    private void CastNonTarget(NonTargetSpellData selfSpell) { }

    private void CastAoe(AoeSpellData selfSpell, Vector3 worldPosition) { }
}