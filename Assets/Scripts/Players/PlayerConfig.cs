using UnityEngine;

namespace Players
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Player Config")]
    public sealed class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] [Range(0,100)] public float m_speed { get; set; } = 5;
    }
}
