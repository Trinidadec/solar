using System;
using UnityEngine;

public class Planet : MonoBehaviour
{

    [SerializeField] private RocketLauncher _rocketLauncher;
    [SerializeField] public Attractor attractor;
    [SerializeField] private float _maxHp;
    [SerializeField] private float _rotateSpeed;

    private IPlayer _player;
    private float _currentHp;
    private float _radius;
    private float _angle;
    private float _orbitSpeed;    

    public event Action OnHealthBarChanged;
    public SolarSystem SolarSystem { get; set; }

    private void Awake()
    {
        _currentHp = _maxHp;
    }

    public void SetPlayer(IPlayer player)
    {
        _player = player;
        _player.OnGameStart(this);
    }

    public float GetMaxHp()
    {
        return _maxHp;
    }

    public float GetCurrentHp()
    {
        return _currentHp;
    }

    public void SetOrbit(float orbitDistance, float orbitPosition, float orbitSpeed)
    {
        _radius = orbitDistance;
        _angle = orbitPosition;
        _orbitSpeed = orbitSpeed;
    }

    public void SetRocketSpawner(RocketSpawner rocketSpawner)
    {
        _rocketLauncher.RocketSpawner = rocketSpawner;
    }

    public bool IsRocketLauncherReady()
    {
        return _rocketLauncher.IsReady();
    }

    public void Shoot()
    {
        _rocketLauncher.Shoot(this);
    }

    public void DoDamage(Rocket rocket)
    {
        _currentHp -= rocket.GetDamage();        
        if (OnHealthBarChanged != null)
            OnHealthBarChanged.Invoke();

        if (_currentHp <= 0)
        {
            SolarSystem.KillPlanet(this);
            Destroy(gameObject);
        }
    }

    public float GetRocketLauncherAngle()
    {
        return _rocketLauncher.transform.eulerAngles.z * Mathf.Deg2Rad;
    }

    public void RotateGunLeft()
    {
        _rocketLauncher.transform.Rotate(new Vector3(0, 0, _rotateSpeed) * Time.deltaTime);
    }

    public void RotateGunRight()
    {
        _rocketLauncher.transform.Rotate(new Vector3(0, 0, -_rotateSpeed) * Time.deltaTime);
    }

    void Update()
    {
        _angle += _orbitSpeed * Time.deltaTime;
        transform.position = GetPosition(_angle);
        _player.OnPlanetUpdate();
    }

    private Vector3 GetPosition(float angle)
    {
        return new Vector3(_radius * Mathf.Sin(angle), _radius * Mathf.Cos(angle));
    }

}