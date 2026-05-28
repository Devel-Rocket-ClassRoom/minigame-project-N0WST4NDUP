using System;
using Unity.Behavior;
using UnityEngine;

public class PirateLord : MonoBehaviour
{
    [SerializeField] private PirateLordData _data;
    [SerializeField] private GameObject _defaultShip;
    [SerializeField] private GameObject _ghostShip;

    [Header("Dependencies")]
    [SerializeField] private ShipBody _body;
    [SerializeField] private ShipMovement _movement;
    [SerializeField] private BehaviorGraphAgent _agent;
    [SerializeField] private Transform _target; // 임시

    public static event Action<Vector3> OnBossSpawned;
    public static event Action<Phase> OnPhaseChanged;
    public static event Action<Vector3> OnBossDeathEvent;

    public Phase CurrentPhase => _currentPhase;

    private Phase _currentPhase = Phase.P1;
    private bool _decaying = false;

    private void Awake()
    {
        _defaultShip.SetActive(true);
        _ghostShip.SetActive(false);

        _body.Init(_data);
        _movement.SetData(_data.PhaseMovements[(int)_currentPhase]);
        _agent.SetVariableValue("Target", _target == null ? null : _target.gameObject);
        _agent.SetVariableValue("Phase", _currentPhase);
    }

    private void Start()
    {
        Debug.Log($"[PirateLord] Spawned at {transform.position} | HP {_body.CurrentHealth}/{_body.MaxHealth} | Phase {_currentPhase}");
        OnBossSpawned?.Invoke(transform.position);
    }

    private void OnDestroy()
    {
        _body.OnDeadEvent -= HandleBossDeath;
    }

    private void Update()
    {
        if (_decaying)
        {
            _body.OnDamaged(_data.Phase3DecayPerSecond * Time.deltaTime);
        }

        // BT가 Phase Blackboard 변수를 갱신하면 부수효과 적용
        if (_agent.GetVariable<Phase>("Phase", out var phaseVar) && phaseVar.Value != _currentPhase)
        {
            ApplyPhaseTransition(phaseVar.Value);
        }
    }

    private void ApplyPhaseTransition(Phase phase)
    {
        Debug.Log($"[PirateLord] Phase transition: {_currentPhase} → {phase} | HP {_body.CurrentHealth}/{_body.MaxHealth}");
        _currentPhase = phase;
        _movement.SetData(_data.PhaseMovements[(int)phase]);

        if (phase == Phase.P3)
        {
            // Pirate Lord 고유 — 유령선화
            Debug.Log("[PirateLord] Ghost ship activated — collider OFF, HP restored, decay started");
            if (TryGetComponent<Collider>(out var col))
            {
                col.enabled = false;
            }
            _defaultShip.SetActive(false);
            _ghostShip.SetActive(true);
            _body.Repair(_body.MaxHealth);
            _decaying = true;
            _body.OnDeadEvent += HandleBossDeath;
        }

        OnPhaseChanged?.Invoke(phase);
    }

    private void HandleBossDeath()
    {
        _body.OnDeadEvent -= HandleBossDeath;
        Debug.Log($"[PirateLord] Defeated at {transform.position}");
        OnBossDeathEvent?.Invoke(transform.position);
    }
}
