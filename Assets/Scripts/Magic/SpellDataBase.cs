using System.Collections.Generic;
using UnityEngine;

public sealed class SpellDataBase : ScriptableObject
{
    [SerializeField] private BaceSpellData[] m_spellData;

    public IReadOnlyList<BaceSpellData> spells => m_spellData;
}
