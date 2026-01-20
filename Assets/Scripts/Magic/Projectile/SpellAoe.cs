using System.Collections.Generic;
using UnityEngine;

public class SpellAoe : MonoBehaviour, ISpellAoe
{
    public void Initialize(Vector3 targetPosition, float radius, IReadOnlyCollection<IEffect> effects)
    {
        var colliders = Physics.OverlapSphere(targetPosition, radius);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.layer == gameObject.layer)
            {
                continue;
            }

            var effectables = collider.GetComponents<IEffectable>();
            effects.ApplyEffect(effectables);
        }
    }
}