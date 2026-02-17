using System;
using UnityEngine;
using System.Collections.Generic;

public class BuffContainer : MonoBehaviour, IEffectable
{
    public event Action<IBuff> buffAdded;
    public event Action<IBuff> buffRemoved;

    private HashSet<string> m_ids = new();
    private Dictionary<string, IBuff> m_buffs = new();

    public IReadOnlyCollection<IBuff> buffs => m_buffs.Values;

    public void Update()
    {
        foreach(var buff in m_buffs.Values)
        {
            buff.Update(Time.deltaTime);
            //m_ids.Remove(m_buffs.Values);
        }

        foreach(var id in m_ids)
        {
            var buff = m_buffs[id]; 
            m_buffs.Remove(id);

            buffRemoved?.Invoke(buff);
        }

        m_ids.Clear();
    }

    public void Add(IBuff buff)
    {
        if(m_buffs.TryGetValue(buff.id, out IBuff existingBuff))
        {
            existingBuff.Refresh(this);
        }
        else
        {
            m_buffs[buff.id] = buff;
            buff.Intitialize(this);

            buffAdded?.Invoke(buff);
        }
    }

    public void Remove(IBuff buff)
    {
        m_ids.Add(buff.id);
    }
}