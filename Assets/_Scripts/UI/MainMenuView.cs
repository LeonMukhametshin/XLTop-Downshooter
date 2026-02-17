using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI
{
    public class MainMenuView : MonoBehaviour
    {
        public event Action playClicked;
        public event Action exitClicked;

        [SerializeField] private Button m_playerButton;
        [SerializeField] private Button m_exitButton;

        private void OnEnable()
        {
            m_playerButton.onClick.AddListener(OnPlayerClicked);
            m_exitButton.onClick.AddListener(OnExitClicked);
        }

        private void OnDisable()
        {
            m_playerButton.onClick.RemoveListener(OnPlayerClicked);
            m_exitButton.onClick.RemoveListener(OnExitClicked);
        }

        private void OnPlayerClicked() =>
             playClicked?.Invoke();

        private void OnExitClicked() =>
             exitClicked?.Invoke();
    }
}