using UnityEngine;

[CreateAssetMenu(fileName = "TargetSpellData", menuName = "ScriptableObject/Spels/Target Spell")]
public class TargetSpellData : BaceSpellData
{
    [SerializeField] [Min(0)] private float m_speed;
    public float speed => m_speed;
}
