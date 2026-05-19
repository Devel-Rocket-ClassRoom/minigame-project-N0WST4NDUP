using UnityEngine;

public class ShipBody : MonoBehaviour, IDamageable
{
    private float _maxHealth;
    private float _currentHealth;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    public void Init(ShipData data)
    {
        _maxHealth = data.Health;
        _currentHealth = _maxHealth;
    }

    public void OnDamaged(float damage)
    {
        if (_currentHealth <= 0f) return;

        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}