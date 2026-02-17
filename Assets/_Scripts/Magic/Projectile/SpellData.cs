using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellData", menuName = "Scriptable Objects/SpellData")]
public class SpellData : ScriptableObject
{
    [field: SerializeField] public Vector3 targetPosition { get; }
    [field: SerializeField] public float speed { get; }
    [field: SerializeField] public IReadOnlyList<IEffect> effects { get; }
}