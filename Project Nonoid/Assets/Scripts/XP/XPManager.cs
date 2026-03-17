using System;
using UnityEngine;

public class XPManager : Singleton<XPManager>
{
    private int _currentXp;
    private int _level;
    private int _xpToNextLevel = 100;
    public event Action OnLevelUpEvent;

    public void AddXp(int amount)
    {
        _currentXp += amount;

        if (_currentXp >= _xpToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        const float xpMultiplierToNextLevel = 1.25f;
        _level++;
        _currentXp -= _xpToNextLevel;
        _xpToNextLevel = Mathf.RoundToInt(_xpToNextLevel * xpMultiplierToNextLevel);

        OnLevelUpEvent?.Invoke();
    }
}