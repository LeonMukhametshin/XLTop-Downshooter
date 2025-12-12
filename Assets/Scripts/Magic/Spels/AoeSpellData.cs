using UnityEngine;

[CreateAssetMenu(fileName = "SelfSpellData", menuName = "ScriptableObject/Spels/AoeSpellData")]
public class AoeSpellData : BaceSpellData
{
    [SerializeField] [Min(0)] private float m_radiuse;
    public float Radiuse => m_radiuse;
}