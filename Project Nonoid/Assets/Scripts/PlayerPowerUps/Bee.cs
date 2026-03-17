using System.Collections;
using UnityEngine;

public class Bee : MonoBehaviour
{
    [SerializeField] private BeeDataSO beeData;

    private int _currentLevel;
    private bool _hasBee;

    private void Start()
    {
        StartCoroutine(AttackCoroutine());
    }

    private void Update()
    {
        OrbitAroundPlayer();
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(beeData.FireRate);
            Attack();
        }
        // ReSharper disable once IteratorNeverReturns
    }


    private void Attack()
    {
        var enemy = EnemyRegistry.Instance.GetClosestEnemy(transform.position);
        if (enemy == null)
            return;

        if (Vector3.Distance(enemy.transform.position, transform.position) < beeData.Range)
            enemy.OnBeeHit(GetSlowPercentage(), GetDamageAmount());
    }

    private void OrbitAroundPlayer()
    {
        var angle = beeData.OrbitSpeed * Time.time;
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * beeData.OrbitRadius;

        transform.localPosition = offset;
    }


    private float GetSlowPercentage()
    {
        return (_currentLevel - 1) * beeData.SlowAmountPercentageIncreasePerLevel
               + beeData.SlowAmountPercentage;
    }

    private int GetDamageAmount()
    {
        return beeData.DamagePerLevel * (_currentLevel - 1);
    }

    public void LevelUp()
    {
        gameObject.SetActive(true);
        _currentLevel++;
    }
}