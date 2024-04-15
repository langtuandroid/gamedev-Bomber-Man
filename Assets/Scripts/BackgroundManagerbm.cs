using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManagerbm : MonoBehaviour
{ 
    
        [SerializeField]
        private Image _backGroundsr;
        [SerializeField]
        private Sprite _bgSmartphonesr;
        [SerializeField]
        private Sprite _bgMidleTabletsr;
        [SerializeField]
        private Sprite _bgTabletsr;
        private void Start()
        {
            CheckDeviceInchesbm();
        }
        private void CheckDeviceInchesbm()
        {
            float screenSizeInchessr = Mathf.Sqrt(Mathf.Pow(Screen.width / Screen.dpi, 2) + Mathf.Pow(Screen.height / Screen.dpi, 2));
            float aspectRatio = (float)Screen.width / Screen.height; 
            Sprite backgroundSpritesr;
            if (screenSizeInchessr >= 7.0f)
            {
                backgroundSpritesr = Mathf.Approximately(aspectRatio, 3f / 5f) ? _bgMidleTabletsr : _bgTabletsr;
            }
            else
            {
                backgroundSpritesr = _bgSmartphonesr;
            }
            _backGroundsr.sprite = backgroundSpritesr;
        }
}