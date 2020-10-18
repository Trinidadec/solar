using UnityEngine;
using UnityEngine.UI;

public class RocketLauncherRechargeBar : MonoBehaviour
{

    [SerializeField] private RocketLauncher _launcher;
    [SerializeField] private Image _rechargerBarImage;

    private void Update()
    {
        _rechargerBarImage.fillAmount = _launcher.GeCooldownProgress();
    }

}