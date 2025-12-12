using UnityEngine;
using UnityEngine.InputSystem;

namespace Players
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(NaveshMouseResolver))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerMovement m_movement;
        [SerializeField] private NaveshMouseResolver m_mouseResolver;
        [SerializeField] private Transform m_targetPositon;
        [SerializeField] private PlayerConfig m_config;

        private void OnValidate()
        {
            if (!m_movement)
            {
                m_movement = GetComponent<PlayerMovement>();
            }
            if (!m_mouseResolver)
            {
                m_mouseResolver = GetComponent<NaveshMouseResolver>();
            }
        }

        private void Start()
        {
            m_mouseResolver.Initialize(Camera.main);
            m_movement.Initialize(m_config.m_speed);
        }

        private void Update()
        {
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                Vector3 mousePosition = Mouse.current.position.ReadValue();
                Vector3? navPoint = m_mouseResolver.GetNavMeshPoint(mousePosition);

                if(navPoint.HasValue)
                {
                    m_movement.SetDestination(navPoint.Value);
                }
            }
        }
    }
}