using UnityEngine;

public class XPGem : MonoBehaviour
{
    [SerializeField] private float _magnetSpeed = 10f;

    private float _xp;

    private bool _isPicked;
    private Transform _target;

    public void Init(DropData data)
    {
        _xp = data.XPReward;
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
        transform.position += dir * (_magnetSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isPicked || _target == null) return;
        if (other.transform != _target) return;

        Debug.Log($"XP Absorbed: {_xp}");
        gameObject.SetActive(false);
    }

    public void OnPick(Transform target)
    {
        _target = target;
        _isPicked = true;
    }
}