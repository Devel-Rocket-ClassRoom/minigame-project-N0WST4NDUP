using UnityEngine;
using UnityEngine.Pool;

public class XPGemPool : MonoBehaviour
{
    public static XPGemPool Instance { get; private set; }

    [SerializeField] private XPGem _prefab;
    [SerializeField] private int _defaultCapacity = 32;
    [SerializeField] private int _maxSize = 256;

    private ObjectPool<XPGem> _pool;

    public int Active => _pool.CountActive;

    private void Awake()
    {
        Instance = this;
        _pool = new ObjectPool<XPGem>(
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize,
            createFunc: () =>
            {
                var gem = Instantiate(_prefab, transform);
                gem.SetPool(this);
                gem.gameObject.SetActive(false);
                return gem;
            },
            actionOnGet: gem =>
            {
                gem.gameObject.SetActive(true);
            },
            actionOnRelease: gem =>
            {
                gem.Reset();
                gem.gameObject.SetActive(false);
            },
            actionOnDestroy: gem =>
            {
                Destroy(gem.gameObject);
            },
            collectionCheck: true
        );
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public XPGem Get() => _pool.Get();

    public void Release(XPGem gem) => _pool.Release(gem);
}
