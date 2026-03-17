using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public string Name;

    [TextArea] public string Description;
    
    public UpgradeTypes StatType;
    
    public int Amount;
}