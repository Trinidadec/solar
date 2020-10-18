using UnityEngine;

[CreateAssetMenu(fileName = "SolarSystemBuilder", menuName = "Orbitality/SolarSystemBuilder")]
public class SolarSystemBuilder : ScriptableObject
{

    [SerializeField] private SolarSystem _solarSystemPrefab;
    [SerializeField] private Sun _sunPrefab;
    [SerializeField] private Planet[] _planetPrefabs;
    [SerializeField] private float _minOrbitDistance;
    [SerializeField] private float _maxOrbitDistance;
    [SerializeField] private float _minOrbitSpeed;
    [SerializeField] private float _maxOrbitSpeed;
    [SerializeField] private RocketSpawner[] _rocketSpawners;

    public SolarSystem CreateSolarSystem(Transform parent)
    {
        var solarSystem = Instantiate(_solarSystemPrefab, parent);
        var sun = Instantiate(_sunPrefab, solarSystem.transform);
        solarSystem.AddSun(sun);
        for(int i = 0; i < _planetPrefabs.Length; i++)
        {
            var planet = Instantiate(_planetPrefabs[i], solarSystem.transform);
            planet.SolarSystem = solarSystem;

            float randomOrbitDistance = _minOrbitDistance + (_maxOrbitDistance - _minOrbitDistance) * i / (_planetPrefabs.Length - 1);
            float randomOrbitAngle = 2 * Mathf.PI * i / _planetPrefabs.Length;
            float randomOrbitSpeed = Random.Range(_minOrbitSpeed, _maxOrbitSpeed);
            planet.SetOrbit(randomOrbitDistance, randomOrbitAngle, randomOrbitSpeed);

            planet.SetRocketSpawner(_rocketSpawners[Random.Range(0, _rocketSpawners.Length)]);
            solarSystem.AddPlanet(planet);            
        }
        return solarSystem;
    }

}