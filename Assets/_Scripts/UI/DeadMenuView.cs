using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI
{
    public class DeadMenuView : MonoBehaviour
    {
        public event Action goToMenuClicked;

        [SerializeField] private Button m_mainMenuButton;

        private void OnEnable() =>
            m_mainMenuButton.onClick.AddListener(OnCklicked);

        private void OnDisable() =>
             m_mainMenuButton.onClick.RemoveListener(OnCklicked);

        public void OnCklicked() =>
            goToMenuClicked?.Invoke();
    }
}