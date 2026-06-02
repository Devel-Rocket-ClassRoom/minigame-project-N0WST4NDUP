using UnityEngine;

namespace OceanSurvival.UI
{
    public class TitlePulseEffect : MonoBehaviour
    {
        [SerializeField] private float pulseSpeed = 2f;
        [SerializeField] private float scaleAmount = 0.05f;
        
        private Vector3 _initialScale;

        private void Awake()
        {
            _initialScale = transform.localScale;
        }

        private void Update()
        {
            float s = 1f + Mathf.Sin(Time.time * pulseSpeed) * scaleAmount;
            transform.localScale = _initialScale * s;
        }
    }
}
