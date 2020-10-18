using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RocketSpawner", menuName = "Orbitality/RocketSpawner")]
public class RocketSpawner : ScriptableObject
{

    [SerializeField] private Rocket _rocketPrefab;    

    private Stack<Rocket> _rocketsPool = new Stack<Rocket>();

    public Rocket SpawnRocket(Transform parent)
    {
        Rocket res;
        if (_rocketsPool.Count > 0)
        {
            res = _rocketsPool.Pop();            
            res.transform.parent = parent;            
        }
        else
        {
            res = Instantiate(_rocketPrefab, parent);
        }
        res.KillAction = () => OnRocketKilled(res);
        return res;
    }

    private void OnRocketKilled(Rocket rocket)
    {
        rocket.transform.parent = null;        
        _rocketsPool.Push(rocket);
    }

}