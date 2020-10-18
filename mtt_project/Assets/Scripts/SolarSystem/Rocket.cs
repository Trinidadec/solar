using System;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    [SerializeField] private Transform _forwardPoint;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _damage;
    [SerializeField] private AnimationCurve _momentumCurve;
    [SerializeField] private float _maxLifeTime;

    private float _momentum;
    private float _flightTime;

    public Action KillAction { get; set; }
    public Planet OwnerPlanet { get; set; }

    public float GetCooldown()
    {
        return _cooldown;
    }

    public float GetDamage()
    {
        return _damage;
    }

    public void Push(Planet ownerPlanet, float momentum)
    {
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;

        OwnerPlanet = ownerPlanet;        
        gameObject.transform.SetParent(OwnerPlanet.SolarSystem.transform);
        gameObject.SetActive(true);        
        
        _flightTime = 0.0f;
        _momentum = momentum;
    }

    public void Kill()
    {
        OwnerPlanet = null;
        gameObject.SetActive(false);
        if (KillAction != null)
            KillAction.Invoke();
    }

    private void FixedUpdate()
    {
        if (_flightTime > _maxLifeTime)
        {
            Kill();
            return;
        }

        if (_momentumCurve.keys[_momentumCurve.length - 1].time > _flightTime)
            ApplyMommentum();        

        AddGravityForces();

        _flightTime += Time.fixedDeltaTime;
    }

    private void ApplyMommentum()
    {
        var rocketDirection = _forwardPoint.position - transform.position;
        var force = _momentum * rocketDirection * _momentumCurve.Evaluate(_flightTime);
        _rigidBody.AddForce(force, ForceMode.Impulse);        
    }

    private void AddGravityForces()
    {
        foreach (var attractor in OwnerPlanet.SolarSystem.GetAttractors())
        {
            if (attractor != OwnerPlanet.attractor)
            {
                var gravityDirection = attractor.transform.position - transform.position;
                float sqrDistance = gravityDirection.sqrMagnitude;
                float forceMagnitude = (_rigidBody.mass * attractor.mass) / sqrDistance;
                var force = forceMagnitude * gravityDirection.normalized;
                _rigidBody.AddForce(force, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var damageReceiver = collision.gameObject.GetComponent<DamageReceiver>();
        if (damageReceiver != null)
            damageReceiver.DoDamage(this);
    }

}