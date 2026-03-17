using UnityEngine;
using System;
using System.Collections.Generic;

public class UpgradePopup : MonoBehaviour
{
    [SerializeField] private UpgradeButton[] buttons;
    [SerializeField] private GameObject optionsContainer;

    private Action<UpgradeSO> _onChoice;

    public void Show(List<UpgradeSO> upgrades, Action<UpgradeSO> callback)
    {
        _onChoice = callback;
        optionsContainer.SetActive(true);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].Setup(upgrades[i], OnOptionSelected);
        }
    }

    private void OnOptionSelected(UpgradeSO upgrade)
    {
        optionsContainer.SetActive(false);
        _onChoice?.Invoke(upgrade);
    }
}