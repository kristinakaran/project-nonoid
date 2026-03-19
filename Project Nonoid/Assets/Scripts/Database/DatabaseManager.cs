using UnityEngine;
using SQLite4Unity3d;
using System.IO;
using System.Linq;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;

    private SQLiteConnection connection;
    private string dbPath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDatabase();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeDatabase()
    {
        dbPath = Path.Combine(Application.persistentDataPath, "gamedata.db");

        connection = new SQLiteConnection(dbPath);

        connection.CreateTable<GameRun>();

        Debug.Log("Database initialized at: " + dbPath);
    }
    
    public void SaveGameRun(string playerName, int score, int enemiesKilled, float survivalTime, int levelReached)
    {
        connection.Insert(new GameRun
        {
            PlayerName = playerName,
            Score = score,
            EnemiesKilled = enemiesKilled,
            SurvivalTime = survivalTime,
            LevelReached = levelReached,
            PlayedAt = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Debug.Log("Game run saved.");
    }
    
    public System.Collections.Generic.List<GameRun> GetTopRuns(int limit = 10)
    {
        return connection.Table<GameRun>()
            .OrderByDescending(run => run.Score)
            .Take(limit)
            .ToList();
    }
}

public class GameRun
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string PlayerName { get; set; }
    public int Score { get; set; }
    public int EnemiesKilled { get; set; }
    public float SurvivalTime { get; set; }
    public int LevelReached { get; set; }
    public string PlayedAt { get; set; }
}