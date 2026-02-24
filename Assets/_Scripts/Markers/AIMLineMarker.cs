using Players;
using UnityEngine;

public class AIMLineMarker : MonoBehaviour
{
    [SerializeField] private LineRenderer m_lineRenderer;
    [SerializeField] private MouseResolver m_mouseResolver;

    [SerializeField] private float m_zOffcet = 0.5f;
    [SerializeField] private float m_lineWidth = 0.1f;
    [SerializeField] private float m_disableDistance = 1f;

    private Transform m_playerTransfrom;
    
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
        if(m_playerTransfrom is null)
        {
            return;
        }

        var playerPosition = m_playerTransfrom.position;
        var end = GetAimPosition();

        var directiion = (end - playerPosition).normalized;
        var start = playerPosition + directiion * m_zOffcet;

        start.y = playerPosition.y;
        end.y = playerPosition.y;

        m_lineRenderer.SetPosition(index: 0, start);
        m_lineRenderer.SetPosition(index: 1, end);
        m_lineRenderer.enabled = Vector3.Distance(start, end) > m_disableDistance;
    }

    public void Initialize(Transform playerTransfrom)
    {
        m_playerTransfrom = playerTransfrom;
    }

    private Vector3 GetAimPosition()
    {
        var worldPosition = m_mouseResolver.GetCursoureWorldPosition();

        if(worldPosition.HasValue)
        {
            return worldPosition.Value;
        }

        return m_playerTransfrom.position + m_playerTransfrom.forward;
    }
}