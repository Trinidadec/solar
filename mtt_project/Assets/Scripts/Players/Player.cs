using UnityEngine;

public class Player : IPlayer
{

    private Planet _planet;

    public void Shoot()
    {
        _planet.Shoot();
    }

    void IPlayer.OnGameStart(Planet planet)
    {
        _planet = planet;
    }

    void IPlayer.OnPlanetUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _planet.Shoot();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _planet.RotateGunLeft();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _planet.RotateGunRight();
        }
    }    

}