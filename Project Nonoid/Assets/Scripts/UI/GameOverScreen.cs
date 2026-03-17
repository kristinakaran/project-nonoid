using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private StatsManager stats;
    [SerializeField] private TextMeshProUGUI enemyText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI levelText;
    
    
    private void OnEnable()
    {
        enemyText.text = stats.GetEnemyCounterText();
        timeText.text = stats.GetSecondsText();
        levelText.text = stats.GetLevelText();
    }
}