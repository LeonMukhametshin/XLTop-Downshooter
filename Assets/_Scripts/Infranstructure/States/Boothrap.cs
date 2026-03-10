using UnityEngine;

[DefaultExecutionOrder(-500)]
public class Boothrap : MonoBehaviour
{
    [SerializeField] private Loading m_loading;

    private void Awake()
    {
        ServiceLocator.Clear();
        ServiceLocator.Register(m_loading);
    }
}

