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

        private PlayerRotationCulculator m_playerRotationCulculator;

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
            var camera = Camera.main;
            m_mouseResolver.Initialize(camera);
            m_movement.Initialize(m_config.speed, m_config.m_angularSpeed);
            m_playerRotationCulculator = new PlayerRotationCulculator(camera, transform);

            SetupCursor();
        }

        private void Update()
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            var loockPoint = m_playerRotationCulculator.Calculate(mousePosition);
            m_movement.RotateTowards(loockPoint);

            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                Vector3? navPoint = m_mouseResolver.GetNavMeshPoint(mousePosition);

                if(navPoint.HasValue)
                {
                    m_movement.SetDestination(navPoint.Value);
                }
            }
        }

        private void SetupCursor()
        {
            var textures = m_config.cursoreTexture;
            var hospot = new Vector2(textures.width / 2f, textures.height / 2f);
            if(textures is not null)
            {
                Cursor.SetCursor(textures, hospot, CursorMode.Auto);
            }
        }
    }
}