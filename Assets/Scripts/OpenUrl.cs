using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MainManagers
{
    public class UrlManagerиь : MonoBehaviour
    {
        [SerializeField]
        private string _urlForPrivacyPolicy;
        [SerializeField] 
        private string _urlForTermsOfUse;
        [SerializeField]
        private Button _privacyButton;
        [SerializeField] 
        private Button _termsButton;

        private bool _externalOpeningUrlDelayFlag = false;

        private void Awake()
        {
            if (_termsButton != null)
                _termsButton.onClick.AddListener(() => OpenUrlbm(_urlForTermsOfUse));

            if (_privacyButton != null)
                _privacyButton.onClick.AddListener(() => OpenUrlbm(_urlForPrivacyPolicy));
        }

        private void OnDestroy()
        {
            if (_termsButton != null)
                _termsButton.onClick.RemoveListener(() => OpenUrlbm(_urlForTermsOfUse));

            if (_privacyButton != null)
                _privacyButton.onClick.RemoveListener(() => OpenUrlbm(_urlForPrivacyPolicy));
        }

        private async void OpenUrlbm(string url)
        {
            if (_externalOpeningUrlDelayFlag) return;
            _externalOpeningUrlDelayFlag = true;
            await OpenURLAsyncиь(url);
            StartCoroutine(WaitForSecondsиь(1, () => _externalOpeningUrlDelayFlag = false));
        }
    
        private async Task OpenURLAsyncиь(string url)
        {
            await Task.Delay(1); // Ждем один кадр, чтобы избежать блокировки основного потока
            try
            {
                Application.OpenURL(url); // Открываем ссылку
            }
            catch (Exception e)
            {
                Debug.LogError($"Ошибка при открытии ссылки {url}: {e.Message}");
            }
        }

        private IEnumerator WaitForSecondsиь(float seconds, Action callback)
        {
            yield return new WaitForSeconds(seconds);
            callback?.Invoke();
        } 
    }
}

