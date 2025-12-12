using System.Linq;
using UnityEngine;

public abstract class BaceSpellData : ScriptableObject
{
    [SerializeField] private string m_spellName;
    [SerializeField] private GameObject m_visualEffect;
    [SerializeField] private ElementType[] m_combination;

    //Effects[]
    [SerializeReferenceDropdown] private IEffect[] m_effects;

    public string SpellName => m_spellName;
    public GameObject VisualEffect => m_visualEffect;
    public ElementType[] combination => m_combination;

    private void OnValidate()
    {
        if(m_combination.Length > 3)
        {
            m_combination = m_combination.Take(3).ToArray();
        }
    }
}