using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRegistry : Singleton<EnemyRegistry>
{
    private readonly List<Enemy> _aliveEnemies = new List<Enemy>();
    public event Action OnEnemyKilled;

    public void Register(Enemy enemy)
    {
        _aliveEnemies.Add(enemy);
    }

    public void Unregister(Enemy enemy)
    {
        if (_aliveEnemies.Contains(enemy))
            _aliveEnemies.Remove(enemy);
        OnEnemyKilled?.Invoke();
    }

    public Enemy GetClosestEnemy(Vector2 position)
    {
        Enemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var enemy in _aliveEnemies)
        {
            float distance = Vector2.Distance(position, enemy.transform.position);
            if (distance > closestDistance) continue;

            closestDistance = distance;
            closestEnemy = enemy;
        }

        return closestEnemy;
    }
}