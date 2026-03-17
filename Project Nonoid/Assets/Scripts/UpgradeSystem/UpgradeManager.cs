using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<UpgradeSO> allUpgrades;
    [SerializeField] private Bubble bubble;
    [SerializeField] private Bee bee;
    [SerializeField] private Shockwave shockwave;
    [SerializeField] private PlayerStatsDataSO playerStatsData;
    [SerializeField] private Player player;
    [SerializeField] private UpgradePopup popup;

    private const int UpgradeOptionsCount = 3;

    private void Start()
    {
        XPManager.Instance.OnLevelUpEvent += TriggerUpgrade;
    }

    private void TriggerUpgrade()
    {
        GameManager.Instance.EnterLevelUp();

        List<UpgradeSO> options = GetRandomUpgrades(UpgradeOptionsCount);
        popup.Show(options, OnUpgradeChosen);
    }

    private List<UpgradeSO> GetRandomUpgrades(int count)
    {
        List<UpgradeSO> result = new();
        var possibleUpgrades = allUpgrades.ToList();

        while (result.Count < count)
        {
            UpgradeSO pick = possibleUpgrades[Random.Range(0, possibleUpgrades.Count)];
            result.Add(pick);
            possibleUpgrades.Remove(pick);
        }

        return result;
    }

    private void OnUpgradeChosen(UpgradeSO chosen)
    {
        switch (chosen.StatType)
        {
            case UpgradeTypes.MaxHp:
                player.IncreaseMaxHealth(chosen.Amount);
                break;

            case UpgradeTypes.MoveSpeed:
                player.IncreaseSpeed(chosen.Amount);
                break;

            case UpgradeTypes.AttackDamage:
                player.IncreaseDamage(chosen.Amount);
                break;

            case UpgradeTypes.Bubble:
                bubble.LevelUp();
                break;

            case UpgradeTypes.Bee:
                bee.LevelUp();
                break;

            case UpgradeTypes.Shockwave:
                shockwave.LevelUp();

                break;
        }

        GameManager.Instance.ResumeGame();
    }
}