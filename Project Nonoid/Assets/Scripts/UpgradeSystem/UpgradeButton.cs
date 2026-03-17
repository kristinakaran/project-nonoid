using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text descriptionText;

    public void Setup(UpgradeSO upgrade, Action<UpgradeSO> onClick)
    {
        titleText.text = upgrade.Name;
        descriptionText.text = upgrade.Description;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClick(upgrade));
    }
}