using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Enemies.Views
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Image m_bar;
        [SerializeField] private HealthComponent m_healthComponent;

        private void OnEnable()
        {
            if (m_healthComponent)
            {
                m_healthComponent.valueChanged += SetValue;
            }

            SetValue();
        }

        private void OnDisable()
        {
            if (m_healthComponent)
            {
                m_healthComponent.valueChanged -= SetValue;
            }
        }

        private void SetValue()
        {
            if (!m_bar || !m_healthComponent || m_healthComponent.maxValue <= 0f)
            {
                if (m_bar)
                {
                    m_bar.fillAmount = 0f;
                }
                return;
            }

            m_bar.fillAmount = m_healthComponent.value / m_healthComponent.maxValue;
        }
    }
}
