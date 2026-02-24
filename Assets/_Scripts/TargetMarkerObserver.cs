using Players;
using UnityEngine;

public class TargetMarkerObserver : MonoBehaviour
{
    [SerializeField] private TargetMarker m_targetMarker;
    private PlayerMovement m_playerMovement;

    public void Initialize(PlayerMovement playerMovement)
    {
        m_playerMovement = playerMovement;

        m_playerMovement.Stopped += OnPlayerStopped;
        m_playerMovement.DestinationChanged += OnDestinationChanged;
    }

    private void Deinitialize()
    {
        m_playerMovement.Stopped -= OnPlayerStopped;
        m_playerMovement.DestinationChanged -= OnDestinationChanged;
    }

    private void OnPlayerStopped() =>
        m_targetMarker.Hide();

    private void OnDestinationChanged(Vector3 worldPosition) =>
        m_targetMarker.Show(worldPosition);
}