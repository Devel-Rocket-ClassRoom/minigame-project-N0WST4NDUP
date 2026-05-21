using UnityEngine;
using UnityEngine.Pool;

public class CombatPool : MonoBehaviour
{
    [SerializeField] private CombatItemBase _prefab;
    [SerializeField] private int _defaultCapacity = 32;
    [SerializeField] private int _maxSize = 200;

    private ObjectPool<CombatItemBase> _pool;

    public int Active => _pool.CountActive;

    private void Awake()
    {
        CombatPoolRegistry.Register(_prefab.GetType(), this);

        _pool = new ObjectPool<CombatItemBase>(
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize,
            createFunc: () =>
            {
                var item = Instantiate(_prefab, transform);
                item.SetPool(this);
                item.gameObject.SetActive(false);
                return item;
            },
            actionOnGet: item =>
            {
                item.gameObject.SetActive(true);
            },
            actionOnRelease: item =>
            {
                item.Reset();
                item.gameObject.SetActive(false);
            },
            actionOnDestroy: item =>
            {
                Destroy(item.gameObject);
            },
            collectionCheck: true
        );
    }

    private void OnDestroy()
    {
        if (_prefab != null)
            CombatPoolRegistry.Unregister(_prefab.GetType());
    }

    public CombatItemBase Get() => _pool.Get();

    public void Release(CombatItemBase item) => _pool.Release(item);
}
