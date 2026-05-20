using UnityEngine;
using UnityEngine.Pool;

public class CommonPool : MonoBehaviour
{
    [SerializeField] private CommonEnemyBase _prefab;
    [SerializeField] private int _defaultCapacity = 16;
    [SerializeField] private int _maxSize = 100;

    private ObjectPool<CommonEnemyBase> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<CommonEnemyBase>(
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize,
            createFunc: () =>
            {
                var enemy = Instantiate(_prefab, transform);
                enemy.SetPool(this);
                enemy.gameObject.SetActive(false);
                return enemy;
            },
            actionOnGet: enemy =>
            {
                enemy.gameObject.SetActive(true);
            },
            actionOnRelease: enemy =>
            {
                enemy.Reset();
                enemy.gameObject.SetActive(false);
            },
            actionOnDestroy: enemy =>
            {
                Destroy(enemy.gameObject);
            },
            collectionCheck: true
        );
    }

    public CommonEnemyBase Get() => _pool.Get();

    public void Release(CommonEnemyBase enemy) => _pool.Release(enemy);
}
