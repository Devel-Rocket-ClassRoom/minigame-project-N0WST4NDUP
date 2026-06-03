using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [SerializeField] private ShipData _shipData;

    [Header("Dependencies")]
    [SerializeField] private ShipBody _body;
    [SerializeField] private ShipMovement _movement;
    [SerializeField] private ShipComponent _component;

    [Header("Fallback Config")]
    [SerializeField] private MainAttachableBase _fallbackMainSlot;

    private InputAction _throttleAction;
    private InputAction _turnAction;

    private void Awake()
    {
        var input = GetComponent<PlayerInput>();
        _throttleAction = input.actions["Throttle"];
        _turnAction = input.actions["Turn"];

        _body.Init(_shipData);
        _body.OnDeadEvent += PlayerDie;
    }

    private void Start()
    {
        _component.Install(
            GameManager.Instance.PlayerConfig.StartingMain ?? _fallbackMainSlot
        );
    }

    private void OnDestroy()
    {
        _body.OnDeadEvent -= PlayerDie;
    }

    private void Update()
    {
        float throttle = _throttleAction.ReadValue<float>();
        float turn = _turnAction.ReadValue<float>();
        _movement.UpdateMove(throttle, turn);
    }

    private void PlayerDie()
    {
        Debug.Log("Player Died!");
        Destroy(gameObject);
    }
}