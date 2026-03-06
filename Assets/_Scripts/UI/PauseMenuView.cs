using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class PauseMenuView : MonoBehaviour
{
    public event Action continueClicked;
    public event Action mainMenuClicked;

    [SerializeField] private Button m_continue;
    [SerializeField] private Button m_mainMenu;
    [SerializeField] private Button m_settings;

    private void OnEnable()
    {
        m_continue.onClick.AddListener(OnContinueClicked);
        m_mainMenu.onClick.AddListener(OnMainMenuClicked);
    }

    private void OnDisable()
    {
        m_continue.onClick.RemoveListener(OnContinueClicked);
        m_mainMenu.onClick.RemoveListener(OnMainMenuClicked);
    }

    private void OnContinueClicked()
    {
        continueClicked?.Invoke();
    }
    private void OnMainMenuClicked()
    {
        mainMenuClicked?.Invoke();
    }

}