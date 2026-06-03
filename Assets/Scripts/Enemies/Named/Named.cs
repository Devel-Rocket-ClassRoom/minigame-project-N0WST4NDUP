using UnityEngine;

[RequireComponent(typeof(ShipComponent))]
public class Named : MonoBehaviour
{
    private ShipComponent _component;

    private void Awake()
    {
        _component = GetComponent<ShipComponent>();
    }
}