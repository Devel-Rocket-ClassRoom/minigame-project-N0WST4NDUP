using UnityEngine;

public class XPDropper : ItemDropper
{
    [Header("XP Drop")]
    [SerializeField] private int _xp = 1;

    protected override void Drop(Vector3 position)
    {
        if (XPGemPool.Instance == null) return;

        var gem = XPGemPool.Instance.Get();
        gem.transform.position = position;
        gem.Init(_xp);
    }
}
