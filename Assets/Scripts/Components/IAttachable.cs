using UnityEngine;

public interface IAttachable
{
    void Attach(LayerMask target, ShipStats stats);
    void Detach();
}