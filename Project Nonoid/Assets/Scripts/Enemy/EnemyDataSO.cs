using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Game/Enemy Type")]
public class EnemyDataSO : ScriptableObject
{
    [field: SerializeField] public int MaxHealth { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public List<GameStage> AvailableInGameStates { get; private set; }

    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float AttackRate { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    
    //TODO: Kristina, popraviti AttackRange, treba da povecam da se ne sudaraju sa playerom
}