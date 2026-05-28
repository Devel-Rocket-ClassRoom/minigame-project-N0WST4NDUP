using UnityEngine;
using UnityEngine.InputSystem;

public class BossDebugHelper : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private ShipBody _bossBody;

    [Header("Damage")]
    [SerializeField] private float _damagePerHit = 100f;
    [SerializeField] private float _bigDamagePerHit = 500f;

    private void Update()
    {
        var kb = Keyboard.current;
        if (kb == null || _bossBody == null) return;

        if (kb.f1Key.wasPressedThisFrame)
        {
            _bossBody.OnDamaged(_damagePerHit);
            Debug.Log($"[BossDebug] -{_damagePerHit} dmg → HP: {_bossBody.CurrentHealth}/{_bossBody.MaxHealth}");
        }

        if (kb.f2Key.wasPressedThisFrame)
        {
            _bossBody.OnDamaged(_bigDamagePerHit);
            Debug.Log($"[BossDebug] -{_bigDamagePerHit} dmg → HP: {_bossBody.CurrentHealth}/{_bossBody.MaxHealth}");
        }

        if (kb.f3Key.wasPressedThisFrame)
        {
            _bossBody.OnDamaged(_bossBody.CurrentHealth);
            Debug.Log($"[BossDebug] Kill → HP: {_bossBody.CurrentHealth}/{_bossBody.MaxHealth}");
        }
    }
}
