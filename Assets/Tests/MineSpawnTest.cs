using UnityEngine;

public class MineSpawnTest : MonoBehaviour
{
    private void Start()
    {
        var mine = CombatPoolRegistry.Get<Mine>();
        mine.transform.position = transform.position;
        mine.SetConfig(
            new(
                LayerMask.GetMask("Player"),
                100f,
                2f,
                30f));
        mine.Fire(Vector3.zero);
    }
}