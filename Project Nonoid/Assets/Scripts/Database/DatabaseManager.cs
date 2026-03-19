using UnityEngine;
using SQLite4Unity3d;
using System.IO;

public class DatabaseManager : MonoBehaviour
{
    private string dbPath;

    private void Start()
    {
        dbPath = Path.Combine(Application.persistentDataPath, "gamedata.db");

        var connection = new SQLiteConnection(dbPath);

        connection.CreateTable<GameRun>();

        connection.Insert(new GameRun
        {
            PlayerName = "Test",
            Score = 100
        });

        Debug.Log("Database works!");
    }
}

public class GameRun
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string PlayerName { get; set; }

    public int Score { get; set; }
}