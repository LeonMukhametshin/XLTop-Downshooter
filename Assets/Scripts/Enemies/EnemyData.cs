using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public AttackEnemyType enemyType { get; private set; }
    [field: SerializeField] [Min(0)] public float health;
    [field: SerializeField] [Range(0,100)] public float speed { get; private set; }
    [field: SerializeField] [Min(0)] public float attackTime { get; private set; }
    [field: SerializeField] [Min(0)] public float attackRange { get; private set; }
    [field: SerializeField] public BaceSpellData spellData { get; private set; }

    //TODO add projectile range 
    //TODO add damage 
}