using UnityEngine;
[CreateAssetMenu(menuName = "Player/PlayerStats")]
public class PlayerStatsDataSO : ScriptableObject
{
    [Header("Health")]
    [field: SerializeField] public int MaxHealth { get; private set; }

    [Header("Offense")] 
    [field: SerializeField] public int AttackDamage { get; set; }
    [field: SerializeField] public float AttackCooldown { get; set; }

    [Header("Movement")]
    [field: SerializeField] public float MoveSpeed { get; set; }
}