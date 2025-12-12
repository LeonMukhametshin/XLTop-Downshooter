using UnityEngine;

namespace Players
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Player Config")]
    public sealed class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] [Range(0,100)] public float speed { get; private set; } = 5;
        [field: SerializeField] public Texture2D cursoreTexture { get; private set; }
        [field: SerializeField] [Min(0)] public float m_angularSpeed { get; private set; } = 500f;
    }
}
