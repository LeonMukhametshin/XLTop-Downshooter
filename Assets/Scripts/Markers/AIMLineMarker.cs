using Players;
using System;
using UnityEngine;

public class AIMLineMarker : MonoBehaviour
{
    [SerializeField] private Transform m_playerTransfrom;
    [SerializeField] private LineRenderer m_lineRenderer;
    [SerializeField] private MouseResolver m_mouseResolver;

    [SerializeField] private float m_zOffce = 0.5f;
    [SerializeField] private float m_lineWidth = 0.1f;
    [SerializeField] private float m_disableDistance = 1f;

    private void OnValidate()
    {
        if(!m_lineRenderer)
        {
            m_lineRenderer = GetComponent<LineRenderer>();
        }
    }

    private void Awake()
    {
        m_lineRenderer.positionCount = 2;
        m_lineRenderer.startWidth = m_lineWidth;
        m_lineRenderer.endWidth = m_lineWidth;
    }

    private void LateUpdate()
    {
        var playerPosition = m_playerTransfrom.position;
        var end = GetAimPosition();

        Vector3 directiion = (end - playerPosition).normalized;
    }

    private Vector3 GetAimPosition()
    {
        throw new NotImplementedException();
    }
}