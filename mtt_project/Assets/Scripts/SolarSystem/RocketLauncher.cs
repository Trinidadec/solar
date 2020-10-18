using System;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    
    [SerializeField] float _momentumValue;
    [SerializeField] Transform _launchPoint;

    private float _fullCooldownTime;
    private float _cooldownEndTime;

    public RocketSpawner RocketSpawner { get; set; }

    public float GeCooldownProgress()
    {
        if (_cooldownEndTime > Time.time)
            return 1.0f - (_cooldownEndTime - Time.time) / _fullCooldownTime;
        else
            return 1.0f;
    }

    public bool IsReady()
    {
        return Time.time >= _cooldownEndTime;
    }

    public void Shoot(Planet planet)
    {
        if (IsReady())
        {
            var rocket = RocketSpawner.SpawnRocket(null);
            rocket.transform.position = _launchPoint.position;
            rocket.transform.rotation = _launchPoint.rotation;
            rocket.Push(planet, _momentumValue);

            _fullCooldownTime = rocket.GetCooldown();
            _cooldownEndTime = Time.time + _fullCooldownTime;
        }        
    }

}