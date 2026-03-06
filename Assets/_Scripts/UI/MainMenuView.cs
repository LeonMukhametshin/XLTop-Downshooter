using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI
{
    public class MainMenuView : MonoBehaviour
    {
        public event Action playClicked;
        public event Action exitClicked;

        private Loading m_loading;
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

        private void Start()
        {
            m_loading = ServiceLocator.Resolved<Loading>();
        }

        private void OnPlayerClicked()
        {
            playClicked?.Invoke();
            m_loading.LoadScene(GlobalConstants.Scenes.Game);
        }
   
        private void OnExitClicked()
        {
            Application.Quit();
        }
    }
}

public class ServiceLocator
{
    private static ServiceLocator m_serviceLocator;

    private Dictionary<Type, object> m_services = new();

    public static void Register<T>(T instance) where T : class
    {
        m_serviceLocator ??= new ServiceLocator();
        m_serviceLocator.m_services.Add(typeof(T), instance);
    }

    public static T Resolved<T>() where T : class
    {
        if(m_serviceLocator is null)
        {
            throw new NullReferenceException("Serivece locator is null");
        }

        return m_serviceLocator.m_services[typeof(T)] as T;
    }
}