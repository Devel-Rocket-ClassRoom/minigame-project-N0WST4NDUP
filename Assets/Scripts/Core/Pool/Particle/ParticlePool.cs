using UnityEngine;
using UnityEngine.Pool;

public class ParticlePool : MonoBehaviour
{
    [SerializeField] private ParticleKind _kind;
    [SerializeField] private PooledParticle _prefab;
    [SerializeField] private int _defaultCapacity = 16;
    [SerializeField] private int _maxSize = 100;

    private ObjectPool<PooledParticle> _pool;

    public int Active => _pool.CountActive;

    private void Awake()
    {
        ParticlePoolRegistry.Register(_kind, this);

        _pool = new ObjectPool<PooledParticle>(
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize,
            createFunc: () =>
            {
                var p = Instantiate(_prefab, transform);
                p.SetPool(this);
                p.gameObject.SetActive(false);
                return p;
            },
            actionOnGet: p => p.gameObject.SetActive(true),
            actionOnRelease: p => p.gameObject.SetActive(false),
            actionOnDestroy: p => Destroy(p.gameObject),
            collectionCheck: true
        );
    }

    private void OnDestroy() => ParticlePoolRegistry.Unregister(_kind);

    public PooledParticle Get() => _pool.Get();
    public void Release(PooledParticle p) => _pool.Release(p);
}
