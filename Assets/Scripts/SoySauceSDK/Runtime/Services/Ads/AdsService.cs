using System;
using SoySauceSDK.Runtime.Services.Ads.Adapter;
using SoySauceSDK.Runtime.Services.Ads.Adapter.Interface;
using SoySauceSDK.Runtime.Services.Ads.Interface;
using UnityEngine;

namespace SoySauceSDK.Runtime.Services.Ads
{
    public class AdsService : IAdsService
    {
        private readonly AdsServiceSettings _adsServiceSettings;

        private IAdsAdapter _adapter;

        private bool _currentConsent;
        private int _gamesPlayed;
        private DateTime _lastShownAdDateTime = new(0);

        public AdsService(AdsServiceSettings adsServiceSettings)
        {
            _adsServiceSettings = adsServiceSettings;
            _adapter = AdsAdapterFactory.Create();
            AdsAdapterErrorHandler.OnAdapterErrorSolved += OnAdapterErrorSolved;
        }

        public Action OnAdShown { get; set; }

        public void Init(bool consent, Action<bool> onComplete)
        {
            var didSucceed = InitAdapter(consent);

            onComplete?.Invoke(didSucceed);
        }

        public void ShowAd()
        {
            if (!_adapter.IsAdReady())
            {
                Debug.Log("Ad is not ready to show");
                return;
            }

            if (_gamesPlayed < _adsServiceSettings.GamesBetweenAds)
            {
                Debug.Log(
                    $"Ad is not shown, number of games played is not matched. ({_gamesPlayed}/{_adsServiceSettings.GamesBetweenAds})");
                return;
            }

            var betweenLastSeenAd = (DateTime.Now - _lastShownAdDateTime).TotalSeconds;
            if (betweenLastSeenAd < _adsServiceSettings.SecondsBetweenAds)
            {
                Debug.Log($"Ad is not shown, player has already seen an ad {betweenLastSeenAd}secs ago.");
                return;
            }

            Debug.Log("Ad is shown.");
            _adapter.ShowAd();
            _lastShownAdDateTime = DateTime.Now;
            _gamesPlayed = 0;
        }

        public void IncrementGamesCounter()
        {
            _gamesPlayed++;
        }

        private bool InitAdapter(bool consent)
        {
            _currentConsent = consent;

            var didSucceed = _adapter.Init(consent, _adsServiceSettings.AdId);

            if (didSucceed) _adapter.OnAdShown += () => OnAdShown?.Invoke();

            return didSucceed;
        }

        private void OnAdapterErrorSolved(IAdsAdapter adsAdapter)
        {
            _adapter = adsAdapter;

            var didSucceed = InitAdapter(_currentConsent);
            if (didSucceed) Debug.Log("AdsAdapter error is solved.");
        }
    }
}