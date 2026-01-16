using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IHealth, IEffectable
{
    public event Action died;
    public event Action valueChanged;

    private float m_value;
    private bool m_isInitialize = false;

    public float maxValue { get; private set; }

    public float value
    {
        get => m_value;
        private set
        {
            if (Mathf.Approximately(m_value, value))
            {
                return;
            }
            m_value = value < 0 ? 0: value;

            valueChanged?.Invoke();

            if(m_value is 0)
            {
                died?.Invoke();
            }
        }
    }

    public void Initialize(float value)
    {
        if (m_isInitialize)
            throw new InvalidOperationException("HealthComponent is already initialize");

        this.value = value;
        maxValue = value;
        m_isInitialize = true;  
    }

    public void Heal(float heal)
    {
        if (heal < 0)
            throw new ArgumentOutOfRangeException(nameof(heal), "Heal cannot be hegative");

        value += heal;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage), "Heal cannot be hegative");

        value += damage;
    }
}   