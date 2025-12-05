using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Players
{
    public class NaveshMouseResolver : MonoBehaviour
    {
        [SerializeField] private LayerMask m_layerMak;
        [SerializeField] [Min(0)] private float m_raycastDistance = 1000f;
        [SerializeField] [Min(0)] private float m_navMeshSampleMaxDistance = 100f;

        private Camera m_camera;

        public void Initialize(Camera camera)
        {
            m_camera = camera;
        }

        public Vector3? GetNavMeshPoint(Vector3 mousePosition)
        {
            var ray = m_camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit, m_raycastDistance, m_layerMak))
            {
                if (NavMesh.SamplePosition(hit.point, out var navHit, m_navMeshSampleMaxDistance, NavMesh.AllAreas))
                {
                    return navHit.position;
                }
            }

            return null;
        }
    }
}