using System;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{

    [SerializeField] private Sun _sun;
    [SerializeField] private List<Planet> _planets;
    [SerializeField] private List<Attractor> _attractors;    

    public event Action<Planet> OnGameOver;

    public void AddSun(Sun sun)
    {
        _sun = sun;
        _attractors.Add(sun.attractor);
    }

    public void AddPlanet(Planet planet)
    {
        _planets.Add(planet);
        _attractors.Add(planet.attractor);
    }

    public void KillPlanet(Planet planet)
    {
        _planets.Remove(planet);
        _attractors.Remove(planet.attractor);

        if (_planets.Count == 1)
        {
            EndGame(_planets[0]);
        }            
    }

    public void EndGame()
    {
        EndGame(null);
    }

    public void EndGame(Planet winner)
    {
        Time.timeScale = 1.0f;
        if (OnGameOver != null)
            OnGameOver(winner);
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
    }

    public List<Planet> GetPlanets()
    {
        return _planets;
    }

    public IEnumerable<Attractor> GetAttractors()
    {
        return _attractors;
    }

}