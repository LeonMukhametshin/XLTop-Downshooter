using UnityEngine;

namespace Cameras
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform m_target;
        [SerializeField] private Vector3 m_offcet = new(0, 15, -10);
        [SerializeField][Range(0, 1)] private float m_smoothTime = 0.5f;

        private Vector3 m_velocity;

        private void LateUpdate()
        {
            if(!m_target)
            {
                return;
            }

            var targetPosition = m_target.position + m_offcet;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref m_velocity, m_smoothTime);
        }

        public void SetTarget(Transform target) =>
            m_target = target;
    }
}