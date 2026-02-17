using System.Collections.Generic;
using UnityEngine;

public sealed class BuffElementsContainerView : MonoBehaviour
{
    [SerializeField] private BuffElementView m_buffView;
    [SerializeField] private BuffElementView m_deBuffView;
    [SerializeField] private BuffContainer m_buffContainer;

    private Dictionary<string, BuffElementView> m_elements;

    private void OnEnable()
    {
        foreach(var buff in m_buffContainer.buffs)
        {
            AddElemet(buff);
        }

        m_buffContainer.buffAdded += AddElemet;
        m_buffContainer.buffRemoved += RemoveElement;
    }

    private void OnDisable()
    {
        foreach (var buff in m_buffContainer.buffs)
        {
            RemoveElement(buff);
        }

        m_buffContainer.buffAdded -= AddElemet;
        m_buffContainer.buffRemoved -= RemoveElement;
    }

    private void AddElemet(IBuff buff)
    {
        var element = buff.type is BuffType.Buff
            ? Instantiate(m_buffView, transform)
            : Instantiate(m_deBuffView, transform);

        element.Initialize(buff);

        m_elements.Add(buff.id, element);
    }

    private void RemoveElement(IBuff buff)
    {
        var element = m_elements[buff.id];
        element.DeInitilize();
        Destroy(element);

        m_elements.Remove(buff.id);
    }
}   