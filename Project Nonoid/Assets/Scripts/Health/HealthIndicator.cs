using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Vector3 offset = new Vector3(0, 1f, 0);

    private Camera _cam;

    private Transform _target;

    private void Awake()
    {
        _cam = Camera.main;
    }

    public void Initialize(Health health, Transform target)
    {
        _target = target;
        slider.maxValue = health.MaxHealth;
        slider.value = health.CurrentHealth;
        MoveBar();
        
        health.OnHealthChanged += (current, max) =>
        {
            slider.maxValue = max;
            slider.value = current;
        };
    }

    private void LateUpdate()
    {
        if (_target == null || _cam == null) return;

        MoveBar();
    }

    private void MoveBar()
    {
        Vector3 worldPosition = _target.position + offset;
        Vector3 screenPosition = _cam.WorldToScreenPoint(worldPosition);
        slider.transform.position = screenPosition;
    }
}