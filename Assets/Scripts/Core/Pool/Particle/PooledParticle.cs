using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PooledParticle : MonoBehaviour
{
    private ParticleSystem _particle;
    private ParticlePool _pool;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
        var main = _particle.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    public void SetPool(ParticlePool pool) => _pool = pool;

    public void Play(Vector3 position, Quaternion rotation = default)
    {
        transform.SetPositionAndRotation(position, rotation);
        _particle.Play(true);
    }

    private void OnParticleSystemStopped() => _pool?.Release(this);
}