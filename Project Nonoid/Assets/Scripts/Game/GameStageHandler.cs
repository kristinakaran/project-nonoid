public static class GameStageHandler
{
    public static GameStage GameStage { get; private set; }

    public static void SetGameState(GameStage stage)
    {
        GameStage = stage;
    }

    public static bool IsBossStage()
    {
        return GameStage == GameStage.Boss;
    }
}

public enum GameStage
{
    EarlyStage,
    MidStage,
    LateStage,
    Boss
}