using UnityEngine;

[CreateAssetMenu(fileName = "Bubble", menuName = "Upgrades/Bubble")]
public class BubbleDataSO : ScriptableObject
{
    [field: SerializeField] public float Cooldown { get; private set; }
    [field: SerializeField] public float cooldownDecreasePerLevel;
}