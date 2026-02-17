using UnityEngine;

[CreateAssetMenu(fileName = "TargetSpellData", menuName = "ScriptableObject/Spels/NonTarget Spell")]
public class NonTargetSpellData : BaceSpellData
{
    [SerializeField][Min(0)] private float m_range;
    [SerializeField][Min(0)] private float m_duration;
    [SerializeField][Min(0)] private float m_effectInternal;

    public float Range => m_range;
    public float EffectInternal => m_effectInternal;
    public float Duration => m_duration;
}
