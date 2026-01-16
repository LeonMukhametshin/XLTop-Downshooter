using System;
using System.Collections.Generic;

public class SpellPreporation
{
    public event Action<IReadOnlyList<ElementType>> elementsChanged;
    public event Action overflowReccured;

    private MagicConfig m_magicConfig;
    private List<ElementType> m_elements = new();

    public SpellPreporation(MagicConfig magicConfig)
    {
        m_magicConfig = magicConfig;
    }

    public void AddElement(ElementType elementType)
    {
        if(m_elements.Count >= m_magicConfig.maxElements)
        {
            Clear();
            overflowReccured?.Invoke();
        }
        else
        {
            m_elements.Add(elementType);
            elementsChanged?.Invoke(m_elements);
        }
    }

    public bool TryGetSpell(out BaceSpellData spell)
    {
        spell = null;

        if(m_elements.Count is 0)
        {
            return false;
        }

        foreach(var spellData in m_magicConfig.spellDataBase.spells)
        {
            if (IsMatchingCombination(spellData.combination))
            {
                spell = spellData;
                return true;
            }
        }

        return false;
    }

    private bool IsMatchingCombination(IReadOnlyList<ElementType> combination)
    {
        if(combination.Count != m_elements.Count)
        {
            return false;
        }

        for(var i = 0; i < combination.Count; i++)
        {
            if (combination[i] != m_elements[i])
            {
                return false;
            }
        }

        return true;
    }

    public void Clear()
    {
        m_elements.Clear();
        elementsChanged?.Invoke(m_elements);
    }
}