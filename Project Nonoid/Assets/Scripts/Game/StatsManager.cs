using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemyCounter;
    [SerializeField] private TextMeshProUGUI secondsInGame;
    [SerializeField] private TextMeshProUGUI levelUpCounter;

    private float _timeCounter;
    private int _level = 1;
    private int _enemyKilledCounter;

    private void Start()
    {
        XPManager.Instance.OnLevelUpEvent += UpdateCurrentLevel;
        EnemyRegistry.Instance.OnEnemyKilled += UpdateEnemyKilled;
        UpdateEnemyKilledText();
        UpdateCurrentLevelText();
    }

    private void Update()
    {
        _timeCounter += Time.deltaTime;
        secondsInGame.text = $"Timer: {(int)_timeCounter}";
    }

    private void UpdateEnemyKilled()
    {
        _enemyKilledCounter++;
        UpdateEnemyKilledText();
    }

    private void UpdateCurrentLevel()
    {
        _level++;
        UpdateCurrentLevelText();
    }

    private void UpdateCurrentLevelText()
    {
        levelUpCounter.text = $"Current Level: {_level}";
    }

    private void UpdateEnemyKilledText()
    {
        enemyCounter.text = $"Enemies Killed: {_enemyKilledCounter}";
    }
    
    public string GetEnemyCounterText() => enemyCounter.text;
    public string GetSecondsText() => secondsInGame.text;
    public string GetLevelText() => levelUpCounter.text;
}