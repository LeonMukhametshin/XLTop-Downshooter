using System.Collections.Generic;
using UnityEngine;

public class SpellAoe : MonoBehaviour, ISpellAoe
{
    public void Initialize(Vector3 targetPosition, float radius, IReadOnlyCollection<IEffect> effects)
    {
        var colliders = Physics.OverlapSphere(targetPosition, radius);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<IEffectable>(out var effectable))
            {
                foreach (var effect in effects)
                {
                    effect.Apply(effectable);
                }
            }
        }
    }
}
