using System.Collections.Generic;
using UnityEngine;

public class BuffContainer : MonoBehaviour, IEffectable
{
    private HashSet<string> m_ids = new();
    private Dictionary<string, IBuff> m_buffs = new();

    public void Update()
    {
        foreach(var buff in m_buffs.Values)
        {
            buff.Update(Time.deltaTime);
            //m_ids.Remove(m_buffs.Values);
        }

        foreach(var id in m_ids)
        {
            m_buffs.Remove(id);
        }

        m_ids.Clear();
    }

    public void Add(IBuff buff)
    {
        if(m_buffs.TryGetValue(buff.Id, out IBuff existingBuff))
        {
            existingBuff.Refresh(this);
        }
        else
        {
            m_buffs[buff.Id] = buff;
            buff.Intitialize(this);
        }
    }

    public void Remove(IBuff buff)
    {
        m_ids.Add(buff.Id);
    }
}