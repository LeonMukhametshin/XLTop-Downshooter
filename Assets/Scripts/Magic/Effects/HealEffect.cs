using System;
using UnityEngine;

[Serializable]
public class HealEffect : IEffect
{
    [SerializeField] private float m_health;

    public void Apply(IEffectable effectable)
    {
        //Do something
    }
}