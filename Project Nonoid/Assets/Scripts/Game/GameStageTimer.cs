using System.Collections;
using UnityEngine;

public class GameStageTimer : MonoBehaviour
{
    [SerializeField] private int earlyStageDuration;
    [SerializeField] private int midStageDuration;
    [SerializeField] private int lateStageDuration;
    [SerializeField] private int bossDuration;

    private bool _bossStarted;
    private float _bossStartTime;

    private float _timer;

    private void Start()
    {
        StartCoroutine(StageFlowRoutine());
    }

    private IEnumerator StageFlowRoutine()
    {
        GameStageHandler.SetGameState(GameStage.EarlyStage);
        yield return new WaitForSeconds(earlyStageDuration);
        
        GameStageHandler.SetGameState(GameStage.MidStage);
        yield return new WaitForSeconds(midStageDuration);
        
        GameStageHandler.SetGameState(GameStage.LateStage);
        yield return new WaitForSeconds(lateStageDuration);
        
        GameStageHandler.SetGameState(GameStage.Boss);
        GameManager.Instance.StartBossFight(bossDuration);
        
        yield return new WaitForSeconds(bossDuration);
    }
}