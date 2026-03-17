using UnityEngine;

[CreateAssetMenu(fileName = "BeeData", menuName = "Upgrades/Bee")]
public class BeeDataSO : ScriptableObject
{
    [field: SerializeField] public int DamagePerLevel { get; private set; }
    [field: SerializeField] public float SlowAmountPercentage { get; private set; }
    [field: SerializeField] public float SlowAmountPercentageIncreasePerLevel { get; private set; }
    [field: SerializeField] public float FireRate { get; private set; }
    [field: SerializeField] public float OrbitRadius { get; private set; }
    [field: SerializeField] public float OrbitSpeed { get; private set; }
    [field: SerializeField] public float Range { get; private set; }
}