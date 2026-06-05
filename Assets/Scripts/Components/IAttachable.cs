using UnityEngine;

public interface IAttachable
{
    int Level { get; }
    Sprite Icon { get; }

    void Attach(LayerMask target, ShipStats stats);
    void Detach();
}