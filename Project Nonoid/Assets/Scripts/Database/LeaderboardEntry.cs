using System;

[Serializable]
public class LeaderboardEntry
{
    public string PlayerName { get; set; }
    public int Score { get; set; }
    public int EnemiesKilled { get; set; }
    public float SurvivalTime { get; set; }
    public int LevelReached { get; set; }
    public string PlayedAt { get; set; }

    public LeaderboardEntry() { }

    public LeaderboardEntry(string playerName, int score, int enemiesKilled, float survivalTime, int levelReached, string playedAt)
    {
        PlayerName = playerName;
        Score = score;
        EnemiesKilled = enemiesKilled;
        SurvivalTime = survivalTime;
        LevelReached = levelReached;
        PlayedAt = playedAt;
    }
}