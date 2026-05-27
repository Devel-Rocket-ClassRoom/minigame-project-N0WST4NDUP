using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ShipMovement _movement;

    private InputAction _throttleAction;
    private InputAction _turnAction;

    private void Awake()
    {
        var input = GetComponent<PlayerInput>();
        _throttleAction = input.actions["Throttle"];
        _turnAction = input.actions["Turn"];
    }

    private void Update()
    {
        float throttle = _throttleAction.ReadValue<float>();
        float turn = _turnAction.ReadValue<float>();
        _movement.UpdateMove(throttle, turn);
    }
}