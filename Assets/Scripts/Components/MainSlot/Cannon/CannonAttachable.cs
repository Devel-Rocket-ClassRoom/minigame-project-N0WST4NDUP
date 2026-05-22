using UnityEngine;

public class CannonAttachable : MainAttachableBase
{
    [Header("Cannon Config")]
    [SerializeField] protected Transform _firePoint;

    [Tooltip("포물선 정점 높이(m)")]
    [SerializeField] protected float _arcHeight = 5f;

    [Tooltip("비행 시간(초)")]
    [SerializeField] protected float _flightDuration = 0.7f;

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

    public override void Attach()
    {
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

        _cannon.SetBarrel(
            _target,
            _firePoint,
            _arcHeight,
            _flightDuration
        );
    }

    private void DoubleCannonTest()
    {
        if (_cannon is Lv1_Cannon || _cannon is Lv2_Cannon)
        {
            _cannon = new DoubleCannon(_cannon);
        }

        _cannon.SetBarrel(
            _target,
            _firePoint,
            _arcHeight,
            _flightDuration
        );
    }

    private void TripleCannonTest()
    {
        if (_cannon is Lv1_Cannon || _cannon is Lv2_Cannon)
        {
            _cannon = new TripleCannon(_cannon);
        }

        _cannon.SetBarrel(
            _target,
            _firePoint,
            _arcHeight,
            _flightDuration
        );
    }
}