using UnityEngine;
using UnityEngine.UI;

public class PlanetHealthBar : MonoBehaviour
{

    [SerializeField] private Planet _planet;
    [SerializeField] private Image _healthBarImage;

    private void OnEnable()
    {
        _planet.OnHealthBarChanged += DrawHealtBar;        
    }

    private void OnDisable()
    {
        _planet.OnHealthBarChanged -= DrawHealtBar;
    }

    private void DrawHealtBar()
    {
        _healthBarImage.fillAmount = _planet.GetCurrentHp() / _planet.GetMaxHp();
    }

}