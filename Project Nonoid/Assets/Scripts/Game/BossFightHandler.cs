using UnityEngine;
using System.Collections;

public class BossFightHandler : MonoBehaviour
{
    [SerializeField] private Enemy bossPrefab;
    [SerializeField] private EnemyDataSO data;
    [SerializeField] private Player player;
    [SerializeField] private Transform healthIndicatorParent;
    [SerializeField] private Transform bossSpawnLocation;
    
    private Coroutine _fightRoutine;
    private Enemy _boss;
    private int _bossFightDuration;
    

    public void StartBossFight(int bossFightDuration)
    {
        _boss = Instantiate(bossPrefab, transform.position, Quaternion.identity);
        _boss.transform.position =  bossSpawnLocation.position;
        _boss.OnSpawn(player, data, healthIndicatorParent);
        _boss.SubscribeActionOnDeath(HandleBossPrefabDeath);
        _bossFightDuration = bossFightDuration;
        
        _fightRoutine = StartCoroutine(FightTimerRoutine());
    }

    private IEnumerator FightTimerRoutine()
    {
        float timer = 0f;

        while (timer < _bossFightDuration)
        {
            timer += Time.deltaTime;
            yield return null;

            if (bossPrefab == null)
                yield break;
        }

        GameManager.Instance.GameOver();
    }

    private void HandleBossPrefabDeath()
    {
        if (_fightRoutine != null)
            StopCoroutine(_fightRoutine);

        Destroy(_boss.gameObject);
        GameManager.Instance.Victory();
    }
}