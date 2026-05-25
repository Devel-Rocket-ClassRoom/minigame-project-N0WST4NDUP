using UnityEngine;

public class XPGem : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;

    private int _xp;

    private bool _isPicked;
    private Transform _target;
    private LayerMask _targetLayer;

    private PlayerXP _playerXp;

    private XPGemPool _pool;

    public void SetPool(XPGemPool pool) => _pool = pool;

    public void Init(int xp)
    {
        _xp = xp;
    }

    public void Reset()
    {
        _isPicked = false;
        _target = null;
    }

    private void Update()
    {
        if (!_isPicked) return;
        if (_target == null) return;

        var dir = (_target.position - transform.position).normalized;
        transform.position += dir * (_moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isPicked || _target == null) return;
        if (other.transform != _target) return;

        if (_targetLayer == other.gameObject.layer)
        {
            _playerXp?.AddXp(_xp);
            _pool?.Release(this);
        }
    }

    public void OnPick(Transform target)
    {
        _isPicked = true;
        _target = target;
        _targetLayer = target.gameObject.layer;
        target.TryGetComponent(out _playerXp);     // 캐싱
    }
}