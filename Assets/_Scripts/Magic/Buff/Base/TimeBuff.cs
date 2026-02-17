using System;
using UnityEngine;

[Serializable]
public abstract class TimeBuff : BaseBuff, ITimeBuff
{
    [SerializeField] private float m_duration;

    public float duration => m_duration;

    [field: NonSerialized] 
    public float timer { get; private set; }

    public TimeBuff() { }

    protected TimeBuff(string id, Sprite icon, BuffType type, float duration) 
        : base(id, icon, type)
    {
        m_duration = duration;
    }

    protected override void OnInitialize()
    {
        timer = m_duration;
        base.OnInitialize();
    }

    protected override void OnDeinitializing() =>
        timer = 0;

    public sealed override void Update(float deltaTime)
    {
        if(timer > m_duration)
        {
            OnUpdated(deltaTime);
            timer -= deltaTime;
        }
        else
        {
            Deinitialize();
        }
    }

    protected virtual void OnUpdated(float deltaTime) { } 
}