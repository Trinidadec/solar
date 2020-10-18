using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{

    [SerializeField] private SolarSystemBuilder _solarSystemBuilder;
    [SerializeField] private Player _player;
    [SerializeField] private int _playerIndex;

    private SolarSystem _currentGame;
    private Planet _usersPlanet;

    public UnityEvent OnGameWon;
    public UnityEvent OnGameLost;
    public UnityEvent OnGameEnd;

	public void StartGame ()
    {
        _player = new Player();
        _currentGame = _solarSystemBuilder.CreateSolarSystem(null);
        for(int i = 0; i < _currentGame.GetPlanets().Count; i++)
        {
            if (i == _playerIndex)
            {
                _currentGame.GetPlanets()[i].SetPlayer(_player);
                _usersPlanet = _currentGame.GetPlanets()[i];
            }                
            else
            {
                _currentGame.GetPlanets()[i].SetPlayer(new Bot());
            }                
        }
        _currentGame.OnGameOver += OnGameOver;
    }

    public void PauseGame()
    {
        _currentGame.Pause();
    }

    public void ResumeGame()
    {
        _currentGame.Resume();
    }

    public void EndGame()
    {
        _currentGame.EndGame();
    }

    public void Shoot()
    {
        _player.Shoot();
    }

    private void OnGameOver(Planet winner)
    {        
        _currentGame.OnGameOver -= OnGameOver;        

        if (winner == _usersPlanet)
            OnGameWon.Invoke();
        else
            OnGameLost.Invoke();

        Destroy(_currentGame.gameObject);
        _usersPlanet = null;
    }

}