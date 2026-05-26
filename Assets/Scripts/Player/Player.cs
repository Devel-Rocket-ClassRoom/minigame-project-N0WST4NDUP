using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    private PlayerInput _input;

    [Header("Dependencies")]
    [SerializeField] private ShipMovement _movement;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        float throttle = _input.actions["Throttle"].ReadValue<float>(); // Fix: _input.actions[Throttle] 캐싱해서 사용
        float turn = _input.actions["Turn"].ReadValue<float>(); // Fix: _input.actions[Turn] 캐싱해서 사용
        _movement.UpdateMove(throttle, turn);
    }
}