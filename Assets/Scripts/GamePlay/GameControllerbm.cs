using System.Collections;
using System.Collections.Generic;
using Integration;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    public class GameControllerbm : MonoBehaviour
    {
        private const int X = 30;
        private const int Y = 20;
        
        public GameObject levelHolder;

        public GameObject[,] level = new GameObject[X, Y];

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
            StartCoroutine(ShowIntegration());
            var componentsInChildren = levelHolder.GetComponentsInChildren<Transform>();
            var array = componentsInChildren;
            foreach (var transform in array)
                if (transform.gameObject.tag != "Floor")
                    level[(int)transform.transform.position.x, (int)transform.transform.position.y] =
                        transform.gameObject;
            level[0, 0] = null;
        }
        
        private IEnumerator ShowIntegration()
        {
            yield return new WaitForSeconds(.5f);
            
            var loadLevelCount = PlayerPrefs.GetInt("IntegrationsCounter", 0);
            loadLevelCount++;
            
            if (loadLevelCount % 2 == 0)
            {
                _adMobController.ShowInterstitialAd();
            } else if (loadLevelCount % 3 == 0)
            {
                _iapService.ShowSubscriptionPanel();
            }
            if (loadLevelCount >= 3)
            {
                loadLevelCount = 0;
            }
            PlayerPrefs.SetInt("IntegrationsCounter", loadLevelCount);
            PlayerPrefs.Save();
        }
    }
}