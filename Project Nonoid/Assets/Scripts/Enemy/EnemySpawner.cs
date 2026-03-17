using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Player player;
    [SerializeField] private float spawnInterval;
    [SerializeField] private List<EnemyDataSO> enemyTypes;
    [SerializeField] private Transform healthIndicatorParent;

    private ObjectPool<Enemy> _enemyPool;

    private void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(
            createFunc: CreateEnemy,
            actionOnGet: OnTakeFromPool,
            actionOnRelease: OnReturnedToPool,
            actionOnDestroy: OnDestroyPooledObject,
            collectionCheck: false,
            defaultCapacity: 5,
            maxSize: 20
        );

        StartCoroutine(SpawningCoroutine());
    }

    private IEnumerator SpawningCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            if(GameStageHandler.IsBossStage())
                yield break;
            
            _enemyPool.Get();
        }
        // ReSharper disable once IteratorNeverReturns
    }

    private Enemy CreateEnemy()
    {
        var enemy = Instantiate(enemyPrefab);
        return enemy;
    }

    private void OnTakeFromPool(Enemy enemy)
    {
        enemy.OnSpawn(player, GetRandomEnemyType(), healthIndicatorParent);

        enemy.gameObject.SetActive(true);

        enemy.SetPool(_enemyPool);

        EnemyRegistry.Instance.Register(enemy);

        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        enemy.transform.position = spawnPoint.position;
    }

    private EnemyDataSO GetRandomEnemyType()
    {
        List<EnemyDataSO> available = enemyTypes
            .Where(e => e.AvailableInGameStates.Contains(GameStageHandler.GameStage))
            .ToList();

        int index = Random.Range(0, available.Count);
        return available[index];
    }

    private void OnReturnedToPool(Enemy enemy)
    {
        enemy.OnDespawn();
        EnemyRegistry.Instance.Unregister(enemy);
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}