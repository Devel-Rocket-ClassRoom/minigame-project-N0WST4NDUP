using UnityEngine;

public class ShipBody : MonoBehaviour, IDamageable
{
    private float _maxHealth;
    private float _currentHealth;
    private float _invincibleTime;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    public float InvincibleTime => _invincibleTime;

    private float _invincibleTimer;
    public bool IsInvincible => Time.time < _invincibleTimer;
    public bool IsDestroyed => _currentHealth <= 0f;

    public void Init(ShipData data)
    {
        _maxHealth = data.Health;
        _currentHealth = _maxHealth;
        _invincibleTime = data.InvincibleTime;
    }

    public void OnDamaged(float damage)
    {
        if (IsInvincible || IsDestroyed) return;

        _currentHealth -= damage;
        _invincibleTimer = Time.time + _invincibleTime;
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