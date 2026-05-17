using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    private PlayerInput _input;
    [SerializeField] private ShipMovement _movement;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        float throttle = _input.actions["Throttle"].ReadValue<float>();
        float turn = _input.actions["Turn"].ReadValue<float>();
        _movement.UpdateMove(throttle, turn);
    }
}