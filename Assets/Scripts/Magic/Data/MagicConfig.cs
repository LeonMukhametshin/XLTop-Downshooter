using UnityEngine;

[CreateAssetMenu(fileName = "MagicConfig", menuName = "Scriptable Objects/MagicConfig")]
public class MagicConfig : ScriptableObject
{
    [SerializeField] private ElementsData m_elementsData;
    [SerializeField] private SpellDataBase m_spellsDataBase;

    [SerializeField] [Min(1)] private int m_maxelements = 3;
    [SerializeField] [Min(0)] private float m_cancelCooldown;

    public ElementsData elementData => m_elementsData;
    public SpellDataBase spellDataBase => m_spellsDataBase;
    public int maxElements => m_maxelements;
    public float cancelCooldown => m_cancelCooldown;
}