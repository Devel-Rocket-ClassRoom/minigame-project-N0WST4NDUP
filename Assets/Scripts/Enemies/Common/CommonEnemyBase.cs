using UnityEngine;

[RequireComponent(typeof(ShipBody))]
public abstract class CommonEnemyBase : MonoBehaviour
{
    [SerializeField] private ShipData _data;
    protected ShipBody _body;

    // TODO: protected CommonPool _pool;

    private void Awake()
    {
        _body = GetComponent<ShipBody>();
    }

    public void Init() // TODO: stage 추가시 체력 증가 등 초기화 작업 추가
    {
        _body.Init(_data);
    }
}