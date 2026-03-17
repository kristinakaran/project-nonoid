using System.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private GameObject bubble;
    [SerializeField] private BubbleDataSO bubbleData;

    private bool _isActive;
    private int _currentLevel;
    private int CurrentLevelIndex => _currentLevel - 1;
    public bool IsActive => _isActive;

    private void Start()
    {
        ActivateShield();
    }

    public void AbsorbHit()
    {
        DeactivateShield();

        StartCoroutine(ReactivateShieldAfterCooldown());
    }

    private void ActivateShield()
    {
        _isActive = true;
        bubble.SetActive(true);
    }

    private void DeactivateShield()
    {
        _isActive = false;
        bubble.SetActive(false);
    }

    private float GetCurrentCooldown()
    {
        const float minimalCooldown = 0.2f;
        float cooldown = bubbleData.Cooldown - bubbleData.cooldownDecreasePerLevel * (_currentLevel - 1);
        return Mathf.Max(minimalCooldown, cooldown);
    }

    private IEnumerator ReactivateShieldAfterCooldown()
    {
        yield return new WaitForSeconds(GetCurrentCooldown());
        ActivateShield();
    }

    public void LevelUp()
    {
        gameObject.SetActive(true);
        _currentLevel++;
    }
}