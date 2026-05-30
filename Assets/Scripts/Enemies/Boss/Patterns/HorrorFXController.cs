using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class HorrorFXController : MonoBehaviour
{
    [SerializeField] private HorrorFXConfig _config;
    [SerializeField] private Volume _volume;
    [SerializeField] private CinemachineBasicMultiChannelPerlin _perlin;
    [SerializeField] private ShipStats _playerStats;

    private float _weight;
    private float _slowTimer;

    private void OnEnable()
    {
        _weight = 0f;
        if (_volume != null) _volume.weight = 0f;
        if (_perlin != null)
        {
            _perlin.AmplitudeGain = _config.PerlinAmplitude;
            _perlin.FrequencyGain = _config.PerlinFrequency;
        }
        _slowTimer = _config.PlayerSlowInterval;
    }

    private void OnDisable()
    {
        if (_volume != null) _volume.weight = 0f;
        if (_perlin != null)
        {
            _perlin.AmplitudeGain = 0f;
            _perlin.FrequencyGain = 0f;
        }
    }

    private void Update()
    {
        if (_weight < 1f)
        {
            _weight = Mathf.Min(1f, _weight + Time.deltaTime / Mathf.Max(0.01f, _config.VolumeWeightLerpSec));
            if (_volume != null) _volume.weight = _weight;
        }

        _slowTimer -= Time.deltaTime;
        if (_slowTimer <= 0f)
        {
            ApplySlow();
            _slowTimer = _config.PlayerSlowInterval;
        }
    }

    private void ApplySlow()
    {
        if (_playerStats == null) return;
        var mod = new Modifier
        {
            Stat = StatType.MoveSpeed,
            Op = ModifierOp.PercentAdd,
            Value = -_config.PlayerSlowPercent
        };
        _playerStats.AddModifier(mod);
        StartCoroutine(RemoveAfter(mod, _config.PlayerSlowDuration));
    }

    private IEnumerator RemoveAfter(Modifier mod, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (_playerStats != null) _playerStats.RemoveModifier(mod);
    }
}
