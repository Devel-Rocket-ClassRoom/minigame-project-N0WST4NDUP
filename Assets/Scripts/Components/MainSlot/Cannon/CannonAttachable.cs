using UnityEngine;

public class CannonAttachable : MainAttachableBase
{
    [SerializeField] protected float _upward = 5f;
    [SerializeField] protected Transform _firePoint;

    private CannonBase _cannon;

    public CannonBase Cannon => _cannon;

    private void Update()
    {
        _cannon?.Tick();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CannonTest();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (_cannon != null)
            {
                DoubleCannonTest();
            }
            else
            {
                Debug.Log("캐넌이 아직 없습니다.");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (_cannon != null)
            {
                TripleCannonTest();
            }
            else
            {
                Debug.Log("캐넌이 아직 없습니다.");
            }
        }
    }

    public override void Attach(Transform transform)
    {
        Instantiate(_prefab, transform);
    }

    public override void Detach()
    {
        Destroy(gameObject);
    }

    private void CannonTest()
    {
        if (_cannon == null)
        {
            _cannon = new Lv1_Cannon(_data);
        }
        else
        {
            _cannon = _cannon.Upgrade();
        }

        _cannon.Settings(_upward, _firePoint);
    }

    private void DoubleCannonTest()
    {
        if (_cannon is Lv1_Cannon || _cannon is Lv2_Cannon)
        {
            _cannon = new DoubleCannon(_cannon);
        }

        _cannon.Settings(_upward, _firePoint);
    }

    private void TripleCannonTest()
    {
        if (_cannon is Lv1_Cannon || _cannon is Lv2_Cannon)
        {
            _cannon = new TripleCannon(_cannon);
        }

        _cannon.Settings(_upward, _firePoint);
    }
}