using UnityEngine;

[CreateAssetMenu(fileName = "SelfSpellData", menuName = "ScriptableObject/Spels/AoeSpellData")]
public class AoeSpellData : BaceSpellData
{
    [SerializeField] private bool m_isTarget;
    [SerializeField] [Min(0)] private float m_radius;

    public bool isTarget => m_isTarget;
    public float radius => m_radius;
}