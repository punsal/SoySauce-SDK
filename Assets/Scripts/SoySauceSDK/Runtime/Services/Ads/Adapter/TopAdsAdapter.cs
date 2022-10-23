using System;
using _3rdParty.Runtime;
using SoySauceSDK.Runtime.Services.Ads.Adapter.Interface;
using UnityEngine;

namespace SoySauceSDK.Runtime.Services.Ads.Adapter
{
    public class TopAdsAdapter : IAdsAdapter
    {
        private string _adId;
        private bool _isAdReady;
        public Action OnAdShown { get; set; }

        public bool Create()
        {
            try
            {
                TopAds.InitializeSDK();
            }
            catch (Exception e)
            {
                Debug.Log($"TopAds Initialization is failed. Caused by:\n{e.Message}");
                return false;
            }

            return true;
        }

        public bool Init(bool consent, string adId)
        {
            _adId = adId;
            _isAdReady = false;

            if (consent)
                TopAds.GrantConsent();
            else
                TopAds.RevokeConsent();

            TopAds.OnAdLoadedEvent += AdLoaded;
            TopAds.OnAdFailedEvent += AdFailed;
            TopAds.OnAdShownEvent += AdShown;

            TopAds.RequestAd(adId);

            return true;
        }

        public bool IsAdReady()
        {
            return _isAdReady;
        }

        public void ShowAd()
        {
            TopAds.ShowAd(_adId);
        }

        private void AdLoaded()
        {
            _isAdReady = true;
        }

        private void AdFailed()
        {
            _isAdReady = false;
            TopAds.RequestAd(_adId);
        }

        private void AdShown()
        {
            OnAdShown?.Invoke();

            _isAdReady = false;
            TopAds.RequestAd(_adId);
        }
    }
}