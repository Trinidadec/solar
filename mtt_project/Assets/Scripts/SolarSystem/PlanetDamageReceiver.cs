using UnityEngine;

public class PlanetDamageReceiver : DamageReceiver
{

    [SerializeField] private Planet _planet;

    public override void DoDamage(Rocket rocket)
    {
        if (_planet != rocket.OwnerPlanet)
        {
            _planet.DoDamage(rocket);
            rocket.Kill();
        }
    }

}