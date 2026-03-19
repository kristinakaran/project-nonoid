using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameState _state = GameState.Playing;

    public event System.Action<GameState> OnGameStateChanged;
    public bool IsPlaying => _state == GameState.Playing;

    public GameState State => _state;


    private void SetState(GameState newState)
    {
        _state = newState;
        OnGameStateChanged?.Invoke(newState);
    }

    public void PauseGame()
    {
        _state = GameState.Paused;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        _state = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void EnterLevelUp()
    {
        _state = GameState.LevelingUp;
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER!");

        _state = GameState.GameOver;
        Time.timeScale = 0f;
        
        DatabaseManager.Instance.SaveGameRun("Player", 100, 5, 30f, 2);

        var ui = Object.FindFirstObjectByType<GameUI>();
        if (ui != null)
            ui.ShowGameOver();
    }

    public void Victory()
    {
        _state = GameState.Victory;
        Time.timeScale = 0f;
        
        var ui = Object.FindFirstObjectByType<GameUI>();
        if (ui != null)
            ui.ShowVictory();
    }

    public void StartBossFight(int fightBossDuration)
    {
        Object.FindFirstObjectByType<BossFightHandler>().StartBossFight(fightBossDuration);
    }
}