using UnityEngine;

public class AttachableWrapper : MonoBehaviour
{
    [SerializeField] private float _lifetime = 30f;

    private UpgradeDefinition _definition;
    private float _despawnTime;

    public UpgradeDefinition Definition => _definition;

    public void SetDefinition(UpgradeDefinition definition)
    {
        _definition = definition;
        _despawnTime = Time.time + _lifetime;

        Debug.Log($"Attachable {definition} dropped at {transform.position}");
    }

    private void Update()
    {
        if (Time.time >= _despawnTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_definition == null) return;

        // 플레이어가 주움 → 강화/교체 카드 표시
        if (other.CompareTag("Player"))
        {
            if (UpgradeUI.Instance == null) return;

            UpgradeUI.Instance.OpenComponentPickup(_definition);
            Consume();
        }
        else if (other.CompareTag("Named"))
        {
            var named = other.GetComponent<Named>();
            if (named != null)
            {
                named.PickupComponent(_definition);
                Consume();
            }
        }
    }

    private void Consume()
    {
        _definition = null;
        Destroy(gameObject); // 풀링은 추후
    }
}