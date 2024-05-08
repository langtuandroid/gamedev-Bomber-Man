using Integration;
using UnityEngine;
using Zenject;

public class BannerController : MonoBehaviour
{
    private AdMobController _adMobController;
    private IAPService _iapService;

    [Inject]
    private void Construct (AdMobController adMobController, IAPService iapService)
    {
        _adMobController = adMobController;
        _iapService = iapService;
    }

    private void Start()
    {
        if (!_iapService.SubscriptionCanvas.activeSelf)
        {
            _adMobController.ShowBanner(true);
        }
    }
}
