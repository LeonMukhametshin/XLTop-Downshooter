using System.Collections.Generic;
using UnityEngine;

public interface ISpellAoe
{
    public void Initialize(Vector3 worldPosition, float radius, IReadOnlyCollection<IEffect> effects);
}