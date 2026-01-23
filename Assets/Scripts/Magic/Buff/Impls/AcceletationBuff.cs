using System;
using UnityEngine;

[Serializable]
public class AcceletationBuff : TimeBuff
{
    [SerializeField][Min(0)] private float m_value;

    private IAcceleration m_acceleration;

    public AcceletationBuff(
        string id, 
        float duration,
        float value) :
        base(id, duration)
    {
        m_value = value;
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();
        m_acceleration = container.GetComponent<IAcceleration>();

        if(m_acceleration is null)
        {
            Deinitialize();
        }
        else
        {
            m_acceleration.IncreaseAcceleration(m_value);
        }
    }

    protected override void OnDeinitializing()
    {
        m_acceleration = null;

        if(m_acceleration is not null)
        {
            m_acceleration.DecreaseAcceleration(m_value);
        }

        base.OnDeinitializing();
    }

    public override IBuff Clone() =>
        new AcceletationBuff(id, duration, m_value);
}