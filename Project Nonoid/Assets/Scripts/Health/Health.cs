using System;
public class Health
{
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    public event Action<int, int> OnHealthChanged;
    public event Action OnDeath;

    public Health(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth < 0)
            CurrentHealth = 0;

        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        if (CurrentHealth == 0)
            OnDeath?.Invoke();
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;

        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }
    
    public void AddMaxHealth(int amount)
    {
        MaxHealth += amount;
        CurrentHealth += amount;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    public void RemoveOnDeathSubscription()
    {
        OnDeath = null;
    }
}