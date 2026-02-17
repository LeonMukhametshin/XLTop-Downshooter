using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public AttackEnemyType enemyType { get; private set; }
    [field: SerializeField] [Min(0)] public float health;
    [field: SerializeField] public BaceSpellData defaultSpell { get; private set; }
    [field: SerializeField] [Range(0,100)] public float speed { get; private set; }
    [field: SerializeField] [Min(0)] public float attackTime { get; private set; }
    [field: SerializeField] [Min(0)] public float attackRange { get; private set; }
    [field: SerializeField] private SpeelEnemyData[] m_spells;

    public IReadOnlyList<SpeelEnemyData> spell => m_spells;
}

[Serializable]
public struct SpeelEnemyData
{
    //TODO refactoring
    [SerializeField] public BaceSpellData data;
    [SerializeField] public int count;
}