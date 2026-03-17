using UnityEngine;

[CreateAssetMenu(fileName = "ShockwaveData", menuName = "Upgrades/ Shockwave")]
public class ShockwaveDataSO : ScriptableObject
{
    [field: SerializeField] public int BaseDamage { get; private set; }
    [field: SerializeField] public float BaseRadius { get; private set; }
    [field: SerializeField] public float Cooldown { get; private set; }
    [field: SerializeField] public int DamagePerLevel { get; private set; }
    [field: SerializeField] public float RadiusPerLevel { get; private set; }
}