using System;
using Unity.Behavior;
using UnityEngine;

public class PirateLord : MonoBehaviour
{
    public enum Phase
    {
        P1 = 0,
        P2 = 1,
        P3 = 2
    }

    [SerializeField] private PirateLordData _data;

    [Header("Dependencies")]
    [SerializeField] private ShipBody _body;
    [SerializeField] private ShipMovement _movement;
    [SerializeField] private Collider _collider;
    [SerializeField] private BehaviorGraphAgent _agent;
    [SerializeField] private Transform _target; // 임시

    private Phase _currentPhase = Phase.P1;
    private bool _decaying = false;

    [Header("Patterns")]
    [SerializeField] private RadialSweepAttack _radialSweep;

    public static event Action<Vector3> OnBossSpawned;
    public static event Action<Phase> OnPhaseChanged;
    public static event Action<Vector3> OnBossDeathEvent;

    private void Awake()
    {
        _body.Init(_data);
        _body.OnDeadEvent += HandleP2Death;
        _movement.SetData(_data.PhaseMovements[(int)_currentPhase]);
        _collider.enabled = true;
        _agent.SetVariableValue("Target", _target == null ? null : _target.gameObject);
    }

    private void Start()
    {
        OnBossSpawned?.Invoke(transform.position);
    }

    private void OnDestroy()
    {
        _body.OnDeadEvent -= HandleP2Death;
        _body.OnDeadEvent -= HandleP3Death;
    }

    private void Update()
    {
        if (_decaying)
        {
            _body.OnDamaged(_data.Phase3DecayPerSecond * Time.deltaTime);
        }

        if (_currentPhase == Phase.P1 && _body.CurrentHealth <= _body.MaxHealth * _data.Phase1ToPhase2HpThreshold)
        {
            ChangePhase(Phase.P2);
        }
    }

    private void ChangePhase(Phase phase)
    {
        if (phase == _currentPhase) return;

        _currentPhase = phase;
        _movement.SetData(_data.PhaseMovements[(int)_currentPhase]);


        _radialSweep.enabled = (phase != Phase.P3);


        OnPhaseChanged?.Invoke(_currentPhase);
    }

    private void HandleP2Death()
    {
        _body.OnDeadEvent -= HandleP2Death;

        _body.Repair(_body.MaxHealth);
        _collider.enabled = false;
        ChangePhase(Phase.P3);
        _decaying = true;

        _body.OnDeadEvent += HandleP3Death;
    }

    private void HandleP3Death()
    {
        // 죽는 모션, 보상 드랍 등등
        Debug.Log("Pirate Lord defeated!");
        OnBossDeathEvent?.Invoke(transform.position);
    }
}