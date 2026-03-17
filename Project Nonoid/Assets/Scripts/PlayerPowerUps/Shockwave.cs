using System.Collections;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    [SerializeField] private ShockwaveDataSO shockwaveData;
    [SerializeField] private Transform player;
    [SerializeField] private Transform shockwaveObject;
    [SerializeField] private PlayerMovement playerMovement;

    private int _currentLevel;
    private int CurrentLevelIndex => _currentLevel - 1;

    private void Start()
    {
        StartCoroutine(ShockwaveRoutine());
    }

    private IEnumerator ShockwaveRoutine()
    {
        while (true)
        {
            TriggerShockwave();
            yield return new WaitForSeconds(shockwaveData.Cooldown);
        }
        // ReSharper disable once IteratorNeverReturns
    }

    private void TriggerShockwave()
    {
        RotateShockwaveObject();

        StartCoroutine(ShockwaveVisual());

        Collider2D[] hits = Physics2D.OverlapCircleAll(shockwaveObject.position, GetCurrentRadius());

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(GetCurrentDamage());
            }
        }
    }

    private void RotateShockwaveObject()
    {
        Vector2 dir = playerMovement.MoveDirection;

        if (dir.sqrMagnitude < 0.01f)
            return;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        shockwaveObject.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    private int GetCurrentDamage()
    {
        return shockwaveData.BaseDamage +
               shockwaveData.DamagePerLevel * CurrentLevelIndex;
    }

    private float GetCurrentRadius()
    {
        return shockwaveData.BaseRadius +
               shockwaveData.RadiusPerLevel * CurrentLevelIndex;
    }

    private IEnumerator ShockwaveVisual()
    {
        shockwaveObject.gameObject.SetActive(true);

        shockwaveObject.localScale = Vector3.zero;

        float t = 0f;
        float duration = 0.15f;
        float radius = GetCurrentRadius();

        while (t < duration)
        {
            t += Time.deltaTime;
            float progress = t / duration;

            float scale = Mathf.Lerp(0f, radius * 2f, progress);

            shockwaveObject.localScale = new Vector3(scale, scale, 1f);

            yield return null;
        }

        shockwaveObject.gameObject.SetActive(false);
    }

    public void LevelUp()
    {
        gameObject.SetActive(true);
        _currentLevel++;
    }
}