public class SunDamageReceiver : DamageReceiver
{

    public override void DoDamage(Rocket rocket)
    {
        rocket.Kill();
    }

}