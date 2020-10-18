using UnityEngine;

public class Bot : IPlayer
{

    private const float HALF_PI = 0.5f * Mathf.PI;
    private const float TWO_PI = 2 * Mathf.PI;

    private Planet _planet;
    private float _randomDeviationAngle;

    void IPlayer.OnGameStart(Planet planet)
    {
        _planet = planet;
    }

    void IPlayer.OnPlanetUpdate()
    {
        if (_planet.IsRocketLauncherReady())
        {
            _planet.Shoot();
            _randomDeviationAngle = Random.Range(-HALF_PI, HALF_PI);
        }            

        var targetPlanet = FindTargetPlanet();
        if (targetPlanet != null)
            RotateGunToTarget(targetPlanet);
    }    

    private void RotateGunToTarget(Planet targetPlanet)
    {
        Vector2 targetGunVector = _planet.transform.position - targetPlanet.transform.position;
        var targetGunAngle = Mathf.Atan2(targetGunVector.y, targetGunVector.x);
        var currentGunAngle = _planet.GetRocketLauncherAngle() - HALF_PI;
        currentGunAngle += _randomDeviationAngle;
        var diffAngle = currentGunAngle - targetGunAngle;
        while (diffAngle >= Mathf.PI) diffAngle -= TWO_PI;
        while (diffAngle < -Mathf.PI) diffAngle += TWO_PI;
        if (diffAngle < 0) _planet.RotateGunLeft();
        else _planet.RotateGunRight();
    }

    private Planet FindTargetPlanet()
    {
        float maxHp = 0.0f;
        Planet result = null;
        var allPlanets = _planet.SolarSystem.GetPlanets();
        for(int i = 0; i < allPlanets.Count; i++)
        {
            if (allPlanets[i] != _planet && allPlanets[i].GetCurrentHp() > maxHp)
            {
                maxHp = allPlanets[i].GetCurrentHp();
                result = allPlanets[i];
            }
        }
        return result;
    }

}