using UnityEngine;

public sealed class PlayerRotationCulculator
{
    private readonly Camera m_camera;
    private readonly Transform m_playerTransform;

    public PlayerRotationCulculator(Camera camera, Transform playerTransform)
    {
        m_camera = camera;
        m_playerTransform = playerTransform;
    }

    public Vector3 Calculate(Vector3 mousePosition)
    {
        var playerScreenPosition = m_camera.WorldToScreenPoint(m_playerTransform.position);
        var delta = (Vector3)mousePosition - (Vector3)playerScreenPosition;

        var cameraRight = m_camera.transform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();

        var cameraForward = m_camera.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        var worldDirection = cameraRight * delta.x + cameraForward * delta.y;

        if (worldDirection.sqrMagnitude > 0.001f)
        {
            return m_playerTransform.position - worldDirection;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
