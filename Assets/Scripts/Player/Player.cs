using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [SerializeField] private ShipData _data;

    [Header("Dependencies")]
    [SerializeField] private ShipBody _body;
    [SerializeField] private ShipMovement _movement;

    private InputAction _throttleAction;
    private InputAction _turnAction;

    private void Awake()
    {
        var input = GetComponent<PlayerInput>();
        _throttleAction = input.actions["Throttle"];
        _turnAction = input.actions["Turn"];

        // 임시
        _body.Init(_data);
        _body.OnDeadEvent += PlayerDie;
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