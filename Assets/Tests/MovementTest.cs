using UnityEngine;
using UnityEngine.InputSystem;

public class MovementTest : MonoBehaviour
{
    public ShipMovement shipMovement;

    private void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        float throttle = kb.wKey.isPressed ? 1f : 0f;

        float turn = 0f;
        if (kb.aKey.isPressed) turn -= 1f;
        if (kb.dKey.isPressed) turn += 1f;

        shipMovement.UpdateMove(throttle, turn);
    }
}